using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace ZWaveJS.NET
{

    internal class ZWJSSJsonConverter : JsonConverter
    {
        private Driver _driver;

        public ZWJSSJsonConverter(Driver driver) :
            base()
        {
            _driver = driver;
        }
        public override bool CanConvert(Type objectType)
        {
            return typeof(ZWaveNode).IsAssignableFrom(objectType) ||
                   typeof(Endpoint).IsAssignableFrom(objectType) ||
                   typeof(Controller).IsAssignableFrom(objectType);
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                         object existingValue,
                                         JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            object target = Activator.CreateInstance(objectType, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, 
                null, new object[] { _driver }, null);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
