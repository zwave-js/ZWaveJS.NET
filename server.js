
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
/* 
 * Here we dont supply a port (number), but an object : { port: number ,host: string }
 * https://nodejs.org/api/net.html#serverlistenoptions-callback
 * It's hacky, but zwave-js-server doesn't offer security
 * https://github.com/zwave-js/zwave-js-server/blob/a4a769924ebef527d480c51c33fee4372f4734ce/src/lib/server.ts#L407
 */

WSServer = new Server.ZwavejsServer(_Driver, { port: { port: WSPort, host: 'localhost' } })
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