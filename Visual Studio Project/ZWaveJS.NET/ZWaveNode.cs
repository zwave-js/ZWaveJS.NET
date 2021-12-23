using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
namespace ZWaveJS.NET
{
    public class ZWaveNode
    {
        internal ZWaveNode()
        {

        }

        public delegate void ValueNotificationEvent(ZWaveNode Node, JObject Args);
        public event ValueNotificationEvent ValueNotification;
        internal void Trigger_ValueNotification(JObject Args)
        {
            ValueNotification?.Invoke(this, Args);
        }

        public delegate void ValueUpdatedEvent(ZWaveNode Node, JObject Args);
        public event ValueUpdatedEvent ValueUpdated;
        internal void Trigger_ValueUpdated(JObject Args)
        {
            ValueUpdated?.Invoke(this, Args);
        }

        public delegate void NotificationEvent(ZWaveNode Node, int ccId, JObject Args);
        public event NotificationEvent Notification;
        internal void Trigger_Notification(int CCID, JObject Args)
        {
            Notification?.Invoke(this, CCID, Args);
        }

        public delegate void AwakeEvent(ZWaveNode Node);
        public event AwakeEvent NodeAwake;
        internal void Trigger_NodeAwake()
        {
            NodeAwake?.Invoke(this);
        }

        public delegate void SleepEvent(ZWaveNode Node);
        public event SleepEvent NodeAsleep;
        internal void Trigger_NodeAsleep()
        {
            NodeAsleep?.Invoke(this);
        }

        public delegate void NodeReadyEvent(ZWaveNode Node);
        public event NodeReadyEvent NodeReady;
        internal void Trigger_NodeReady()
        {
            NodeReady?.Invoke(this);
        }

        public delegate void NodeInterviewStartedEvent(ZWaveNode Node);
        public event NodeInterviewStartedEvent NodeInterviewStarted;
        internal void Trigger_NodeInterviewStarted()
        {
            NodeInterviewStarted?.Invoke(this);
        }

        public delegate void NodeInterviewCompletedEvent(ZWaveNode Node);
        public event NodeInterviewCompletedEvent NodeInterviewCompleted;
        internal void Trigger_NodeInterviewCompleted()
        {
            NodeInterviewCompleted?.Invoke(this);
        }

        public delegate void NodeInterviewFailedEvent(ZWaveNode Node, JObject Args);
        public event NodeInterviewFailedEvent NodeInterviewFailed;
        internal void Trigger_NodeInterviewFailed(JObject Args)
        {
            NodeInterviewFailed?.Invoke(this, Args);
        }

        public Task<bool> RefreshInfo()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();
            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(true);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.RefreshInfo);
            Request.Add("nodeId", this.nodeId);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<bool> SetValue(ValueID ValueID, object Value, SetValueOptions Options = null)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();
            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.SetValue);
            Request.Add("nodeId", this.nodeId);
            Request.Add("valueId", ValueID);
            Request.Add("value", Value);

            if (Options != null)
            {
                Request.Add("options", Options);
            }

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<JObject> PollValue(ValueID ValueID)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<JObject> Result = new TaskCompletionSource<JObject>();
            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JO.Value<JObject>("result"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.PollValue);
            Request.Add("nodeId", this.nodeId);
            Request.Add("valueId", ValueID);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<ValueID[]> GetDefinedValueIDs()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<ValueID[]> Result = new TaskCompletionSource<ValueID[]>();
            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JsonConvert.DeserializeObject<ValueID[]>(JO.SelectToken("result.valueIds").ToString()));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.GetDefinedValueIDs);
            Request.Add("nodeId", this.nodeId);


            string RequestPL = JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<ValueMetaData> GetValueMetadata(ValueID VID)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<ValueMetaData> Result = new TaskCompletionSource<ValueMetaData>();
            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JsonConvert.DeserializeObject<ValueMetaData>(JO.SelectToken("result").ToString()));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.GetValueMetadata);
            Request.Add("nodeId", this.nodeId);
            Request.Add("valueId", VID);


            string RequestPL = JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<JObject> InvokeCCAPI(int CommandClass, string Method, params object[] Params)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<JObject> Result = new TaskCompletionSource<JObject>();
            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JsonConvert.DeserializeObject<JObject>(JO.SelectToken("result").ToString()));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.InvokeCCAPI);
            Request.Add("nodeId", this.nodeId);
            Request.Add("commandClass", CommandClass);
            Request.Add("methodName", Method);
            Request.Add("args", Params);


            string RequestPL = JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Endpoint GetEndpoint(int Index)
        {
            Endpoint EP = this.endpoints.FirstOrDefault((E) => E.index.Equals(Index));
            return EP;
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
        public Enums.NodeStatus status { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool ready { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool isListening { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool isRouting { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool isSecure { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int manufacturerId { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int productId { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int productType { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string firmwareVersion { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string zwavePlusVersion { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public DeviceConfig deviceConfig { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string label { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int interviewAttempts { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        internal Endpoint[] endpoints { get; set; }
        [Newtonsoft.Json.JsonProperty]
        public object isFrequentListening { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public long maxDataRate { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public long[] supportedDataRates { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int protocolVersion { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool supportsBeaming { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool supportsSecurity { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int nodeType { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int zwavePlusNodeType { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int zwavePlusRoleType { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public DeviceClass deviceClass { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public CommandClass[] commandClasses { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string interviewStage { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string deviceDatabaseUrl { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int highestSecurityClass { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public ValueDump[] values { get; internal set; }

        private string _Name;
        [Newtonsoft.Json.JsonProperty]
        public string name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                Guid ID = Guid.NewGuid();

                Dictionary<string, object> Request = new Dictionary<string, object>();
                Request.Add("messageId", ID);
                Request.Add("command", Enums.Commands.SetName);
                Request.Add("nodeId", this.nodeId);
                Request.Add("name", _Name);

                string RequestPL = JsonConvert.SerializeObject(Request);
                Driver.Client.Send(RequestPL);
            }
        }

        private string _Location;
        [Newtonsoft.Json.JsonProperty]
        public string location
        {
            get
            {
                return _Location;
            }
            set
            {
                _Location = value;
                Guid ID = Guid.NewGuid();

                Dictionary<string, object> Request = new Dictionary<string, object>();
                Request.Add("messageId", ID);
                Request.Add("command", Enums.Commands.SetLocation);
                Request.Add("nodeId", this.nodeId);
                Request.Add("location", _Location);

                string RequestPL = JsonConvert.SerializeObject(Request);
                Driver.Client.Send(RequestPL);
            }
        }
    }
}
