
const { Driver, ZWaveError, ZWaveErrorCodes } = require("zwave-js")
const Server = require("@zwave-js/server")

let WSServer;

const SieralPort = process.env.SERIAL_PORT;
const WSPort = parseInt(process.env.WS_PORT);
const DriverOptions = JSON.parse(process.env.CONFIG)

if (DriverOptions.securityKeys) {
    for (const key in DriverOptions.securityKeys) {
        DriverOptions.securityKeys[key] = Buffer.from(DriverOptions.securityKeys[key], 'hex');
    }
}

const _Driver = new Driver(SieralPort, DriverOptions);
WSServer = new Server.ZwavejsServer(_Driver, {port: WSPort, host: 'localhost'})
_Driver.on('error', (e) => {
    if (e instanceof ZWaveError && e.code === ZWaveErrorCodes.Driver_Failed) {
        process.exit(0);
    }
})

_Driver.on("driver ready", () => {
    WSServer.start();
})

_Driver.start()
    .catch((e) => {
        process.exit(0);
    })
    .then(() => {

    })