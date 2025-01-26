const esbuild = require('esbuild');
const { cp, readFile, writeFile } = require('fs/promises');
const { exists } = require('fs-extra');
const outputDir = 'build';
const outfile = `${outputDir}/server.js`;
const { join } = require('path');

const externals = [
	'@serialport/bindings-cpp/prebuilds',
	'zwave-js/package.json',
	'@zwave-js/config/package.json',
	'@zwave-js/config/config',
	'@zwave-js/config/build',
	'@zwave-js/server/package.json'
];

const nativeNodeModulesPlugin = {
	name: 'native-node-modules',
	setup(build) {
		build.onResolve({ filter: /\.node$/, namespace: 'file' }, (args) => ({
			path: require.resolve(args.path, { paths: [args.resolveDir] }),
			namespace: 'node-file'
		}));

		build.onLoad({ filter: /.*/, namespace: 'node-file' }, (args) => ({
			contents: `
          import path from ${JSON.stringify(args.path)}
          try { module.exports = require(path) }
          catch {}
        `
		}));

		build.onResolve({ filter: /\.node$/, namespace: 'node-file' }, (args) => ({
			path: args.path,
			namespace: 'file'
		}));

		const opts = build.initialOptions;
		opts.loader = opts.loader || {};
		opts.loader['.node'] = 'file';
	}
};

const run = async () => {
	const config = {
		entryPoints: ['./server_source.js'],
		plugins: [nativeNodeModulesPlugin],
		bundle: true,
		platform: 'node',
		target: 'node18',
		outfile,
		external: externals
	};
	await esbuild.build(config);

	const patchedServer = (await readFile(outfile, 'utf-8'))
		.replace(/__dirname, "\.\.\/"/g, '__dirname, "./node_modules/@serialport/bindings-cpp"')
		.replace('../../package.json', './node_modules/@zwave-js/server/package.json');

	await writeFile(outfile, patchedServer);

	for (const ext of externals) {
		const path = ext.startsWith('./') ? ext : `node_modules/${ext}`;
		if (await exists(path)) {
			await cp(path, `${outputDir}/${path}`, { recursive: true });
		}
	}

	let PKGFile;
	let PackagePatch;

	const getPath = (path) => {
		return join(outputDir, path, 'package.json');
	};

	const patch = (Package) => {
		delete Package.dependencies;
		delete Package.devDependencies;
		delete Package.scripts;
		delete Package.exports;
		delete Package.optionalDependencies;
	};

	/* Root Package File */
	PKGFile = './package.json';
	PackagePatch = require(PKGFile);
	delete PackagePatch.dependencies;
	delete PackagePatch.devDependencies;
	delete PackagePatch.optionalDependencies;
	PackagePatch.scripts = {
		start: 'node server.js'
	};
	PackagePatch.bin = 'server.js';
	PackagePatch.pkg = {
		assets: ['node_modules/**']
	};
	await writeFile(`${outputDir}/package.json`, JSON.stringify(PackagePatch, null, 2));

	/* Config Package File */
	PKGFile = getPath('node_modules/@zwave-js/config');
	console.log(`Patching ${PKGFile}`);
	PackagePatch = require(`./${PKGFile}`);
	patch(PackagePatch);
	await writeFile(PKGFile, JSON.stringify(PackagePatch, null, 2));

	/* ZwaveJS Package File */
	PKGFile = getPath('node_modules/zwave-js');
	console.log(`Patching ${PKGFile}`);
	PackagePatch = require(`./${PKGFile}`);
	patch(PackagePatch);
	await writeFile(PKGFile, JSON.stringify(PackagePatch, null, 2));

	/* Server Package File */
	PKGFile = getPath('node_modules/@zwave-js/server');
	console.log(`Patching ${PKGFile}`);
	PackagePatch = require(`./${PKGFile}`);
	patch(PackagePatch);
	await writeFile(PKGFile, JSON.stringify(PackagePatch, null, 2));
};

run().catch((err) => {
	console.error(err);
	process.exit(1);
});
