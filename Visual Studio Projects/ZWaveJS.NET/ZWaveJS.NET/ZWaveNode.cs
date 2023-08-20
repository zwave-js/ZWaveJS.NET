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

        public delegate void StatisticsUpdatedEvent(ZWaveNode Node, NodeStatisticsUpdatedArgs Args);
        public event StatisticsUpdatedEvent StatisticsUpdated;
        internal void Trigger_StatisticsUpdated(NodeStatisticsUpdatedArgs Args)
        {
            this.statistics = Args;
            StatisticsUpdated?.Invoke(this, Args);
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

        public delegate void ValueNotificationEvent(ZWaveNode Node, ValueNotificationArgs Args);
        public event ValueNotificationEvent ValueNotification;
        internal void Trigger_ValueNotification(ValueNotificationArgs Args)
        {
            ValueNotification?.Invoke(this, Args);
        }

        public delegate void ValueUpdatedEvent(ZWaveNode Node, ValueUpdatedArgs Args);
        public event ValueUpdatedEvent ValueUpdated;
        internal void Trigger_ValueUpdated(ValueUpdatedArgs Args)
        {
            ValueUpdated?.Invoke(this, Args);
        }

        public delegate void ValueAddedEvent(ZWaveNode Node, ValueAddedArgs Args);
        public event ValueAddedEvent ValueAdded;
        internal void Trigger_ValueAdded(ValueAddedArgs Args)
        {
            ValueAdded?.Invoke(this, Args);
        }

        public delegate void ValueRemovedEvent(ZWaveNode Node, ValueRemovedArgs Args);
        public event ValueRemovedEvent ValueRemoved;
        internal void Trigger_ValueRemoved(ValueRemovedArgs Args)
        {
            ValueRemoved?.Invoke(this, Args);
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

        public Task<CMDResult> Interview()
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
            Request.Add("command", Enums.Commands.Interview);
            Request.Add("nodeId", this.id);


            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }
        
        public Task<CMDResult> CheckLifelineHealth(int Rounds, LifelineHealthCheckProgress OnProgress = null)
        {
            LifelineHealthCheckProgressSub = OnProgress;

            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    LifelineHealthCheckSummary LLHCS = JsonConvert.DeserializeObject<LifelineHealthCheckSummary>(JO.SelectToken("result.summary").ToString());
                    Res.SetPayload(LLHCS);
                }
                
                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.CheckLifelineHealth);
            Request.Add("nodeId", this.id);
            Request.Add("rounds", Rounds);


            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> AbortFirmwareUpdate()
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
            Request.Add("command", Enums.Commands.AbortFirmwareUpdate);
            Request.Add("nodeId", this.id);
          

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> BeginFirmwareUpdate(string FileName, int Target = -1, string FirmwareFileFormat = null)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                Result.SetResult(Res);
            });

            FileInfo FI = new FileInfo(FileName);
            byte[] FileData = File.ReadAllBytes(FileName);

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.BeginFirmwareUpdate);
            Request.Add("nodeId", this.id);
            Request.Add("firmwareFile", FileData);
            Request.Add("firmwareFilename", FI.Name);

            if(Target > -1)
                Request.Add("target", Target);
            
            if (!string.IsNullOrEmpty(FirmwareFileFormat))
                Request.Add("firmwareFileFormat", FirmwareFileFormat);
           
            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> RefreshInfo(RefreshInfoOptions Options = null)
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
            Request.Add("command", Enums.Commands.RefreshInfo);
            Request.Add("nodeId", this.id);

            if(Options != null)
                Request.Add("options", Options);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> GetValue(ValueID ValueID)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Res.SetPayload(JO.Value<JObject>("result"));
                }

                Result.SetResult(Res);

            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.GetValue);
            Request.Add("valueId", ValueID);
            Request.Add("nodeId", this.id);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
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
            Request.Add("command", Enums.Commands.SetValue);
            Request.Add("nodeId", this.id);
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

        public Task<CMDResult> PollValue(ValueID ValueID)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Res.SetPayload(JO.Value<JObject>("result"));
                }

                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.PollValue);
            Request.Add("nodeId", this.id);
            Request.Add("valueId", ValueID);

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
                    Res.SetPayload(JsonConvert.DeserializeObject<ValueID[]>(JO.SelectToken("result.valueIds").ToString()));
                }

                Result.SetResult(Res);



            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.GetDefinedValueIDs);
            Request.Add("nodeId", this.id);


            string RequestPL = JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> GetValueMetadata(ValueID VID)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Res.SetPayload(JsonConvert.DeserializeObject<ValueMetadata>(JO.SelectToken("result").ToString()));
                }

                Result.SetResult(Res);

            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.GetValueMetadata);
            Request.Add("nodeId", this.id);
            Request.Add("valueId", VID);


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
            Request.Add("command", Enums.Commands.SupportsCCAPI);
            Request.Add("nodeId", this.id);
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
            Request.Add("command", Enums.Commands.InvokeCCAPI);
            Request.Add("nodeId", this.id);
            Request.Add("commandClass", CommandClass);
            Request.Add("methodName", Method);
            Request.Add("args", Params);

            string RequestPL = JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

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
            Request.Add("command", Enums.Commands.GetEndpointCount);
            Request.Add("nodeId", this.id);

            string RequestPL = JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> GetHighestSecurityClass()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    int Value = JO.SelectToken("result.highestSecurityClass").Value<int>();
                    Res.SetPayload((Enums.SecurityClass)Value);
                }
                Result.SetResult(Res);

               
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.GetHighestSecurityClass);
            Request.Add("nodeId", this.id);

            string RequestPL = JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> HasSecurityClass(Enums.SecurityClass Class)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Res.SetPayload(JO.SelectToken("result.hasSecurityClass").Value<bool>());
                }
                Result.SetResult(Res);
              
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.HasSecurityClass);
            Request.Add("nodeId", this.id);
            Request.Add("securityClass", Class);

            string RequestPL = JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // FIX ME
        public Task<CMDResult> WaitForWakeup()
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
            Request.Add("command", Enums.Commands.WaitForWakeUp);
            Request.Add("nodeId", this.id);

            string RequestPL = JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

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
                if (Driver.Instance.Inited)
                {
                    Guid ID = Guid.NewGuid();

                    Dictionary<string, object> Request = new Dictionary<string, object>();
                    Request.Add("messageId", ID);
                    Request.Add("command", Enums.Commands.KeepNodeAwake);
                    Request.Add("nodeId", this.id);
                    Request.Add("keepAwake", _KeepAwake);

                    string RequestPL = JsonConvert.SerializeObject(Request);
                    Driver.Instance.ClientWebSocket.SendInstant(RequestPL);
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
                if (Driver.Instance.Inited)
                {
                    Guid ID = Guid.NewGuid();

                    Dictionary<string, object> Request = new Dictionary<string, object>();
                    Request.Add("messageId", ID);
                    Request.Add("command", Enums.Commands.SetName);
                    Request.Add("nodeId", this.id);
                    Request.Add("name", _Name);

                    string RequestPL = JsonConvert.SerializeObject(Request);
                    Driver.Instance.ClientWebSocket.SendInstant(RequestPL);
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
                if (Driver.Instance.Inited)
                {
                    
                    Guid ID = Guid.NewGuid();

                    Dictionary<string, object> Request = new Dictionary<string, object>();
                    Request.Add("messageId", ID);
                    Request.Add("command", Enums.Commands.SetLocation);
                    Request.Add("nodeId", this.id);
                    Request.Add("location", _Location);

                    string RequestPL = JsonConvert.SerializeObject(Request);
                    Driver.Instance.ClientWebSocket.SendInstant(RequestPL);
                }
            }
        }
    }
}
