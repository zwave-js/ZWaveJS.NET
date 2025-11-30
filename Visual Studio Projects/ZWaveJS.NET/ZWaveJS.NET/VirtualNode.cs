using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ZWaveJS.NET
{
    public class VirtualNode
    {
        internal VirtualNode(Driver driver, int[] Nodes)
        {
            _driver = driver;
            this.Nodes = Nodes;
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> GetEndpointCount()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Res.SetPayload(JO.SelectToken("result.count").ToObject<int>());
                }
                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.MCGetEndpointCount);
            Request.Add("nodeIDs", this.Nodes);

            string RequestPL = JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> SetValue(ValueID ValueID, object Value, SetValueAPIOptions Options = null)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Res.SetPayload(JO.SelectToken("result.result").ToObject<SetValueResult>());
                }
                
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.MCSetValue);
            Request.Add("nodeIDs", this.Nodes);
            Request.Add("valueId", ValueID);
            Request.Add("value", Value);

            if (Options != null)
            {
                Request.Add("options", Options);
            }

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> GetDefinedValueIDs()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Res.SetPayload(JO.SelectToken("result.valueIDs").ToObject<ValueID[]>());
                }

                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.MCGetDefinedValueIDs);
            Request.Add("nodeIDs", this.Nodes);


            string RequestPL = JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> SupportsCCAPI(int CommandClass)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Res.SetPayload(JO.SelectToken("result.supported").ToObject<bool>());

                }
                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.MCSupportsCCAPI);
            Request.Add("nodeIDs", this.Nodes);
            Request.Add("commandClass", CommandClass);

            string RequestPL = JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }
        
        // Checked as of : 3.5.0
        public Task<CMDResult> InvokeCCAPI(int CommandClass, string Method, params object[] Params)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Res.SetPayload(JO.SelectToken("result").ToObject<JObject>());
                }
                Result.SetResult(Res);

            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.MCInvokeCCAPI);
            Request.Add("nodeIDs", this.Nodes);
            Request.Add("commandClass", CommandClass);
            Request.Add("methodName", Method);
            Request.Add("args", Params);

            string RequestPL = JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        private int[] Nodes { get; set; }
        private Driver _driver { get; set; }
    }

    
}
