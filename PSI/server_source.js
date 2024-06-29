const { Driver } = require('zwave-js');
const { ZwavejsServer } = require('@zwave-js/server');

const serialPort = process.env.SERIAL_PORT;
const wsPort = parseInt(process.env.WS_PORT);
const driverOptions = JSON.parse(process.env.CONFIG);
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
const server = new ZwavejsServer(driver, { port: wsPort, host: 'localhost' });
server.on('listening', () => {
	ServerStarted = true;
});
driver.on('error', (e) => {});

driver.on('driver ready', () => {
	server.start();
});

driver
	.start()
	.then(() => {
		DriverStarted = true;
		process.stdin.on('data', HandleInput);
	})
	.catch((e) => {
		process.stderr.write('1\n');
	});

const HandleInput = async (Data) => {
	if (Data.toString().trim() === 'KILL') {
		if (ServerStarted) await server.destroy();
		if (DriverStarted) await driver.destroy();
		process.exit(0);
	}
};
