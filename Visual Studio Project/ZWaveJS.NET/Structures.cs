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

    public class ValueDump
    {
        internal ValueDump() { }

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
        internal ValueMetaData() { }

        public string type { get; set; }
        public bool readable { get; set; }
        public bool writeable { get; set; }
        public string label { get; set; }
        public string description { get; set; }
        public int @default { get; set; }
        public int min { get; set; }
        public int max { get; set; }
        public Dictionary<string, string> states { get; set; }
    }

    public class CommandClass
    {
        internal CommandClass() { }

        public int id { get; set; }
        public string name { get; set; }
        public int version { get; set; }
        public bool isSecure { get; set; }
    }

    public class DeviceClass
    {
        internal DeviceClass() { }

        public DeviceClassType basic { get; set; }
        public DeviceClassType generic { get; set; }
        public DeviceClassType specific { get; set; }
        public int[] mandatorySupportedCCs { get; set; }
        public int[] mandatoryControlledCCs { get; set; }
    }
    public class DeviceClassType
    {
        internal DeviceClassType() { }

        public int key { get; set; }
        public string label { get; set; }
    }

    public class DeviceConfig
    {
        internal DeviceConfig() { }

        public string filename { get; set; }
        public bool isEmbedded { get; set; }
        public string manufacturer { get; set; }
        public string label { get; set; }
        public string description { get; set; }
        public Device[] devices { get; set; }
        public FirmwareVersion firmwareVersion { get; set; }
    }

    public class FirmwareVersion
    {
        internal FirmwareVersion() { }

        public string min { get; set; }
        public string max { get; set; }
    }

    public class Device
    {
        internal Device() { }

        public int productType { get; set; }
        public int productId { get; set; }
    }

    public class ValueID
    {
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
