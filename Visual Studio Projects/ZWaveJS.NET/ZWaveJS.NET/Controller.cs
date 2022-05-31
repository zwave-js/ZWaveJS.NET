using System;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace ZWaveJS.NET
{
    public class Controller
    {
        internal Controller()
        {

        }

        public delegate void BackupNVMProgress(int BytesRead, int Total);
        private  BackupNVMProgress BackupNVMProgressSub;
        internal void Trigger_BackupNVMProgress(int BytesRead, int Total)
        {
            BackupNVMProgressSub?.Invoke(BytesRead, Total);
        }

        public delegate void ConvertRestoreNVMProgress(int BytesRead, int Total);
        private ConvertRestoreNVMProgress ConvertRestoreNVMProgressSub;
        internal void Trigger_ConvertRestoreNVMProgress(int BytesRead, int Total)
        {
            ConvertRestoreNVMProgressSub?.Invoke(BytesRead, Total);
        }

        public delegate void RestoreNVMProgress(int BytesWritten, int Total);
        private RestoreNVMProgress RestoreNVMProgressSub;
        internal void Trigger_RestoreNVMProgressSub(int BytesWritten, int Total)
        {
            RestoreNVMProgressSub?.Invoke(BytesWritten, Total);
        }

        public delegate void StatisticsUpdatedEvent(ControllerStatistics Statistics);
        public event StatisticsUpdatedEvent StatisticsUpdated;
        internal void Trigger_StatisticsUpdated(ControllerStatistics Statistics)
        {
            this.statistics = Statistics;
            StatisticsUpdated?.Invoke(Statistics);
        }

        public delegate void HealNetworkProgressEvent(Dictionary<string,string> Progress);
        public event HealNetworkProgressEvent HealNetworkProgress;
        internal void Trigger_HealNetworkProgress(Dictionary<string, string> Progress)
        {
             HealNetworkProgress?.Invoke(Progress);
        }

        public delegate void HealNetworkDoneEvent(Dictionary<string, string> Result);
        public event HealNetworkDoneEvent HealNetworkDone;
        internal void Trigger_HealNetworkDone(Dictionary<string, string> Result)
        {
            this.isHealNetworkActive = false;
            HealNetworkDone?.Invoke(Result);
        }

        private Abort AbortSub;
        internal void Trigger_InclusionAborted()
        {
            AbortSub?.Invoke();
        }

        private ValidateDSKAndEnterPIN ValidateDSKAndEnterPINSub;
        internal string Trigger_ValidateDSK(string DSK)
        {
            return ValidateDSKAndEnterPINSub?.Invoke(DSK);
        }

        private GrantSecurityClasses GrantSecurityClassesSub;
        internal InclusionGrant Trigger_GrantSecurityClasses(InclusionGrant Requested)
        {
            return GrantSecurityClassesSub?.Invoke(Requested);
        }

        public delegate void InclusionStartedEvent(bool Secure);
        public event InclusionStartedEvent InclusionStarted;
        internal void Trigger_InclusionStarted(bool Secure)
        {
            InclusionStarted?.Invoke(Secure);
        }

        public delegate void InclusionStoppedEvent();
        public event InclusionStoppedEvent InclusionStopped;
        internal void Trigger_InclusionStopped()
        {
            InclusionStopped?.Invoke();
        }

        public delegate void ExclusionStartedEvent();
        public event ExclusionStartedEvent ExclusionStarted;
        internal void Trigger_ExclusionStarted()
        {
            ExclusionStarted?.Invoke();
        }

        public delegate void ExclusionStoppedEvent();
        public event ExclusionStoppedEvent ExclusionStopped;
        internal void Trigger_ExclusionStopped()
        {
            ExclusionStopped?.Invoke();
        }

        public delegate void NodeRemovedEvent(ZWaveNode Node);
        public event NodeRemovedEvent NodeRemoved;
        internal void Trigger_NodeRemoved(ZWaveNode Node)
        {
            NodeRemoved?.Invoke(Node);
        }

        public delegate void NodeAddedEvent(ZWaveNode Node, InclusionResult Result);
        public event NodeAddedEvent NodeAdded;
        internal void Trigger_NodeAdded(ZWaveNode Node, InclusionResult Result)
        {
            NodeAdded?.Invoke(Node, Result);
        }

        public Task<bool> RestoreNVM(byte[] NVMData, ConvertRestoreNVMProgress ConvertProgress = null, RestoreNVMProgress RestoreProgress = null)
        {
            ConvertRestoreNVMProgressSub = ConvertProgress;
            RestoreNVMProgressSub = RestoreProgress;

            Guid ID = Guid.NewGuid();
            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();
            Driver.Callbacks.Add(ID, (JO) =>
            {
                if (JO.Value<bool>("success"))
                {
                    Result.SetResult(true);
                    _Driver.Restart();
                }
                else
                {
                    Result.SetResult(false);
                }
                
                
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.RestoreNVM);
            Request.Add("nvmData", Convert.ToBase64String(NVMData));

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<byte[]> BackupNVMRaw(BackupNVMProgress OnProgress = null)
        {
            BackupNVMProgressSub = OnProgress;

            Guid ID = Guid.NewGuid();
            TaskCompletionSource<byte[]> Result = new TaskCompletionSource<byte[]>();
            Driver.Callbacks.Add(ID, (JO) =>
            {
                string B64 = JO.SelectToken("result.nvmData").ToString();
                Result.SetResult(Convert.FromBase64String(B64));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.BackUpNVM);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<bool> ReplaceFailedNode(int NodeID, InclusionOptions Options)
        {
            ValidateDSKAndEnterPINSub = null;
            GrantSecurityClassesSub = null;
            AbortSub = null;

            switch (Options.strategy)
            {
                case Enums.InclusionStrategy.Default:
                case Enums.InclusionStrategy.Security_S2:
                    ValidateDSKAndEnterPINSub = Options.userCallbacks?.validateDSKAndEnterPIN ?? null;
                    GrantSecurityClassesSub = Options.userCallbacks?.grantSecurityClasses ?? null;
                    AbortSub = Options.userCallbacks?.abort ?? null;
                    break;
            }

            if(Options.strategy == Enums.InclusionStrategy.Default || Options.strategy == Enums.InclusionStrategy.Security_S2)
            {
                if(ValidateDSKAndEnterPINSub == null || GrantSecurityClassesSub == null || AbortSub == null)
                {
                    throw new InvalidOperationException("S2 Security require userCallbacks to be provided");
                }
            }

            Guid ID = Guid.NewGuid();
            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();
            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> _Options = new Dictionary<string, object>();
            _Options.Add("strategy", (int)Options.strategy);

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.ReplaceFailedNode);
            Request.Add("nodeId", NodeID);
            Request.Add("options", _Options);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<bool> RemoveFailedNode(int NodeID)
        {
            Guid ID = Guid.NewGuid();
            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();

            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();

            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.RemoveFailedNode);
            Request.Add("nodeId", NodeID);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<bool> HealNode(int NodeID)
        {
            Guid ID = Guid.NewGuid();
            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();

            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();

            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.HealNode);
            Request.Add("nodeId", NodeID);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<bool> BeginHealingNetwork()
        {
            Guid ID = Guid.NewGuid();
            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();

            Driver.Callbacks.Add(ID, (JO) =>
            {
                bool _Result = JO.Value<bool>("success");
                if (_Result)
                {
                    this.isHealNetworkActive = true;
                }

                Result.SetResult(_Result);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();

            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.BeginHealingNetwork);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<bool> StopHealingNetwork()
        {
            
            Guid ID = Guid.NewGuid();
            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();

            Driver.Callbacks.Add(ID, (JO) =>
            {
                this.isHealNetworkActive = false;
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();

            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.StopHealingNetwork);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<bool> BeginInclusion(InclusionOptions Options)
        {
            ValidateDSKAndEnterPINSub = null;
            GrantSecurityClassesSub = null;
            AbortSub = null;

            switch (Options.strategy)
            {
                case Enums.InclusionStrategy.Default:
                case Enums.InclusionStrategy.Security_S2:
                    ValidateDSKAndEnterPINSub = Options.userCallbacks?.validateDSKAndEnterPIN ?? null;
                    GrantSecurityClassesSub = Options.userCallbacks?.grantSecurityClasses ?? null;
                    AbortSub = Options.userCallbacks?.abort ?? null;
                    break;
            }

            if (Options.strategy == Enums.InclusionStrategy.Default || Options.strategy == Enums.InclusionStrategy.Security_S2)
            {
                if (ValidateDSKAndEnterPINSub == null || GrantSecurityClassesSub == null || AbortSub == null)
                {
                    throw new InvalidOperationException("S2 Security require userCallbacks to be provided");
                }

            }

            Guid ID = Guid.NewGuid();
            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();
            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> _Options = new Dictionary<string, object>();
            _Options.Add("strategy", (int)Options.strategy);
            _Options.Add("forceSecurity", Options.forceSecurity);

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.BeginInclusion);
            Request.Add("options", _Options);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<bool> StopInclusion()
        {
            Guid ID = Guid.NewGuid();
            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();

            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.StopInclusion);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<bool> UnprovisionSmartStartNode(int NodeID)
        {
            Guid ID = Guid.NewGuid();
            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();

            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();

            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.UnprovisionSmartStartNode);
            Request.Add("dskOrNodeId", NodeID);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<bool> UnprovisionSmartStartNode(string DSK)
        {
            Guid ID = Guid.NewGuid();
            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();

            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();

            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.UnprovisionSmartStartNode);
            Request.Add("dskOrNodeId", DSK);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }


        public Task<bool> ProvisionSmartStartNode(string QRCode)
        {
            Guid ID = Guid.NewGuid();
            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();

            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();

            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.ProvisionSmartStartNode);
            Request.Add("entry", QRCode);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<bool> BeginExclusion(bool unprovision = false)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();

            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();

            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.BeginExclusion);
            Request.Add("unprovision", unprovision);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<bool> StopExclusion()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();

            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();

            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.StopExclusion);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        

        public NodesCollection Nodes { get; internal set; }

        [Newtonsoft.Json.JsonProperty]
        public string libraryVersion { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int type { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public long homeId { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int ownNodeId { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool isSecondary { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool isUsingHomeIdFromOtherNetwork { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool isSISPresent { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool wasRealPrimary { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool isStaticUpdateController { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool isSlave { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public string serialApiVersion { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int manufacturerId { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int productType { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int productId { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int[] supportedFunctionTypes { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public int sucNodeId { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool supportsTimers { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool isHealNetworkActive { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public ControllerStatistics statistics { get; internal set; }
        internal Driver _Driver;

    }
}
