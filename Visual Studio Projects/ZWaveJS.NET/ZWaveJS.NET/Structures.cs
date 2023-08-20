using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace ZWaveJS.NET
{
    class CustomBooleanJsonConverter : JsonConverter<bool>
    {
        public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.ValueType == typeof(string) && reader.Value.ToString().Equals("unknown"))
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(reader.Value);
            }
        }
        public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }

    public class SmartStartProvisioningEntry
    {
        internal SmartStartProvisioningEntry() { }

        [Newtonsoft.Json.JsonProperty]
        public string dsk { get; private set; }
        [Newtonsoft.Json.JsonProperty]
        public int[] securityClasses { get; private set; }
        public int version { get; private set; }
        [Newtonsoft.Json.JsonProperty]
        public int[] requestedSecurityClasses { get; private set; }
        [Newtonsoft.Json.JsonProperty]
        public int genericDeviceClass { get; private set; }
        [Newtonsoft.Json.JsonProperty]
        public int specificDeviceClass { get; private set; }
        [Newtonsoft.Json.JsonProperty]
        public int installerIconType { get; private set; }
        [Newtonsoft.Json.JsonProperty]
        public int manufacturerId { get; private set; }
        [Newtonsoft.Json.JsonProperty]
        public int productType { get; private set; }
        [Newtonsoft.Json.JsonProperty]
        public int productId { get; private set; }
        [Newtonsoft.Json.JsonProperty]
        public decimal applicationVersion { get; private set; }


    }

    public class AssociationAddress
    {
        [Newtonsoft.Json.JsonProperty]
        public int nodeId { get;  set; }

        [Newtonsoft.Json.JsonProperty]
        public int? endpoint { get;  set; }
    }

    public class NetworkHealStats
    {
        internal NetworkHealStats() { }
        public int[] HealedNodes { get; internal set; }
        public int[] SkippedNodes { get; internal set; }
        public int[] FailedNodes { get; internal set; }
    }

    public class NetworkHealDoneArgs : NetworkHealStats
    {
        internal NetworkHealDoneArgs() { }
    }

    public class NetworkHealProgressArgs : NetworkHealStats
    {
        internal NetworkHealProgressArgs() { }
        public int[] PendingNodes { get; internal set; }
    }

    public class AssociationGroup
    {

        internal AssociationGroup() { }

        [Newtonsoft.Json.JsonProperty]
        public int maxNodes { get; internal set; }

        [Newtonsoft.Json.JsonProperty]
        public bool isLifeline { get; internal set; }

        [Newtonsoft.Json.JsonProperty]
        public string label { get; internal set; }
    }

    public class InclusionGrant
    {
        public Enums.SecurityClass[] securityClasses { get; set; }
        public bool clientSideAuth { get; set; }
    }

    public class ValueMetadata
    {
        internal ValueMetadata() { }

        [Newtonsoft.Json.JsonProperty]
        public string type { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool readable { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool writeable { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string label { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string description { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int @default { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int min { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int max { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public Dictionary<string, string> states { get; internal set; }
    }

    public class DeviceClass
    {
        internal DeviceClass() { }

        [Newtonsoft.Json.JsonProperty]
        public DeviceClassType basic { get; internal  set; }
        [Newtonsoft.Json.JsonProperty]
        public DeviceClassType generic { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public DeviceClassType specific { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int[] mandatorySupportedCCs { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int[] mandatoryControlledCCs { get; internal set; }
    }

    public class DeviceClassType
    {
        internal DeviceClassType() { }

        [Newtonsoft.Json.JsonProperty]
        public int key { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string label { get; internal set; }
    }

    public class DeviceConfig
    {
        internal DeviceConfig() { }

        [Newtonsoft.Json.JsonProperty]
        public string filename { get;internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool isEmbedded { get;internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string manufacturer { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string label { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string description { get; internal  set; }
        [Newtonsoft.Json.JsonProperty]
        public Device[] devices { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public FirmwareVersion firmwareVersion { get; internal set; }
    }

    public class CommandClass
    {
        internal CommandClass() { }

        [Newtonsoft.Json.JsonProperty]
        public int id { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string name { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int version { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool isSecure { get; internal set; }
    }

    public class FirmwareVersion
    {
        internal FirmwareVersion() { }

        [Newtonsoft.Json.JsonProperty]
        public string min { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string max { get; internal set; }
    }

    public class Device
    {
        internal Device() { }

        [Newtonsoft.Json.JsonProperty]
        public int productType { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int productId { get; internal set; }
    }

    public class ValueID
    {
        public int commandClass { get; set; }
        public int endpoint { get; set; }
        public object property { get; set; }
        public object propertyKey { get; set; }
        public string commandClassName { get; set; }
        public string propertyName { get; set; }
        public string propertyKeyName { get; set; }
    }

    public class SetValueAPIOptions
    {
        public string transitionDuration { get; set; }
        public int volume { get; set; }
    }

    public class NodeInterviewFailedEventArgs
    {
        internal NodeInterviewFailedEventArgs() { }

        [Newtonsoft.Json.JsonProperty]
        public string errorMessage { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool isFinal { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int attempt { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int maxAttempts { get; internal set; }
    }

    public class LifelineHealthCheckSummary
    {
        internal LifelineHealthCheckSummary() { }

        [Newtonsoft.Json.JsonProperty]
        public  LifelineHealthCheckResult[] results { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int rating { get; internal set; }
    }

    public class LifelineHealthCheckResult
    {
        internal LifelineHealthCheckResult() { }

        [Newtonsoft.Json.JsonProperty]
        public int routeChanges { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int latency { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int numNeighbors { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int failedPingsNode { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int minPowerlevel { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int failedPingsController { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int snrMargin { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int rating { get; internal set; }
    }

    public class InclusionResultArgs
    {
        internal InclusionResultArgs() { }
        [Newtonsoft.Json.JsonProperty]
        public bool lowSecurity { get; internal set; }
        public Enums.SecurityBootstrapFailure lowSecurityReason { get; internal set; }
    }

    public delegate string ValidateDSKAndEnterPIN(string dsk);
    public delegate InclusionGrant GrantSecurityClasses(InclusionGrant requested);
    public delegate void Abort();

    public class InclusionOptions
    {
        public Enums.InclusionStrategy strategy { get; set; }
        public bool forceSecurity { get; set; }
        public InclusionUserCallbacks userCallbacks { get;  set; }
    }

    public class InclusionUserCallbacks
    {
        public ValidateDSKAndEnterPIN validateDSKAndEnterPIN { get; set; }
        public GrantSecurityClasses grantSecurityClasses { get; set; }
        public Abort abort { get; set; }
    }

    public class LoggingEventArgs
    {
        internal LoggingEventArgs() { }

        [Newtonsoft.Json.JsonProperty]
        public string formattedMessage { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string direction { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string primaryTags { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string secondaryTags { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int? secondaryTagPadding { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool? multiline { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string timestamp { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string label { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string message { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string level { get; internal set; }
    }

    public class ValueUpdatedArgs : ValueID
    {
        internal ValueUpdatedArgs() { }

        [Newtonsoft.Json.JsonProperty]
        public object prevValue { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public object newValue { get; internal set; }
    }

    public class ValueAddedArgs : ValueID
    {
        internal ValueAddedArgs() { }

        [Newtonsoft.Json.JsonProperty]
        public object newValue { get; internal set; }
    }

    public class ValueRemovedArgs : ValueID
    {
        internal ValueRemovedArgs() { }

        [Newtonsoft.Json.JsonProperty]
        public object newValue { get; internal set; }
    }

    public class ValueNotificationArgs : ValueID
    {
        internal ValueNotificationArgs() { }

        [Newtonsoft.Json.JsonProperty]
        public object value { get; internal set; }
      
    }

    public class RefreshInfoOptions
    {
        public bool resetSecurityClasses { get; set; }
        public bool waitForWakeup { get; set; }
    }
}
