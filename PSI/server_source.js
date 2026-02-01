const { Driver } = require('zwave-js');
const { ZwavejsServer } = require('@zwave-js/server');
const { PKGFSDriver } = require('./PKGFSDriver');

const serialPort = process.env.SERIAL_PORT;
const wsPort = parseInt(process.env.WS_PORT);
const driverOptions = JSON.parse(process.env.CONFIG);
driverOptions.host = {
	fs: new PKGFSDriver()
};
let ServerStarted = false;
let DriverStarted = false;

if (driverOptions.securityKeys) {
	for (const key of Object.keys(driverOptions.securityKeys)) {
		driverOptions.securityKeys[key] = Buffer.from(driverOptions.securityKeys[key], 'hex');
	}
}

if (driverOptions.securityKeysLongRange) {
	for (const key of Object.keys(driverOptions.securityKeysLongRange)) {
		driverOptions.securityKeysLongRange[key] = Buffer.from(driverOptions.securityKeysLongRange[key], 'hex');
	}
}

const driver = new Driver(serialPort, driverOptions);
const server = new ZwavejsServer(driver, { port: wsPort, host: 'localhost', reconnect: false, 'disable-dns-sd': true });
server.on('listening', () => {
	ServerStarted = true;
});
driver.on('error', (e) => {
	e.start = false;
	process.stderr.write(`${JSON.stringify(e)}\n`);
});

driver.on('driver ready', () => {
	if (!ServerStarted) {
		server.start();
	}
});

driver
	.start()
	.then(() => {
		DriverStarted = true;
		process.stdin.on('data', HandleInput);
	})
	.catch((e) => {
		e.start = true;
		process.stderr.write(`${JSON.stringify(e)}\n`);
	});

const HandleInput = async (Data) => {
	if (Data.toString().trim() === 'KILL') {
		if (ServerStarted) await server.destroy();
		if (DriverStarted) await driver.destroy();
		process.exit(0);
	}
};
