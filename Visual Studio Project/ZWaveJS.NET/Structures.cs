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

    public class InclusionGrant
    {
        public Enums.SecurityClass[] securityClasses { get; set; }
        public bool clientSideAuth { get; set; }
    }

    public class ValueMetaData
    {
        internal ValueMetaData() { }

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

    public class SetValueOptions
    {
        public string transitionDuration { get; set; }
        public int volume { get; set; }
    }
}
