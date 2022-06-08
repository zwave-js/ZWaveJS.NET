const { Driver } = require("zwave-js");
const { ZwavejsServer } = require("@zwave-js/server");

console.log("ZWaveJS.NET: Preparing server...");

const serialPort = process.env.SERIAL_PORT;
const wsPort = parseInt(process.env.WS_PORT);
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
const server = new ZwavejsServer(driver, { port: wsPort, host: "localhost" });
driver.on("error", (e) => {});

driver.on("driver ready", () => {
    server.start();
});

console.log("ZWaveJS.NET: Starting driver...");
driver.start()
.then(() =>{
    process.on("beforeExit",CleanUp);
})
.catch((e) => {
    process.stderr.write("1\n");
})

const CleanUp = async (code) =>{
    console.log("ZWaveJS.NET: Cleaning up...");
    await server.destroy();
    await driver.destroy();
    process.exit(0);

}
