// @ts-check

const { Driver, ZWaveError, ZWaveErrorCodes } = require("zwave-js");
const { ZwavejsServer } = require("@zwave-js/server");

console.log("ZWaveJS.NET: Preparing server...");

const serialPort = process.env.SERIAL_PORT;
const wsPort = parseInt(process.env.WS_PORT);
/** @type {import("zwave-js").ZWaveOptions} */
const driverOptions = JSON.parse(process.env.CONFIG);

console.log(`ZWaveJS.NET: Serial Port: ${serialPort}, WSPort: ${wsPort}`);

if (driverOptions.securityKeys) {
    for (const key of Object.keys(driverOptions.securityKeys)) {
        driverOptions.securityKeys[key] = Buffer.from(
            driverOptions.securityKeys[key],
            "hex"
        );
    }
}

console.log("ZWaveJS.NET: Instantiating driver...");
const driver = new Driver(serialPort, driverOptions);
// @ts-expect-error The host property is a workaround
const server = new ZwavejsServer(driver, { port: wsPort, host: "localhost" });
driver.on("error", (e) => {
    console.error(e);
    if (e instanceof ZWaveError && e.code === ZWaveErrorCodes.Driver_Failed) {
        process.exit(2); // Exit code 2: restart requested
    }
});

driver.on("driver ready", () => {
    server.start();
});

console.log("ZWaveJS.NET: Starting driver...");
driver.start().catch((e) => {
    console.error(e);
    process.exit(1); // Exit code 1: cannot start
});
