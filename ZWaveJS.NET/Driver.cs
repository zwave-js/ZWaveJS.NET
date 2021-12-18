using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Websocket.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ZWaveJS.Net
{
    public class Driver
    {
      
        // Things
        private WebsocketClient Client;
        private Dictionary<Guid, Action<JObject>> Callbacks;
        private Dictionary<string, Action<JObject>> EventMap;
        private const int SchemaVersionID = 13;
        private CustomBooleanJsonConverter BoolConverter;
        public static int ServerCommunicationPort = 50001;

        // Events 
        public delegate void ValueUpdatedEvent(int NodeID, JObject Args);
        public event ValueUpdatedEvent ValueUpdated;

        public delegate void NotificationEvent(int NodeID, int ccId, JObject Args);
        public event NotificationEvent Notification;

        public delegate void AwakeEvent(int NodeID);
        public event AwakeEvent NodeAwake;

        public delegate void SleepEvent(int NodeID);
        public event SleepEvent NodeAsleep;

        public delegate void NodeReadyEvent(int NodeID, ZWaveNode Node);
        public event NodeReadyEvent NodeReady;

        public delegate void NodeInterviewStartedEvent(int NodeID);
        public event NodeInterviewStartedEvent NodeInterviewStarted;

        public delegate void NodeInterviewCompletedEvent(int NodeID);
        public event NodeInterviewCompletedEvent NodeInterviewCompleted;

        public delegate void NodeInterviewFailedEvent(int NodeID);
        public event NodeInterviewFailedEvent NodeInterviewFailed;

        public delegate void InclusionStartedEvent(bool Secure);
        public event InclusionStartedEvent InclusionStarted;

        public delegate void InclusionStoppedEvent();
        public event InclusionStoppedEvent InclusionStopped;

        public delegate void ExclusionStartedEvent();
        public event ExclusionStartedEvent ExclusionStarted;

        public delegate void ExclusionStoppedEvent();
        public event ExclusionStoppedEvent ExclusionStopped;

        public delegate void NodeRemovedEvent(int NodeID);
        public event NodeRemovedEvent NodeRemoved;

        public delegate void NodeAddedEvent(int NodeID);
        public event NodeAddedEvent NodeAdded;

        public delegate void DriverReadyEvent(Controller Controller, ZWaveNode[] Nodes);
        public event DriverReadyEvent DriverReady;

        private void MapEvents()
        {
            EventMap = new Dictionary<string, Action<JObject>>();
            EventMap.Add("value updated", (JO) => ValueUpdated?.Invoke(JO.SelectToken("event.nodeId").Value<int>(), JO.SelectToken("event.args").Value<JObject>()));
            EventMap.Add("value added", (JO) => ValueUpdated?.Invoke(JO.SelectToken("event.nodeId").Value<int>(), JO.SelectToken("event.args").Value<JObject>()));
            EventMap.Add("notification", (JO) => Notification?.Invoke(JO.SelectToken("event.nodeId").Value<int>(), JO.SelectToken("event.ccId").Value<int>(), JO.SelectToken("event.args").Value<JObject>()));
            EventMap.Add("wake up", (JO) => NodeAwake?.Invoke(JO.SelectToken("event.nodeId").Value<int>()));
            EventMap.Add("sleep", (JO) => NodeAsleep?.Invoke(JO.SelectToken("event.nodeId").Value<int>()));
            EventMap.Add("ready", (JO) => NodeReady?.Invoke(JO.SelectToken("event.nodeId").Value<int>(), JsonConvert.DeserializeObject<ZWaveNode>(JO.SelectToken("event.nodeState").ToString())));
            EventMap.Add("interview started", (JO) => NodeInterviewStarted?.Invoke(JO.SelectToken("event.nodeId").Value<int>()));
            EventMap.Add("interview completed", (JO) => NodeInterviewCompleted?.Invoke(JO.SelectToken("event.nodeId").Value<int>()));
            EventMap.Add("interview failed", (JO) => NodeInterviewFailed?.Invoke(JO.SelectToken("event.nodeId").Value<int>()));
            EventMap.Add("inclusion started", (JO) => InclusionStarted?.Invoke(JO.SelectToken("event.secure").Value<bool>()));
            EventMap.Add("inclusion stopped", (JO) => InclusionStopped?.Invoke());
            EventMap.Add("exclusion started", (JO) => ExclusionStarted?.Invoke());
            EventMap.Add("exclusion stopped", (JO) => ExclusionStopped?.Invoke());
            EventMap.Add("node removed", (JO) =>  NodeRemoved?.Invoke(JO.SelectToken("event.node.nodeId").Value<int>()));
            EventMap.Add("node added", (JO) => NodeAdded?.Invoke(JO.SelectToken("event.node.nodeId").Value<int>()));
        }
        
        // CLient Mode
        public Driver(Uri Server)
        {
            Callbacks = new Dictionary<Guid, Action<JObject>>();
            MapEvents();
            BoolConverter = new CustomBooleanJsonConverter();
            Client = new WebsocketClient(Server);
            Client.ReconnectTimeout = null;
            Client.ErrorReconnectTimeout = TimeSpan.Parse("00:00:05");
            Client.MessageReceived.Subscribe(ProcessMessage);
        }

        // Host Mode
        public Driver(string SerialPort, ZWaveOptions Options)
        {
            try
            {
                Callbacks = new Dictionary<Guid, Action<JObject>>();
                MapEvents();
                BoolConverter = new CustomBooleanJsonConverter();
                Server.Start(SerialPort, Options, ServerCommunicationPort);

                Client = new WebsocketClient(new Uri("ws://127.0.0.1:" + ServerCommunicationPort));

                Client.ReconnectTimeout = null;
                Client.ErrorReconnectTimeout = TimeSpan.Parse("00:00:05");

                Client.MessageReceived.Subscribe(ProcessMessage);
            }
            catch(Exception err)
            {
                throw err;
            }
            
        }
        
        // Start Driver
        public void Start()
        {
            Client.Start();
        }

        // Proces Message
        private void ProcessMessage(ResponseMessage IncomingMessage)
        {
            if(IncomingMessage.MessageType == System.Net.WebSockets.WebSocketMessageType.Text)
            {
                JObject JO = JObject.Parse(IncomingMessage.Text);

                string Type = JO.Value<string>("type");
                Guid MessageID = JO.ContainsKey("messageId") ? Guid.Parse(JO.Value<string>("messageId")) : Guid.Empty;
                
                System.Diagnostics.Debug.WriteLine(IncomingMessage.Text);

                if(MessageID != Guid.Empty)
                {
                    Callbacks[MessageID].Invoke(JO);
                    Callbacks.Remove(MessageID);

                    return;
                }

                if(Type == "version")
                {

                    Guid CBID = Guid.NewGuid();
                    Callbacks.Add(CBID, SetAPIVersionCB);

                    Dictionary<string, object> Request = new Dictionary<string, object>();
                    Request.Add("messageId", CBID.ToString());
                    Request.Add("command", Enums.Commands.SetAPIVersion);
                    Request.Add("schemaVersion", SchemaVersionID);

                    string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);

                    Client.Send(RequestPL);

                    return;
                }

                if(Type == "event")
                {
                    string EE = JO.SelectToken("event.event").Value<string>();
                    if (EventMap.ContainsKey(EE))
                    {
                        EventMap[EE].Invoke(JO);
                    }

                    return;

                }
            }

            
        }



        private void SetAPIVersionCB(JObject JO)
        {
            if(JO.Value<bool>("success"))
            {
                Guid CBID = Guid.NewGuid();
                Callbacks.Add(CBID, StartListetningCB);

                Dictionary<string, object> Request = new Dictionary<string, object>();
                Request.Add("messageId", CBID.ToString());
                Request.Add("command", Enums.Commands.StartListetning);

                string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);

                Client.Send(RequestPL);
            }

        }

        private void StartListetningCB(JObject JO)
        {
            if (JO.Value<bool>("success"))
            {
                Controller C = JsonConvert.DeserializeObject<Controller>(JO.SelectToken("result.state.controller").ToString());
                ZWaveNode[] Nodes = JsonConvert.DeserializeObject<ZWaveNode[]>(JO.SelectToken("result.state.nodes").ToString(), BoolConverter);
                DriverReady?.Invoke(C, Nodes);
            }
        }

        public Task<bool> BeginInclusion(Enums.InclusionStrategy Strategy, bool EnforceSecurity = false)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();

            Callbacks.Add(ID, (JO) => {
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Dictionary<string, object> Options = new Dictionary<string, object>();

            Options.Add("strategy", (int)Strategy);
            Options.Add("forceSecurity", EnforceSecurity);
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.BeginInclusion);
            Request.Add("options", Options);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Client.Send(RequestPL);

            return Result.Task;

        }

        public Task<bool> StopInclusion()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();

            Callbacks.Add(ID, (JO) => {
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.StopInclusion);
            
            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Client.Send(RequestPL);

            return Result.Task;

        }

        public Task<bool> BeginExclusion()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();

            Callbacks.Add(ID, (JO) => {
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();

            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.BeginExclusion);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Client.Send(RequestPL);

            return Result.Task;

        }

        public Task<bool> StopExclusion()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();

            Callbacks.Add(ID, (JO) => {
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();

            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.StopExclusion);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Client.Send(RequestPL);

            return Result.Task;

        }



        public Task<bool> SetValue(int NodeID, ValueID ValueID,object Value, SetValueOptions Options = null)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();
            Callbacks.Add(ID, (JO) => {
                Result.SetResult(JO.Value<bool>("success"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.SetValue);
            Request.Add("nodeId", NodeID);
            Request.Add("valueId", ValueID);
            Request.Add("value", Value);

            if(Options != null)
            {
                Request.Add("options", Options);
            }

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Client.Send(RequestPL);

            return Result.Task;
        }

       

        public Task<JObject> PollValue(int NodeID, ValueID ValueID)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<JObject> Result = new TaskCompletionSource<JObject>();
            Callbacks.Add(ID, (JO) => {
                Result.SetResult(JO.Value<JObject>("result"));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.PollValue);
            Request.Add("nodeId", NodeID);
            Request.Add("valueId", ValueID);
    
            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Client.Send(RequestPL);

            return Result.Task;
        }

        public Task<ValueID[]> GetDefinedValueIDs(int NodeID)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<ValueID[]> Result = new TaskCompletionSource<ValueID[]>();
            Callbacks.Add(ID, (JO) => {
                Result.SetResult(JsonConvert.DeserializeObject<ValueID[]>(JO.SelectToken("result.valueIds").ToString()));
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.GetDefinedValueIDs);
            Request.Add("nodeId", NodeID);
          

            string RequestPL = JsonConvert.SerializeObject(Request);
            Client.Send(RequestPL);

            return Result.Task;
        }


    }
}
