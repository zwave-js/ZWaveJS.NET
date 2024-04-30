using System.IO;

namespace ZWaveJS.NET
{

    public class ZWaveOptions
    {
        public ZWaveOptions()
        {
            this.logConfig = new CFGLogConfig();
            this.interview = new CFGInterview();
            this.storage = new CFGStorage();
            this.securityKeys = new CFGSecurityKeys();
            this.securityKeysLongRange = new CFGSecurityKeysLR();
            this.timeouts = new CFGTimeouts();
            this.enableSoftReset = true;
            this.disableOptimisticValueUpdate = false;
            this.emitValueUpdateAfterSetValue = false;
        }

        public CFGTimeouts timeouts { get; set; }
        public CFGLogConfig logConfig { get; set; }
        public CFGStorage storage { get; set; }
        public CFGSecurityKeys securityKeys { get; set; }
        public CFGSecurityKeysLR securityKeysLongRange { get; set; }
        public CFGInterview interview { get; set; }
        public bool enableSoftReset { get; set; }
        public bool disableOptimisticValueUpdate { get; set; }
        public bool emitValueUpdateAfterSetValue { get; set; }

        internal bool MissingLRKeys()
        {
            if (this.securityKeysLongRange == null)
                return true;

            if (this.securityKeysLongRange.S2_AccessControl == null)
                return true;

            if (this.securityKeysLongRange.S2_Authenticated == null)
                return true;



            return false;
        }

        internal bool MissingKeys(bool IncludeS2, bool IncludeS0)
        {
            if(this.securityKeys == null)
                return true;

            if (this.securityKeys.S0_Legacy == null && IncludeS0)
                return true;

            if (this.securityKeys.S2_AccessControl == null && IncludeS2)
                return true;

            if (this.securityKeys.S2_Authenticated == null && IncludeS2)
                return true;

            if (this.securityKeys.S2_Unauthenticated == null && IncludeS2)
                return true;

            return false;

        }

        internal bool CheckKeyLengthLR()
        {
            if (this.securityKeysLongRange != null && this.securityKeysLongRange.S2_AccessControl != null && this.securityKeysLongRange.S2_AccessControl.Length != 32)
                return false;

            if (this.securityKeysLongRange != null && this.securityKeysLongRange.S2_Authenticated != null && this.securityKeysLongRange.S2_Authenticated.Length != 32)
                return false;

            return true;

        }

        internal bool CheckKeyLength()
        {
            if (this.securityKeys != null && this.securityKeys.S0_Legacy != null && this.securityKeys.S0_Legacy.Length != 32)
                return false;

            if (this.securityKeys != null && this.securityKeys.S2_AccessControl != null && this.securityKeys.S2_AccessControl.Length != 32)
                return false;

            if (this.securityKeys != null && this.securityKeys.S2_Authenticated != null && this.securityKeys.S2_Authenticated.Length != 32)
                return false;

            if (this.securityKeys != null && this.securityKeys.S2_Unauthenticated != null && this.securityKeys.S2_Unauthenticated.Length != 32)
                return false;

            return true;

        }
    }

    public class CFGInterview
    {
        public CFGInterview()
        {
            this.queryAllUserCodes = false;
            this.disableOnNodeAdded = false;
        }

        public bool queryAllUserCodes { get; set; }
        public bool disableOnNodeAdded { get; set; }
    }

    public class CFGTimeouts
    {
        public int? ack { get; set; }
        public int? response { get; set; }
        public int? sendDataCallback { get; set; }
        public int? report { get; set; }
        public int? nonce { get; set; }
        public int? serialAPIStarted { get; set; }
    }

    public class CFGLogConfig
    {
        public CFGLogConfig()
        {
            this.enabled = false;
            this.logToFile = false;
            this.level = Enums.LogLevel.Debug;
            this.filename = Path.Combine(Directory.GetCurrentDirectory(), "zwave-js.log");
        }

        public bool logToFile { get; set; }
        public bool enabled { get; set; }
        public Enums.LogLevel level { get; set; }
        public int[] nodeFilter { get; set; }
        public string filename { get; set; }
    }

    public class CFGStorage
    {

        public CFGStorage()
        {
            this.throttle = "normal";
            this.cacheDir = Path.Combine(Directory.GetCurrentDirectory(), "zwave-js-cache");
        }

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

    public class CFGSecurityKeysLR
    {
        public string S2_Authenticated { get; set; }
        public string S2_AccessControl { get; set; }
    }
}
