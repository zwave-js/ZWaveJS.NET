using System;
using System.IO;
using Newtonsoft.Json;
namespace ZWaveJS.NET
{

    public class ZWaveOptions
    {
        public ZWaveOptions()
        {
            this.logConfig = new CFGLogConfig();
            this.logConfig.enabled = false;
            this.logConfig.logToFile = false;
            this.logConfig.level = Enums.LogLevel.Debug;
            this.logConfig.filename = Path.Combine(Directory.GetCurrentDirectory(), "zwave-js.log");

            this.storage = new CFGStorage();
            this.storage.throttle = "normal";
            this.storage.cacheDir = Path.Combine(Directory.GetCurrentDirectory(), "zwave-js-cache");
            this.enableSoftReset = false;
            this.disableOptimisticValueUpdate = false;
            this.emitValueUpdateAfterSetValue = false;
        }

        public CFGTimeouts timeouts { get; set; }
        public CFGLogConfig logConfig { get; private set; }
        public CFGStorage storage { get; private set; }
        public bool enableSoftReset { get; set; }
        public CFGSecurityKeys securityKeys { get; set; }
        public CFGInterview interview { get; set; }
        public bool disableOptimisticValueUpdate { get; set; }
        public bool emitValueUpdateAfterSetValue { get; set; }
    }

    public class CFGInterview
    {
        public bool queryAllUserCodes { get; set; }
    }

    public class CFGTimeouts
    {

        public int ack { get; set; }
        public int response { get; set; }
        public int sendDataCallback { get; set; }
        public int report { get; set; }
        public int nonce { get; set; }
        public int serialAPIStarted { get; set; }
    }

    public class CFGLogConfig
    {
        public bool logToFile { get; set; }
        public bool enabled { get; set; }
        public Enums.LogLevel level { get; set; }
        public int[] nodeFilter { get; set; }
        public string filename { get; set; }
    }

    public class CFGStorage
    {

        public string cacheDir { get; set; }
        public string deviceConfigPriorityDir { get; set; }
        public string throttle { get; set; }
    }

    public class CFGSecurityKeys
    {

        public string S2_Unauthenticated { get; set; }
        public string S2_Authenticated { get; set; }
        public string S2_AccessControl { get; set; }
        public string S0_Legacy { get; set; }
    }
}
