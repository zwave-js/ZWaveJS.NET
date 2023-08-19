using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.WebSockets;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Websocket.Client;

namespace ZWaveJS.NET
{
    public class Driver
    {
        internal static Websocket.Client.WebsocketClient ClientWebSocket;
        internal static Dictionary<Guid, Action<JObject>> Callbacks;
        internal static CustomBooleanJsonConverter BoolConverter;
        internal static bool Inited = false;
        internal ZWaveOptions Options;

        private Dictionary<string, Action<JObject>> NodeEventMap;
        private Dictionary<string, Action<JObject>> ControllerEventMap;
        private Dictionary<string, Action<JObject>> DriverEventMap;
        private static int SchemaVersionID = 30;
        private string SerialPort;

        private Uri WSAddress;
        private bool Host = true;
        
        private string _ZWaveJSDriverVersion;
        public string ZWaveJSDriverVersion
        {
            get
            {
                return _ZWaveJSDriverVersion;
            }
        }

        private string _ZWaveJSServerVersion;
        public string ZWaveJSServerVersion
        {
            get
            {
                return _ZWaveJSServerVersion;
            }
        }

        public static int ServerCommunicationPort = 50001;
        public Controller Controller { get; internal set; }
        public delegate void DriverReadyEvent();
        public event DriverReadyEvent DriverReady;
        public delegate void StartupError(string Message);
        public event StartupError StartupErrorEvent;

        public delegate void LoggingEventDelegate(LoggingEventArgs args);
        public event LoggingEventDelegate LoggingEvent;
        internal void Trigger_LoggingEvent(LoggingEventArgs args)
        {
            LoggingEvent?.Invoke(args);
        }

        private void MapNodeEvents()
        {
            NodeEventMap.Add("check lifeline health progress", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                int Round = JO.SelectToken("event.round").Value<int>();
                int Total = JO.SelectToken("event.totalRounds").Value<int>();
                int LastRating = JO.SelectToken("event.lastRating").Value<int>();

                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_LifelineHealthCheckProgress(Round, Total, LastRating);
            });

            NodeEventMap.Add("statistics updated", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                NodeStatisticsUpdatedArgs NS = JsonConvert.DeserializeObject<NodeStatisticsUpdatedArgs>(JO.SelectToken("event.statistics").ToString());
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_StatisticsUpdated(NS);
            });

            NodeEventMap.Add("firmware update finished", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                int Status = JO.SelectToken("event.status").Value<int>();
                int Wait = 0;

                if (JO.SelectToken("event.waitTime") != null)
                {
                    Wait = JO.SelectToken("event.waitTime").Value<int>();
                }

                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_FirmwareUpdateFinished(Status, Wait);

            });


            NodeEventMap.Add("firmware update progress", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                int Sent = JO.SelectToken("event.sentFragments").Value<int>();
                int Total = JO.SelectToken("event.totalFragments").Value<int>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_FirmwareUpdateProgress(Sent, Total);
            });

            NodeEventMap.Add("value updated", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                ValueUpdatedArgs Args = JsonConvert.DeserializeObject<ValueUpdatedArgs>(JO.SelectToken("event.args").ToString());
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_ValueUpdated(Args);
            });

            NodeEventMap.Add("value added", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                ValueAddedArgs Args = JsonConvert.DeserializeObject<ValueAddedArgs>(JO.SelectToken("event.args").ToString());
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_ValueAdded(Args);
            });

            NodeEventMap.Add("value removed", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                ValueRemovedArgs Args = JsonConvert.DeserializeObject<ValueRemovedArgs>(JO.SelectToken("event.args").ToString());
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_ValueRemoved(Args);
            });

            NodeEventMap.Add("value notification", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                ValueNotificationArgs Args = JsonConvert.DeserializeObject<ValueNotificationArgs>(JO.SelectToken("event.args").ToString());
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_ValueNotification(Args);
            });

            NodeEventMap.Add("notification", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                int CCID = JO.SelectToken("event.ccId").Value<int>();
                JObject IJO = JO.SelectToken("event.args").Value<JObject>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_Notification(CCID, IJO);
            });

            NodeEventMap.Add("dead", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_NodeDead();
            });

            NodeEventMap.Add("wake up", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_NodeAwake();
            });

            NodeEventMap.Add("sleep", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_NodeAsleep();
            });

            NodeEventMap.Add("ready", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                ZWaveNode NNI = JsonConvert.DeserializeObject<ZWaveNode>(JO.SelectToken("event.nodeState").ToString(), BoolConverter);

                ZWaveNode N = this.Controller.Nodes.Get(NID);
                this.Controller.Nodes.ReplaceInformation(NNI, N);
                N.Trigger_NodeReady();
            });

            NodeEventMap.Add("interview started", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_NodeInterviewStarted();
            });

            NodeEventMap.Add("interview completed", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_NodeInterviewCompleted();
            });

            NodeEventMap.Add("interview failed", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                NodeInterviewFailedEventArgs FII = JsonConvert.DeserializeObject<NodeInterviewFailedEventArgs>(JO.SelectToken("event.args").ToString());
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_NodeInterviewFailed(FII);
            });
        }

        private void MapControllerEvents()
        {
            ControllerEventMap.Add("nvm backup progress", (JO) =>
            {
                int Read = JO.SelectToken("event.bytesRead").Value<int>();
                int Total = JO.SelectToken("event.total").Value<int>();
                this.Controller.Trigger_BackupNVMProgress(Read, Total);
            });

            ControllerEventMap.Add("nvm convert progress", (JO) =>
            {
                int Read = JO.SelectToken("event.bytesRead").Value<int>();
                int Total = JO.SelectToken("event.total").Value<int>();
                this.Controller.Trigger_ConvertRestoreNVMProgress(Read, Total);
            });

            ControllerEventMap.Add("nvm restore progress", (JO) =>
            {
                int Written = JO.SelectToken("event.bytesWritten").Value<int>();
                int Total = JO.SelectToken("event.total").Value<int>();
                this.Controller.Trigger_RestoreNVMProgressSub(Written, Total);
            });

            ControllerEventMap.Add("statistics updated", (JO) =>
            {
                ControllerStatisticsUpdatedArgs CS = JsonConvert.DeserializeObject<ControllerStatisticsUpdatedArgs>(JO.SelectToken("event.statistics").ToString());
                this.Controller.Trigger_StatisticsUpdated(CS);
            });

            ControllerEventMap.Add("inclusion aborted", (JO) =>
            {
                this.Controller.Trigger_InclusionAborted();
            });

            ControllerEventMap.Add("inclusion started", (JO) =>
            {
                bool Secure = JO.SelectToken("event.secure").Value<bool>();
                this.Controller.Trigger_InclusionStarted(Secure);
            });

            ControllerEventMap.Add("inclusion stopped", (JO) =>
            {
                this.Controller.Trigger_InclusionStopped();
            });

            ControllerEventMap.Add("exclusion started", (JO) =>
            {
                this.Controller.Trigger_ExclusionStarted();
            });

            ControllerEventMap.Add("exclusion stopped", (JO) =>
            {
                this.Controller.Trigger_ExclusionStopped();
            });

            ControllerEventMap.Add("node removed", (JO) =>
            {
                int NID = JO.SelectToken("event.node.nodeId").Value<int>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                this.Controller.Trigger_NodeRemoved(N);

                this.Controller.Nodes.RemoveNodeFromCollection(NID);
            });

            ControllerEventMap.Add("node added", (JO) =>
            {
                int NID = JO.SelectToken("event.node.nodeId").Value<int>();
                InclusionResult IR = JsonConvert.DeserializeObject<InclusionResult>(JO.SelectToken("event.result").ToString());

                ZWaveNode NN = new ZWaveNode();
                NN.id = NID;

                this.Controller.Nodes.AddNodeToCollection(NN);
                this.Controller.Trigger_NodeAdded(NN, IR);
            });

            ControllerEventMap.Add("grant security classes",  (JO) =>
            {
                InclusionGrant RIG = JsonConvert.DeserializeObject<InclusionGrant>(JO.SelectToken("event.requested").ToString());
                InclusionGrant SIG = this.Controller.Trigger_GrantSecurityClasses(RIG);

                Dictionary<string, object> Request = new Dictionary<string, object>();
                Request.Add("messageId", Guid.NewGuid().ToString());
                Request.Add("command", Enums.Commands.GrantSecurityClasses);
                Request.Add("inclusionGrant", SIG);

                string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
                ClientWebSocket.SendInstant(RequestPL);
                
            });

            ControllerEventMap.Add("validate dsk and enter pin",  (JO) =>
            {
                string DSK = this.Controller.Trigger_ValidateDSK(JO.SelectToken("event.dsk").Value<string>());

                Dictionary<string, object> Request = new Dictionary<string, object>();
                Request.Add("messageId", Guid.NewGuid().ToString());
                Request.Add("command", Enums.Commands.ValidateDSK);
                Request.Add("pin", DSK);

                string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
                ClientWebSocket.SendInstant(RequestPL);
            });

            ControllerEventMap.Add("heal network progress", (JO) =>
            {
                Dictionary<string, string> Progress = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(JO.SelectToken("event.progress").ToString());
                this.Controller.Trigger_HealNetworkProgress(Progress);
            });

            ControllerEventMap.Add("heal network done", (JO) =>
            {
                Dictionary<string, string> Result = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(JO.SelectToken("event.result").ToString());
                this.Controller.Trigger_HealNetworkDone(Result);

            });
        }

        private void MapDriverEvents()
        {
            DriverEventMap.Add("logging", (JO) =>
            {
                LoggingEventArgs Args = Newtonsoft.Json.JsonConvert.DeserializeObject<LoggingEventArgs>(JO.SelectToken("event").ToString());
                Trigger_LoggingEvent(Args);
            });
        }

        private void MapEvents()
        {
            NodeEventMap = new Dictionary<string, Action<JObject>>();
            MapNodeEvents();

            ControllerEventMap = new Dictionary<string, Action<JObject>>();
            MapControllerEvents();

            DriverEventMap = new Dictionary<string, Action<JObject>>();
            MapDriverEvents();
        }

      

        // Client Mode
        public Driver(Uri Server, int SchemaVersion = 0)
        {
            if (SchemaVersion > 0)
            {
                SchemaVersionID = SchemaVersion;
            }

            Callbacks = new Dictionary<Guid, Action<JObject>>();
            MapEvents();
            BoolConverter = new CustomBooleanJsonConverter();
            
            this.WSAddress = Server;
            this.Host = false;

            InternalPrep();

        }

        // Host Mode
        public Driver(string SerialPort, ZWaveOptions Options)
        {

            Callbacks = new Dictionary<Guid, Action<JObject>>();
            MapEvents();
            BoolConverter = new CustomBooleanJsonConverter();

            Server.FatalError += Server_FatalError;

            this.SerialPort = SerialPort;
            this.Options = Options;
            this.WSAddress = new Uri("ws://localhost:" + ServerCommunicationPort);
            this.Host = true;

            InternalPrep();

        }

        private void InternalPrep()
        {
            if (this.Host)
            {
                Server.Start(SerialPort, Options, ServerCommunicationPort);
            }

            ClientWebSocket = new Websocket.Client.WebsocketClient(this.WSAddress);
            ClientWebSocket.MessageReceived.Subscribe((Message) => {
                WebsocketClient_MessageReceived(ClientWebSocket, Message);
            });

            ClientWebSocket.ReconnectTimeout = TimeSpan.FromSeconds(5);
            ClientWebSocket.ErrorReconnectTimeout = TimeSpan.FromSeconds(5);

        }
        
        // Proces Message
        private void WebsocketClient_MessageReceived(object sender, ResponseMessage Message)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debug.WriteLine(Message);
            }
            
            if (Message.MessageType == WebSocketMessageType.Text)
            {
                string Content = Message.Text;
                
                JObject JO = JObject.Parse(Content);

                string Type = JO.Value<string>("type");
                Guid MessageID = JO.ContainsKey("messageId") ? Guid.Parse(JO.Value<string>("messageId")) : Guid.Empty;

                if (MessageID != Guid.Empty)
                {
                    if (Callbacks.ContainsKey(MessageID))
                    {
                        // Guard against race condition
                        try
                        {
                            Callbacks[MessageID].Invoke(JO);
                            Callbacks.Remove(MessageID);
                        }
                        catch (Exception Error) { }
                        
                    }

                    return;
                }

                if (Type == "version")
                {
                    _ZWaveJSDriverVersion = JO.Value<string>("driverVersion");
                    _ZWaveJSServerVersion = JO.Value<string>("serverVersion");

                    Guid CBID = Guid.NewGuid();
                    Callbacks.Add(CBID, SetAPIVersionCB);

                    Dictionary<string, object> Request = new Dictionary<string, object>();
                    Request.Add("messageId", CBID.ToString());
                    Request.Add("command", Enums.Commands.SetAPIVersion);
                    Request.Add("schemaVersion", SchemaVersionID);

                    string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);

                    ClientWebSocket.SendInstant(RequestPL);

                    return;
                }

                if (Type == "event")
                {
                    string _Source = JO.SelectToken("event.source").Value<string>();
                    string _Event = JO.SelectToken("event.event").Value<string>();

                    switch (_Source)
                    {
                        case "node":
                            if (NodeEventMap.ContainsKey(_Event))
                            {
                                NodeEventMap[_Event].Invoke(JO);
                            }
                            break;

                        case "controller":
                            if (ControllerEventMap.ContainsKey(_Event))
                            {
                                ControllerEventMap[_Event].Invoke(JO);
                            }
                            break;

                        case "driver":
                            if (DriverEventMap.ContainsKey(_Event))
                            {
                                DriverEventMap[_Event].Invoke(JO);
                            }
                            break;

                    }
                    return;
                }
            }
        }

        // Start Driver
        public void Start()
        {
            ClientWebSocket.Start();
        }

        public void Destroy()
        {
            if (ClientWebSocket != null)
            {
                if (ClientWebSocket.IsRunning)
                {
                    ClientWebSocket.Stop(WebSocketCloseStatus.NormalClosure, "Destroy");
                }
              
                ClientWebSocket.Dispose();
                ClientWebSocket = null;
            }

            Server.Terminate();
        }

       

        private void Client_ServerDisconnected(object sender, EventArgs e)
        {
            // Signal waiting callbacks
            Guid[] Keys = Callbacks.Keys.ToArray();
            foreach (Guid ID in Keys)
            {
                JObject JO = new JObject();
                JO.Add("success", false);
                JO.Add("zwaveErrorCode", Enums.ErrorCodes.WSConnectionError);
                JO.Add("zwaveErrorMessage", "The Connection to the Server was interrupted. It is unknown if the command was successfull, assuming false. The connection will be restored.");

                // Guard against race condition
                try
                {
                    Callbacks[ID].Invoke(JO);
                    Callbacks.Remove(ID);
                }
                catch(Exception Error)
                {
                    continue;
                }

            }
            Restart();

        }

     

        async internal void Restart()
        {
            Destroy();

            await Task.Delay(5000);
            InternalPrep();
            Start();
        }

        private void Server_FatalError()
        {
            Destroy();
            StartupErrorEvent?.Invoke("Fatal ZWave Server Error.");
        }
        
        private void SetAPIVersionCB(JObject JO)
        {
            if (JO.Value<bool>("success"))
            {
                Guid CBID = Guid.NewGuid();
                Callbacks.Add(CBID, StartListetningCB);

                Dictionary<string, object> Request = new Dictionary<string, object>();
                Request.Add("messageId", CBID.ToString());
                Request.Add("command", Enums.Commands.StartListetning);

                string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);

                ClientWebSocket.SendInstant(RequestPL);
            }
            else
            {
                string ErrorCode = JO.Value<string>("errorCode");
                switch (ErrorCode)
                {
                    case "schema_incompatible":
                        StartupErrorEvent?.Invoke("Client and Server schema mismatch");
                        break;

                }
            }

        }

        private void StartListetningCB(JObject JO)
        {
            if (!Inited)
            {
                if (JO.Value<bool>("success"))
                {
                    Controller C = JsonConvert.DeserializeObject<Controller>(JO.SelectToken("result.state.controller").ToString());
                    C._Driver = this;
                    ZWaveNode[] Nodes = JsonConvert.DeserializeObject<ZWaveNode[]>(JO.SelectToken("result.state.nodes").ToString(), BoolConverter);
                    Nodes = Nodes.Where((N) => !N.isControllerNode).ToArray();

                    this.Controller = C;
                    this.Controller.Nodes = new NodesCollection(Nodes);

                    Inited = true;

                    DriverReady?.Invoke();
                }
            }
        }

        public Task<CMDResult> StartListeningLogs()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();

            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.StartListeningLogs);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> StopListeningLogs()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();

            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.StopListeningLogs);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }
    }
}
