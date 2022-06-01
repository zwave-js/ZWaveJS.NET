using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZWaveJS.NET
{
    public class Endpoint
    {
        internal Endpoint() { }


        public Task<CMDResult> SupportsCCAPI(int CommandClass)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            Driver.Callbacks.Add(ID, (JO) =>
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
            Request.Add("command", Enums.Commands.SupportsCCAPI);
            Request.Add("nodeId", this.nodeId);
            Request.Add("endpoint", this.index);
            Request.Add("commandClass", CommandClass);

            string RequestPL = JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> InvokeCCAPI(int CommandClass, string Method, params object[] Params)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            Driver.Callbacks.Add(ID, (JO) =>
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
            Request.Add("command", Enums.Commands.InvokeCCAPI);
            Request.Add("nodeId", this.nodeId);
            Request.Add("endpoint", this.index);
            Request.Add("commandClass", CommandClass);
            Request.Add("methodName", Method);
            Request.Add("args", Params);


            string RequestPL = JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        [Newtonsoft.Json.JsonProperty]
        public int nodeId { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int index { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int installerIcon { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int userIcon { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public DeviceClass deviceClass { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public CommandClass[] commandClasses { get; internal set; }
    }
}
