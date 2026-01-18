using System;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Reflection;
using System.Collections.Generic;

namespace ZWaveJS.NET
{
    public class ConfigManager
    {
        private Driver _driver;
        internal ConfigManager(Driver Driver)
        {
            _driver = Driver;

            Guid ID = Guid.Empty;
            string RequestPL = string.Empty;
            Dictionary<string, object> Request;
            
            Request = new Dictionary<string, object>();
            ID = Guid.NewGuid();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.LoadManufacturers);
            RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);
            
            Request = new Dictionary<string, object>();
            ID = Guid.NewGuid();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.LoadDeviceIndex);
            RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);
    
           
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> LookupDevice(int ManufacturerID, int ProductTypeID, int ProductId)
        {
            
            Guid ID = Guid.NewGuid();
            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    DeviceConfig Config = JO.SelectToken("result.config").ToObject<DeviceConfig>();
                    Res.SetPayload(Config);
                }
                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.LookupDevice);
            Request.Add("manufacturerId", ManufacturerID);
            Request.Add("productType", ProductTypeID);
            Request.Add("productId", ProductId);
            
            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> LookupManufacturer(int ManufacturerID)
        {
            Guid ID = Guid.NewGuid();
            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    string Name = JO.SelectToken("result.name").ToObject<string>();
                    Res.SetPayload(Name);
                }
                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.LookupManufacturer);
            Request.Add("manufacturerId", ManufacturerID);
            
            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }
    }
}