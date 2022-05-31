using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
namespace ZWaveJS.NET
{
    public class ZWaveNode
    {
        internal ZWaveNode()
        {

        }

        public delegate void LifelineHealthCheckProgress(int Round, int TotalRounds, int LastRating);
        private LifelineHealthCheckProgress LifelineHealthCheckProgressSub;
        internal void Trigger_LifelineHealthCheckProgress(int Round, int TotalRounds, int LastRating)
        {
            LifelineHealthCheckProgressSub?.Invoke(Round, TotalRounds, LastRating);
        }

        public delegate void StatisticsUpdatedEvent(ZWaveNode Node, NodeStatistics Statistics);
        public event StatisticsUpdatedEvent StatisticsUpdated;
        internal void Trigger_StatisticsUpdated(NodeStatistics Statistics)
        {
            this.statistics = Statistics;
            StatisticsUpdated?.Invoke(this, Statistics);
        }

        public delegate void FirmwareUpdateFinishedEvent(ZWaveNode Node, int Status, int WaitTime);
        public event FirmwareUpdateFinishedEvent FirmwareUpdateFinished;
        internal void Trigger_FirmwareUpdateFinished(int Status, int Time)
        {
            FirmwareUpdateFinished?.Invoke(this, Status, Time);
        }

        public delegate void FirmwareUpdateProgressEvent(ZWaveNode Node, int SentFragments, int TotalFragments);
        public event FirmwareUpdateProgressEvent FirmwareUpdateProgress;
        internal void Trigger_FirmwareUpdateProgress(int SentFragments, int TotalFragments)
        {
            FirmwareUpdateProgress?.Invoke(this, SentFragments, TotalFragments);
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

        public delegate void NodeDeadEvent(ZWaveNode Node);
        public event NodeDeadEvent NodeDead;
        internal void Trigger_NodeDead()
        {
            this.status = Enums.NodeStatus.Dead;
            NodeDead?.Invoke(this);
        }

        public delegate void AwakeEvent(ZWaveNode Node);
        public event AwakeEvent NodeAwake;
        internal void Trigger_NodeAwake()
        {
            this.status = Enums.NodeStatus.Awake;
            NodeAwake?.Invoke(this);
        }

        public delegate void SleepEvent(ZWaveNode Node);
        public event SleepEvent NodeAsleep;
        internal void Trigger_NodeAsleep()
        {
            this.status = Enums.NodeStatus.Asleep;
            NodeAsleep?.Invoke(this);
        }

        public delegate void NodeReadyEvent(ZWaveNode Node);
        public event NodeReadyEvent NodeReady;
        internal void Trigger_NodeReady()
        {
            this.ready = true;
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

        public delegate void NodeInterviewFailedEvent(ZWaveNode Node, NodeInterviewFailedEventArgs Args);
        public event NodeInterviewFailedEvent NodeInterviewFailed;
        internal void Trigger_NodeInterviewFailed(NodeInterviewFailedEventArgs Args)
        {
            NodeInterviewFailed?.Invoke(this, Args);
        }

        public Task<LifelineHealthCheckSummary> CheckLifelineHealth(int Rounds, LifelineHealthCheckProgress OnProgress = null)
        {
            LifelineHealthCheckProgressSub = OnProgress;

            Guid ID = Guid.NewGuid();

            TaskCompletionSource<LifelineHealthCheckSummary> Result = new TaskCompletionSource<LifelineHealthCheckSummary>();
            Driver.Callbacks.Add(ID, (JO) =>
            {
                LifelineHealthCheckSummary LLHCS =  JsonConvert.DeserializeObject<LifelineHealthCheckSummary>(JO.SelectToken("result.summary").ToString());
                Result.SetResult(LLHCS);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.CheckLifelineHealth);
            Request.Add("nodeId", this.id);
            Request.Add("rounds", Rounds);


            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<bool> AbortFirmwareUpdate()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();
            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.AbortFirmwareUpdate);
            Request.Add("nodeId", this.id);
          

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<bool> BeginFirmwareUpdate(string FileName)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();
            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JO.Value<bool>("success"));
            });

            FileInfo FI = new FileInfo(FileName);
            byte[] FileData = File.ReadAllBytes(FileName);

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.BeginFirmwareUpdate);
            Request.Add("nodeId", this.id);
            Request.Add("firmwareFile", FileData);
            Request.Add("firmwareFilename", FI.Name);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
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
            Request.Add("nodeId", this.id);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<JObject> GetValue(ValueID ValueID)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<JObject> Result = new TaskCompletionSource<JObject>();
            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JO.Value<JObject>("result"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.GetValue);
            Request.Add("valueId", ValueID);
            Request.Add("nodeId", this.id);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<bool> SetValue(ValueID ValueID, object Value, SetValueAPIOptions Options = null)
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
            Request.Add("nodeId", this.id);
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
            Request.Add("nodeId", this.id);
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
            Request.Add("nodeId", this.id);


            string RequestPL = JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<ValueMetadata> GetValueMetadata(ValueID VID)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<ValueMetadata> Result = new TaskCompletionSource<ValueMetadata>();
            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JsonConvert.DeserializeObject<ValueMetadata>(JO.SelectToken("result").ToString()));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.GetValueMetadata);
            Request.Add("nodeId", this.id);
            Request.Add("valueId", VID);


            string RequestPL = JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<bool> SupportsCCAPI(int CommandClass)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();
            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JO.SelectToken("result.supported").Value<bool>());
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.SupportsCCAPI);
            Request.Add("nodeId", this.id);
            Request.Add("commandClass", CommandClass);

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
            Request.Add("nodeId", this.id);
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

        public Endpoint[] GetAllEndpoints()
        {
            return endpoints;
        }

        public Task<int> GetEndpointCount()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<int> Result = new TaskCompletionSource<int>();

            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JO.SelectToken("result.count").Value<int>()) ;
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.GetEndpointCount);
            Request.Add("nodeId", this.id);

            string RequestPL = JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<Enums.SecurityClass> GetHighestSecurityClass()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<Enums.SecurityClass> Result = new TaskCompletionSource<Enums.SecurityClass>();

            Driver.Callbacks.Add(ID, (JO) =>
            {
                int Value = JO.SelectToken("result.highestSecurityClass").Value<int>();
                Result.SetResult((Enums.SecurityClass)Value);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.GetHighestSecurityClass);
            Request.Add("nodeId", this.id);

            string RequestPL = JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<bool> HasSecurityClass(Enums.SecurityClass Class)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();

            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JO.SelectToken("result.hasSecurityClass").Value<bool>());
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.HasSecurityClass);
            Request.Add("nodeId", this.id);
            Request.Add("securityClass", Class);

            string RequestPL = JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

       

        [Newtonsoft.Json.JsonProperty]
        internal Endpoint[] endpoints { get; set; }

        [Newtonsoft.Json.JsonProperty]
        public bool isControllerNode { get; internal set; }

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
        public int zwavePlusNodeType { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int zwavePlusRoleType { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public DeviceClass deviceClass { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string interviewStage { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string deviceDatabaseUrl { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int interviewAttempts { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string label { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int nodeType { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public CommandClass[] commandClasses { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public NodeStatistics statistics { get; internal set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "nodeId")]
        public int id { get; internal set; }

        private bool _KeepAwake;
        [Newtonsoft.Json.JsonProperty]
        public bool keepAwake
        {
            get
            {
                return _KeepAwake;
            }
            set
            {
                _KeepAwake = value;
                if (Driver.Inited)
                {
                    Guid ID = Guid.NewGuid();

                    Dictionary<string, object> Request = new Dictionary<string, object>();
                    Request.Add("messageId", ID);
                    Request.Add("command", Enums.Commands.KeepNodeAwake);
                    Request.Add("nodeId", this.id);
                    Request.Add("keepAwake", _KeepAwake);

                    string RequestPL = JsonConvert.SerializeObject(Request);
                    Driver.Client.Send(RequestPL);
                }
            }
        }

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
                if (Driver.Inited)
                {
                    Guid ID = Guid.NewGuid();

                    Dictionary<string, object> Request = new Dictionary<string, object>();
                    Request.Add("messageId", ID);
                    Request.Add("command", Enums.Commands.SetName);
                    Request.Add("nodeId", this.id);
                    Request.Add("name", _Name);

                    string RequestPL = JsonConvert.SerializeObject(Request);
                    Driver.Client.Send(RequestPL);
                }
              
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
                if (Driver.Inited)
                {
                    
                    Guid ID = Guid.NewGuid();

                    Dictionary<string, object> Request = new Dictionary<string, object>();
                    Request.Add("messageId", ID);
                    Request.Add("command", Enums.Commands.SetLocation);
                    Request.Add("nodeId", this.id);
                    Request.Add("location", _Location);

                    string RequestPL = JsonConvert.SerializeObject(Request);
                    Driver.Client.Send(RequestPL);
                }
            }
        }
    }
}
