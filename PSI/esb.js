const esbuild = require("esbuild");
const { cp, readFile, writeFile } = require("fs/promises");
const { exists } = require("fs-extra");
const { join } = require("path");
const outputDir = "build";
const outfile = `${outputDir}/server.js`;

const externals = [
  "@serialport/bindings-cpp/prebuilds",
  "zwave-js/package.json",
  "@zwave-js/config/package.json",
  "@zwave-js/config/config",
  "@zwave-js/config/build",
];

const cleanPkgJson = (json) => {
  delete json.devDependencies;
  delete json.dependencies;
  return json;
};

const patchPkgJson = async (path) => {
  const pkgJsonPath = join(outputDir, path, "package.json");
  const pkgJson = require("./" + pkgJsonPath);
  cleanPkgJson(pkgJson);
  delete pkgJson.scripts;
  delete pkgJson.exports;
  await writeFile(pkgJsonPath, JSON.stringify(pkgJson, null, 2));
};

const nativeNodeModulesPlugin = {
  name: "native-node-modules",
  setup(build) {
    build.onResolve({ filter: /\.node$/, namespace: "file" }, (args) => ({
      path: require.resolve(args.path, { paths: [args.resolveDir] }),
      namespace: "node-file",
    }));

    build.onLoad({ filter: /.*/, namespace: "node-file" }, (args) => ({
      contents: `
          import path from ${JSON.stringify(args.path)}
          try { module.exports = require(path) }
          catch {}
        `,
    }));

    build.onResolve({ filter: /\.node$/, namespace: "node-file" }, (args) => ({
      path: args.path,
      namespace: "file",
    }));

    let opts = build.initialOptions;
    opts.loader = opts.loader || {};
    opts.loader[".node"] = "file";
  },
};

const run = async () => {
  const config = {
    entryPoints: ["./server_source.js"],
    plugins: [nativeNodeModulesPlugin],
    bundle: true,
    platform: "node",
    target: "node18",
    outfile,
    external: externals,
  };
  await esbuild.build(config);

  const patchedServer = (await readFile(outfile, "utf-8"))
    .replace(
      /__dirname, "\.\.\/"/g,
      '__dirname, "./node_modules/@serialport/bindings-cpp"'
    )
    .replace(
      `__dirname, "../package.json"`,
      `__dirname, "./node_modules/@zwave-js/config/package.json"`
    )
    .replace(
      `__dirname, "../config"`,
      `__dirname, "./node_modules/@zwave-js/config/config"`
    );

  await writeFile(outfile, patchedServer);

  for (const ext of externals) {
    const path = ext.startsWith("./") ? ext : `node_modules/${ext}`;
    if (await exists(path)) {
      await cp(path, `${outputDir}/${path}`, { recursive: true });
    }
  }

  const pkgJson = require("./package.json");
  cleanPkgJson(pkgJson);
  pkgJson.scripts = {
    start: "node server.js",
  };

  pkgJson.bin = "server.js";
  pkgJson.pkg = {
    assets: ["node_modules/**"],
  };

  await writeFile(
    `${outputDir}/package.json`,
    JSON.stringify(pkgJson, null, 2)
  );

  await patchPkgJson("node_modules/@zwave-js/config");
  await patchPkgJson("node_modules/zwave-js");
};

run().catch((err) => {
  console.error(err);
  process.exit(1);
});
