using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using Newtonsoft.Json;
using static ZWaveJS.NET.Enums;

namespace ZWaveJS.NET
{
    public class QRProvisioningInformation
    {
        internal QRProvisioningInformation() { }

        [Newtonsoft.Json.JsonProperty]
        public QRCodeVersion version { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public SecurityClass[] securityClasses { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string dsk { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int genericDeviceClass { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int specificDeviceClass { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int installerIconType { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int manufacturerId { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int productType { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int productId { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string applicationVersion { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int? maxInclusionRequestInterval { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public  string uuid { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public Protocols[] supportedProtocols { get; internal set; }
    }

    public class SetValueResult
    {
        internal SetValueResult() { }

        [Newtonsoft.Json.JsonProperty]
        public Enums.SetValueStatus status { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string remainingDuration { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string message { get; internal set; }
    }



    public class FirmwareUpdateFileInfo
    {
        internal FirmwareUpdateFileInfo() { }

        [Newtonsoft.Json.JsonProperty]
        public int target { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string url { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string integrity { get; internal set; }
    }

    public class FirmwareUpdateDeviceID
    {
        internal FirmwareUpdateDeviceID() { }

        [Newtonsoft.Json.JsonProperty]
        public int manufacturerId { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int productType { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int productId { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string firmwareVersion { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public Enums.RFRegion? rfRegion { get; internal set; }
    }


    public class FirmwareUpdateInfo
    {
        internal FirmwareUpdateInfo() { }

        [Newtonsoft.Json.JsonProperty]
        public FirmwareUpdateDeviceID device { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string version { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string changelog { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string channel { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public FirmwareUpdateFileInfo[] files { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool downgrade { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string normalizedVersion { get; internal set; }
    }

    public class PowerLevel
    {
        internal PowerLevel() { }

        [Newtonsoft.Json.JsonProperty]
        public decimal powerlevel { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public decimal measured0dBm { get; internal set; }
    }

    public class FirmwareUpdateProgress
    {
        internal FirmwareUpdateProgress() { }

        [Newtonsoft.Json.JsonProperty]
        public int sentFragments { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int totalFragments { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public decimal progress { get; internal set; }
    }

    public class ControllerFirmwareUpdateResultArgs
    {
        internal ControllerFirmwareUpdateResultArgs() { }

        [Newtonsoft.Json.JsonProperty]
        public bool success { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public Enums.ControllerFirmwareUpdateStatus status { get; internal  set; }
    }


    public class NodeFirmwareUpdateResultArgs
    {
        internal NodeFirmwareUpdateResultArgs() { }

        [Newtonsoft.Json.JsonProperty]
        public Enums.NodeFirmwareUpdateStatus status { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool success { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int? waitTime { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool reInterview { get; internal set; }
    }

    public class ControllerFirmwareUpdateProgressArgs : FirmwareUpdateProgress
    {
        internal ControllerFirmwareUpdateProgressArgs() { }
    }

    public class NodeFirmwareUpdateProgressArgs : FirmwareUpdateProgress
    {
        internal NodeFirmwareUpdateProgressArgs() { }

        [Newtonsoft.Json.JsonProperty]
        public int currentFile { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public  int totalFiles { get; internal set; }
    }

    public class SmartStartProvisioningEntry
    {
        internal SmartStartProvisioningEntry() { }

        [Newtonsoft.Json.JsonProperty]
        public string dsk { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int[] securityClasses { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int version { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int[] requestedSecurityClasses { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int genericDeviceClass { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int specificDeviceClass { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int installerIconType { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int manufacturerId { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int productType { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int productId { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public decimal applicationVersion { get; internal set; }
    }

    public class RebuildRoutesOptions
    {
        public bool includeSleeping { get; set; }
    }


    public class AssociationAddress
    {
        public int nodeId { get; set; }
        public int? endpoint { get;  set; }
    }

    public class RebuildRouteStats
    {
        internal RebuildRouteStats() { }

        public int[] HealedNodes { get; internal set; }
        public int[] SkippedNodes { get; internal set; }
        public int[] FailedNodes { get; internal set; }
    }

    public class RebuildRoutesDoneArgs : RebuildRouteStats
    {
        internal RebuildRoutesDoneArgs() { }
    }

    public class RebuildRoutesProgressArgs : RebuildRouteStats
    {
        internal RebuildRoutesProgressArgs() { }

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
        public int? @default { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int? min { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int? max { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int? steps { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string unit { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public Dictionary<string, string> states { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public Dictionary<string, object> ccSpecific { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int? valueSize { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public ConfigValueFormat? format { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool? allowManualEntry { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool? isFromConfig { get; internal set; }
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
        [Newtonsoft.Json.JsonProperty]
        public Enums.SecurityBootstrapFailure lowSecurityReason { get; internal set; }
    }

    public delegate string ValidateDSKAndEnterPIN(string dsk);
    public delegate InclusionGrant GrantSecurityClasses(InclusionGrant requested);
    public delegate void Abort();

    public class ExclusionOptions
    {
        public Enums.ExclusionStrategy strategy { get; set; }
    }

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
        public object prevValue { get; internal set; }
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

    public class FirmwareUpdate
    {
        public static FirmwareUpdate Create(string Filename)
        {
            FirmwareUpdate U = new FirmwareUpdate();
            U.data = File.ReadAllBytes(Filename);
            U.filename = new FileInfo(Filename).Name;

            return U;
        }


        public static FirmwareUpdate Create(string Filename, int Target)
        {
            FirmwareUpdate U = new FirmwareUpdate();
            U.firmwareTarget = Target;
            U.data = File.ReadAllBytes(Filename);
            U.filename = new FileInfo(Filename).Name;

            return U;
        }

        internal FirmwareUpdate() { }
        
        [Newtonsoft.Json.JsonProperty(PropertyName = "file")]
        public byte[] data { get; internal set; }
        public string filename { get; internal set; }
        public int? firmwareTarget { get; internal set; }
    }
}
