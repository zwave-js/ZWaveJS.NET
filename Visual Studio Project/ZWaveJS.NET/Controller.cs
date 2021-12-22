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
            HealNetworkDone?.Invoke(Result);
        }

        public delegate string ValidateDSKEvent(string PartialDSK);
        public event ValidateDSKEvent ValidateDSK;
        internal string Trigger_ValidateDSK(string PartialDSK)
        {
            return ValidateDSK?.Invoke(PartialDSK);
        }

        public delegate InclusionGrant GrantSecurityClassesEvent(Enums.SecurityClass[] SecurityClasses, bool ClientSideAuth);
        public event GrantSecurityClassesEvent GrantSecurityClasses;
        internal InclusionGrant Trigger_GrantSecurityClasses(Enums.SecurityClass[] SecurityClasses, bool ClientSideAuth)
        {
            return GrantSecurityClasses?.Invoke(SecurityClasses, ClientSideAuth);
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

        public delegate void NodeRemovedEvent(int NodeID);
        public event NodeRemovedEvent NodeRemoved;
        internal void Trigger_NodeRemoved(int NodeID)
        {
            NodeRemoved?.Invoke(NodeID);
        }

        public delegate void NodeAddedEvent(ZWaveNode Node);
        public event NodeAddedEvent NodeAdded;
        internal void Trigger_NodeAdded(ZWaveNode Node)
        {
            NodeAdded?.Invoke(Node);
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
                Result.SetResult(JO.Value<bool>("success"));
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
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();

            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.StopHealingNetwork);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<bool> BeginInclusion(Enums.InclusionStrategy Strategy, bool EnforceSecurity = false)
        {
            if (Strategy == Enums.InclusionStrategy.Default || Strategy == Enums.InclusionStrategy.Security_S2)
            {
                if (GrantSecurityClasses == null || ValidateDSK == null)
                {
                    throw new InvalidOperationException("Events: Controller.GrantSecurityClasses and Controller.ValidateDSK need to be subscribed to.");
                }
            }

            Guid ID = Guid.NewGuid();
            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();
            Driver.Callbacks.Add(ID, (JO) =>
            {
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> Options = new Dictionary<string, object>();
            Options.Add("strategy", (int)Strategy);
            Options.Add("forceSecurity", EnforceSecurity);

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.BeginInclusion);
            Request.Add("options", Options);

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

        public Task<bool> BeginExclusion()
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

    }
}
