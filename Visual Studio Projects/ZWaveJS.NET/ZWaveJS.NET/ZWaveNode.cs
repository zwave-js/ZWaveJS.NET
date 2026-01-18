using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using static ZWaveJS.NET.Enums;

namespace ZWaveJS.NET
{
    public class ZWaveNode
    {
        private Driver _driver;
        internal ZWaveNode(Driver driver = null)
        {
            _driver = driver;
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

        public delegate void FirmwareUpdateFinishedEvent(ZWaveNode Node, NodeFirmwareUpdateResultArgs Args);
        public event FirmwareUpdateFinishedEvent FirmwareUpdateFinished;
        internal void Trigger_FirmwareUpdateFinished(NodeFirmwareUpdateResultArgs Args)
        {
            FirmwareUpdateFinished?.Invoke(this, Args);
        }

        public delegate void FirmwareUpdateProgressEvent(ZWaveNode Node, NodeFirmwareUpdateProgressArgs Args);
        public event FirmwareUpdateProgressEvent FirmwareUpdateProgress;
        internal void Trigger_FirmwareUpdateProgress(NodeFirmwareUpdateProgressArgs Args)
        {
            FirmwareUpdateProgress?.Invoke(this, Args);
        }

        public delegate void ValueNotificationEvent(ZWaveNode Node, ValueNotificationArgs Args);
        public event ValueNotificationEvent ValueNotification;
        internal void Trigger_ValueNotification(ValueNotificationArgs Args)
        {
            ValueNotification?.Invoke(this, Args);
        }

        public delegate void MetadataUpdatedEvent(ZWaveNode Node, MetadataUpdatedArgs Args);
        public event MetadataUpdatedEvent MetadataUpdated;
        internal void Trigger_MetadataUpdated(MetadataUpdatedArgs Args)
        {
            MetadataUpdated?.Invoke(this, Args);
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
        
        public delegate void NodeInfoEvent(ZWaveNode Node);
        public event NodeInfoEvent NodeInfo;
        internal void Trigger_NodeInfo()
        {
            NodeInfo?.Invoke(this);
        }

        public delegate void NodeAliveEvent(ZWaveNode Node);
        public event NodeAliveEvent NodeAlive;
        internal void Trigger_NodeAlive()
        {
            this.status = Enums.NodeStatus.Alive;
            NodeAlive?.Invoke(this);
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
        
        // Checked as of : 3.5.0
        public Task<CMDResult> Ping()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Res.SetPayload(JO.SelectToken("result.responded").ToObject<bool>());
                }
                
                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.Ping);
            Request.Add("nodeId", this.id);


            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> Interview()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.Interview);
            Request.Add("nodeId", this.id);


            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }
        
        // Checked as of : 3.5.0
        public Task<CMDResult> CheckLifelineHealth(int Rounds, LifelineHealthCheckProgress OnProgress = null)
        {
            LifelineHealthCheckProgressSub = OnProgress;

            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    LifelineHealthCheckSummary LLHCS = JO.SelectToken("result.summary").ToObject<LifelineHealthCheckSummary>();
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
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> AbortFirmwareUpdate()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.AbortFirmwareUpdate);
            Request.Add("nodeId", this.id);
          

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> UpdateFirmware(FirmwareUpdate[] Updates)
        {

            foreach(FirmwareUpdate FWU in Updates)
            {
                if(FWU.firmwareTarget == null)
                {
                    TaskCompletionSource<CMDResult> Fail = new TaskCompletionSource<CMDResult>();
                    CMDResult Res = new CMDResult(Enums.ErrorCodes.WrongOverride, "Please use the override that includes 'firmwareTarget'", false);
                    Fail.SetResult(Res);

                    return Fail.Task;
                }
            }

            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Res.SetPayload(JO.SelectToken("result.result").ToObject<NodeFirmwareUpdateResultArgs>());
                }
                
                Result.SetResult(Res);
            });
            
            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.UpdateFirmware);
            Request.Add("nodeId", this.id);
            Request.Add("updates", Updates);
            
            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> RefreshInfo(RefreshInfoOptions Options = null)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            _driver.Callbacks.Add(ID, (JO) =>
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
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> GetValue(ValueID ValueID)
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
            Request.Add("command", Enums.Commands.GetValue);
            Request.Add("valueId", ValueID);
            Request.Add("nodeId", this.id);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
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
                    SetValueResult SVR = JO.SelectToken("result.result").ToObject<SetValueResult>();
                    Res.SetPayload(SVR);
                }
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
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> PollValue(ValueID ValueID)
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
            Request.Add("command", Enums.Commands.PollValue);
            Request.Add("nodeId", this.id);
            Request.Add("valueId", ValueID);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0 - Variant 1: Normal parameter, defined in a config file
        public Task<CMDResult> ZWJSS_SetRawConfigParameterValue(int Parameter, int Value)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.SetRawConfigParameterValue);
            Request.Add("nodeId", this.id);
            Request.Add("parameter", Parameter);
            Request.Add("value", Value);


            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0 - Variant 2: Normal parameter, not defined in a config file
        public Task<CMDResult> ZWJSS_SetRawConfigParameterValue(int Parameter, int Value, int ValueSize, Enums.ConfigValueFormat ValueFormat)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.SetRawConfigParameterValue);
            Request.Add("nodeId", this.id);
            Request.Add("parameter", Parameter);
            Request.Add("value", Value);
            Request.Add("valueSize", ValueSize);
            Request.Add("valueFormat", ValueFormat);


            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0 - Variant 3: Partial parameter, must be defined in a config file
        public Task<CMDResult> ZWJSS_SetRawConfigParameterValue(int Parameter, int Bitmask, int Value)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.SetRawConfigParameterValue);
            Request.Add("nodeId", this.id);
            Request.Add("parameter", Parameter);
            Request.Add("bitMask", Bitmask);
            Request.Add("value", Value);


            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }
        
        // Checked as of : 3.5.0
        public Task<CMDResult> RefreshValues()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.RefreshValues);
            Request.Add("nodeId", this.id);


            string RequestPL = JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> RefreshCCValues(int CommandClass)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.RefreshCCValues);
            Request.Add("commandClass", CommandClass);
            Request.Add("nodeId", this.id);


            string RequestPL = JsonConvert.SerializeObject(Request);
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
                    Res.SetPayload(JO.SelectToken("result.valueIds").ToObject<ValueID[]>());
                }

                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.GetDefinedValueIDs);
            Request.Add("nodeId", this.id);


            string RequestPL = JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> GetValueMetadata(ValueID VID)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Res.SetPayload(JO.SelectToken("result").ToObject<ValueMetadata>());
                }

                Result.SetResult(Res);

            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.GetValueMetadata);
            Request.Add("nodeId", this.id);
            Request.Add("valueId", VID);


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
            Request.Add("command", Enums.Commands.SupportsCCAPI);
            Request.Add("nodeId", this.id);
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
            Request.Add("command", Enums.Commands.InvokeCCAPI);
            Request.Add("nodeId", this.id);
            Request.Add("commandClass", CommandClass);
            Request.Add("methodName", Method);
            Request.Add("args", Params);

            string RequestPL = JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
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
            Request.Add("command", Enums.Commands.GetEndpointCount);
            Request.Add("nodeId", this.id);

            string RequestPL = JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> GetHighestSecurityClass()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Enums.SecurityClass Value = JO.SelectToken("result.highestSecurityClass").ToObject<Enums.SecurityClass>();
                    Res.SetPayload(Value);
                }
                Result.SetResult(Res);

               
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.GetHighestSecurityClass);
            Request.Add("nodeId", this.id);

            string RequestPL = JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> HasSecurityClass(Enums.SecurityClass Class)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Res.SetPayload(JO.SelectToken("result.hasSecurityClass").ToObject<bool>());
                }
                Result.SetResult(Res);
              
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.HasSecurityClass);
            Request.Add("nodeId", this.id);
            Request.Add("securityClass", Class);

            string RequestPL = JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> WaitForWakeup()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                Result.SetResult(Res);

            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.WaitForWakeUp);
            Request.Add("nodeId", this.id);

            string RequestPL = JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> ManuallyIdleNotificationValue(ValueID VID)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                Result.SetResult(Res);

            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.ManuallyIdleNotificationValue);
            Request.Add("nodeId", this.id);
            Request.Add("valueId", VID);

            string RequestPL = JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> ManuallyIdleNotificationValue(int notificationType, int prevValue, int? endpointIndex = null)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                Result.SetResult(Res);

            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.ManuallyIdleNotificationValue);
            Request.Add("nodeId", this.id);
            Request.Add("notificationType", notificationType);
            Request.Add("prevValue", prevValue);
            if (endpointIndex.HasValue)
            {
                Request.Add("endpointIndex", endpointIndex.Value);
            }

            string RequestPL = JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

         // LOCAL
        public Endpoint GetEndpoint(int Index)
        {
            Endpoint EP = this.endpoints.FirstOrDefault((E) => E.index.Equals(Index));
            return EP;
        }

        [Newtonsoft.Json.JsonProperty]
        public Endpoint[] endpoints { get; internal set; }
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
        [Newtonsoft.Json.JsonProperty]
        public DateTime? lastSeen { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public Protocols protocol { get; internal set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "nodeId")]
        public int id { get; internal set; }
        
        [Newtonsoft.Json.JsonProperty]
        public bool keepAwake { get; internal set; }
        public Task<CMDResult>  SetKeepAwake(bool Option)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    this.keepAwake = Option;
                }
                Result.SetResult(Res);

            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.KeepNodeAwake);
            Request.Add("nodeId", this.id);
            Request.Add("keepAwake", Option);

            string RequestPL = JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        [Newtonsoft.Json.JsonProperty]
        public string name { get; internal set; }
        public Task<CMDResult> SetName(string Name, bool UpdateCC = true)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    this.name = Name;
                }
                Result.SetResult(Res);

            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.SetName);
            Request.Add("nodeId", this.id);
            Request.Add("name", Name);
            Request.Add("updateCC", UpdateCC);

            string RequestPL = JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        [Newtonsoft.Json.JsonProperty]
        public string location { get; internal set; }
        public Task<CMDResult> SetLocation(string Location, bool UpdateCC = true)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    this.location = Location;
                }
                Result.SetResult(Res);

            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.SetLocation);
            Request.Add("nodeId", this.id);
            Request.Add("location", Location);
            Request.Add("updateCC", UpdateCC);

            string RequestPL = JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

    }
}
