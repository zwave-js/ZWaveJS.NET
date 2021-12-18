using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace ZWaveJS.Net
{
    class CustomBooleanJsonConverter : JsonConverter<bool>
    {
        public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if(reader.ValueType == typeof(string) && reader.Value.ToString().Equals("unknown"))
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
    
    public class ZWaveNode
    {
      
        public int nodeId { get; set; }
        public int index { get; set; }
        public int installerIcon { get; set; }
        public int userIcon { get; set; }
        public Enums.NodeStatus status { get; set; }
        public bool ready { get; set; }
        public bool isListening { get; set; }
        public bool isRouting { get; set; }
        public bool isSecure { get; set; }
        public int manufacturerId { get; set; }
        public int productId { get; set; }
        public int productType { get; set; }
        public string firmwareVersion { get; set; }
        public string zwavePlusVersion { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public DeviceConfig deviceConfig { get; set; }
        public string label { get; set; }
        public int interviewAttempts { get; set; }
        public Endpoint[] endpoints { get; set; }
        public object isFrequentListening { get; set; }
        public long maxDataRate { get; set; }
        public long[] supportedDataRates { get; set; }
        public int protocolVersion { get; set; }
        public bool supportsBeaming { get; set; }
        public bool supportsSecurity { get; set; }
        public int nodeType { get; set; }
        public int zwavePlusNodeType { get; set; }
        public int zwavePlusRoleType { get; set; }
        public DeviceClass deviceClass { get; set; }
        public CommandClass[] commandClasses { get; set; }
        public string interviewStage { get; set; }
        public string deviceDatabaseUrl { get; set; }
        public int highestSecurityClass { get; set; }
        public ValueDump[] values { get; set; }
    }

    public class ValueDump
    {
        public int commandClass { get; set; }
        public int endpoint { get; set; }
        public object property { get; set; }
        public object propertyKey { get; set; }
        public string commandClassName { get; set; }
        public string propertyName { get; set; }
        public string propertyKeyName { get; set; }
        public int ccVersion { get; set; }
        public ValueMetaData metadata { get; set; }
        public object value { get; set; }
    }

    public class ValueMetaData
    {
        public string type { get; set; }
        public bool readable { get; set; }
        public bool writeable { get; set; }
        public string label { get; set; }
        public string description { get; set; }
        public int @default {get;set;}
        public int min { get; set; }
        public int max { get; set; }
        public Dictionary<string, string> states { get; set; }

    }

    public class CommandClass
    {
        public int id { get; set; }
        public string name { get; set; }
        public int version { get; set; }
        public bool isSecure { get; set; }
    }

    public class Endpoint
    {
        public int nodeId { get; set; }
        public int index { get; set; }
        public int installerIcon { get; set; }
        public int userIcon { get; set; }
        public DeviceClass deviceClass { get; set; }
    }

    public class DeviceClass
    {
        public DeviceClassType basic { get; set; }
        public DeviceClassType generic { get; set; }
        public DeviceClassType specific { get; set; }
        public int[] mandatorySupportedCCs { get; set; }
        public int[] mandatoryControlledCCs { get; set; }
    }
    public class DeviceClassType
    {
        public int key { get; set; }
        public string label { get; set; }
    }

    public class DeviceConfig
    {
        public string filename { get; set; }
        public bool isEmbedded { get; set; }
        public string manufacturer { get; set; }
        public string label { get; set; }
        public string description { get; set; }
        public Device[] devices { get; set; }
        public FirmwareVersion firmwareVersion {get;set;}
    }

    public class FirmwareVersion
    {
        public string min { get; set; }
        public string max { get; set; }
    }

    public class Device
    {
        public int productType { get; set; }
        public int productId { get; set; }
    }

    public class Controller
    {
        public string libraryVersion { get; set; }
        public int type { get; set; }
        public long homeId { get; set; }
        public int ownNodeId { get; set; }
        public bool isSecondary { get; set; }
        public bool isUsingHomeIdFromOtherNetwork { get; set; }
        public bool isSISPresent { get; set; }
        public bool wasRealPrimary { get; set; }
        public bool isStaticUpdateController { get; set; }
        public bool isSlave { get; set; }
        public string serialApiVersion { get; set; }
        public int manufacturerId { get; set; }
        public int productType { get; set; }
        public int productId { get; set; }
        public int[] supportedFunctionTypes { get; set; }
        public int sucNodeId { get; set; }
        public bool supportsTimers { get; set; }
        public bool isHealNetworkActive { get; set; }
       
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

    public class SetValueOptions
    {
        public string transitionDuration { get; set; }
        public int volume { get; set; }
    }




}
