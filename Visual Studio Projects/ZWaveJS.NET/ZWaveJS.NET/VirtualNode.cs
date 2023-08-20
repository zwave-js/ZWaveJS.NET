using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ZWaveJS.NET
{
    public class VirtualNode
    {

        internal VirtualNode(int[] Nodes)
        {
            this.Nodes = Nodes;
        }

        public Task<CMDResult> GetEndpointCount()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Res.SetPayload(JO.SelectToken("result.count").Value<int>());
                }
                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.MCGetEndpointCount);
            Request.Add("nodeIDs", this.Nodes);

            string RequestPL = JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> SetValue(ValueID ValueID, object Value, SetValueAPIOptions Options = null)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                Result.SetResult(Res);
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
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> GetDefinedValueIDs()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Res.SetPayload(JsonConvert.DeserializeObject<ValueID[]>(JO.SelectToken("result.valueIDs").ToString()));
                }

                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.MCGetDefinedValueIDs);
            Request.Add("nodeIDs", this.Nodes);


            string RequestPL = JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> SupportsCCAPI(int CommandClass)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Res.SetPayload(JO.SelectToken("result.supported").Value<bool>());

                }
                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.MCSupportsCCAPI);
            Request.Add("nodeIDs", this.Nodes);
            Request.Add("commandClass", CommandClass);

            string RequestPL = JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> InvokeCCAPI(int CommandClass, string Method, params object[] Params)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Res.SetPayload(JsonConvert.DeserializeObject<JObject>(JO.SelectToken("result").ToString()));
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
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        private int[] Nodes { get; set; }
    }

    
}
