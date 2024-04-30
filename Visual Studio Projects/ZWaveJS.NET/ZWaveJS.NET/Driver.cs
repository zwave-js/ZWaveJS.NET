using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.WebSockets;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Websocket.Client;

namespace ZWaveJS.NET
{
    public class Driver
    {
        // Global List of Socket Ports that are registered
        internal static List<int> UsedPorts = new List<int>();

        internal Websocket.Client.WebsocketClient ClientWebSocket;
        internal Dictionary<Guid, Action<JObject>> Callbacks;
        internal bool Inited = false;
        internal ZWaveOptions Options;
        internal const string FWUSAPIKey = "921f8000486fcc2744721cfc747aab2db8fc025b5d487cbf2eba76e88ff6f79a064644bf";

        private Dictionary<string, Action<JObject>> NodeEventMap;
        private Dictionary<string, Action<JObject>> ControllerEventMap;
        private Dictionary<string, Action<JObject>> DriverEventMap;
        private Semver.SemVersion SchemaVersionID = new Semver.SemVersion(1, 35, 0);
        private string SerialPort;
        private bool RequestedExit = false;
        private JsonSerializer _jsonSerializer;


        private Uri WSAddress;
        private bool Host = true;
        private Server _server;
        
        private string _ZWaveJSDriverVersion;
        public string ZWJSS_DriverVersion
        {
            get
            {
                return _ZWaveJSDriverVersion;
            }
        }

        private string _ZWaveJSServerVersion;
        public string ZWJSS_ServerVersion
        {
            get
            {
                return _ZWaveJSServerVersion;
            }
        }

        public int ServerCommunicationPort { get; private set; }
        public int ServerErrorThrottleTime { get; private set; }
        private DateTime LastError;

        public Controller Controller { get; internal set; }
        public Utils Utils { get; internal set; }

        public delegate void DriverReadyEvent();
        public event DriverReadyEvent DriverReady;

        public delegate void StartupErrorEvent(string Message);
        public event StartupErrorEvent StartUpError;

        public delegate void ConnectionLostEvent(string Message);
        public event ConnectionLostEvent ConnectionLost;


        public delegate bool UnexpectedHostExitEvent();
        public event UnexpectedHostExitEvent UnexpectedHostExit;
        
        public delegate void LoggingEventDelegate(LoggingEventArgs args);
        public event LoggingEventDelegate ZWJSS_LoggingEvent;
        internal void Trigger_LoggingEvent(LoggingEventArgs args)
        {
            ZWJSS_LoggingEvent?.Invoke(args);
        }

        private void MapNodeEvents()
        {
            NodeEventMap.Add("check lifeline health progress", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").ToObject<int>();
                int Round = JO.SelectToken("event.round").ToObject<int>();
                int Total = JO.SelectToken("event.totalRounds").ToObject<int>();
                int LastRating = JO.SelectToken("event.lastRating").ToObject<int>();

                ZWaveNode N = this.Controller.Nodes.Get(NID);

                Task.Run(() =>
                {
                    N.Trigger_LifelineHealthCheckProgress(Round, Total, LastRating);
                });
            });

            NodeEventMap.Add("statistics updated", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                NodeStatisticsUpdatedArgs NS = JO.SelectToken("event.statistics").ToObject<NodeStatisticsUpdatedArgs>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                Task.Run(() =>
                {
                    N.Trigger_StatisticsUpdated(NS);
                });
            });

            NodeEventMap.Add("firmware update finished", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                NodeFirmwareUpdateResultArgs Result = JO.SelectToken("event.result").ToObject<NodeFirmwareUpdateResultArgs>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                Task.Run(() =>
                {
                    N.Trigger_FirmwareUpdateFinished(Result);
                });

            });


            NodeEventMap.Add("firmware update progress", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                NodeFirmwareUpdateProgressArgs Progress  = JO.SelectToken("event.progress").ToObject<NodeFirmwareUpdateProgressArgs>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                Task.Run(() =>
                {
                    N.Trigger_FirmwareUpdateProgress(Progress);
                });
            });

            NodeEventMap.Add("value updated", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                ValueUpdatedArgs Args = JO.SelectToken("event.args").ToObject<ValueUpdatedArgs>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                Task.Run(() =>
                {
                    N.Trigger_ValueUpdated(Args);
                });
            });

            NodeEventMap.Add("value added", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                ValueAddedArgs Args = JO.SelectToken("event.args").ToObject<ValueAddedArgs>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                Task.Run(() =>
                {
                    N.Trigger_ValueAdded(Args);
                });
            });

            NodeEventMap.Add("value removed", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                ValueRemovedArgs Args = JO.SelectToken("event.args").ToObject<ValueRemovedArgs>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                Task.Run(() =>
                {
                    N.Trigger_ValueRemoved(Args);
                });
            });

            NodeEventMap.Add("value notification", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                ValueNotificationArgs Args = JO.SelectToken("event.args").ToObject<ValueNotificationArgs>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                Task.Run(() =>
                {
                    N.Trigger_ValueNotification(Args);
                });
            });

            NodeEventMap.Add("notification", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").ToObject<int>();
                int CCID = JO.SelectToken("event.ccId").ToObject<int>();
                JObject IJO = JO.SelectToken("event.args").ToObject<JObject>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                Task.Run(() =>
                {
                    N.Trigger_Notification(CCID, IJO);
                });
            });

            NodeEventMap.Add("dead", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").ToObject<int>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                Task.Run(() =>
                {
                    N.Trigger_NodeDead();
                });
            });

            NodeEventMap.Add("wake up", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").ToObject<int>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                Task.Run(() =>
                {
                    N.Trigger_NodeAwake();
                });
            });

            NodeEventMap.Add("sleep", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").ToObject<int>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                Task.Run(() =>
                {
                    N.Trigger_NodeAsleep();
                });
            });

            NodeEventMap.Add("ready", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").ToObject<int>();
                ZWaveNode NNI = JO.SelectToken("event.nodeState").ToObject<ZWaveNode>(_jsonSerializer);

                ZWaveNode N = this.Controller.Nodes.Get(NID);
                this.Controller.Nodes.ReplaceInformation(NNI, N);

                Task.Run(() =>
                {
                    N.Trigger_NodeReady();
                });
            });

            NodeEventMap.Add("interview started", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").ToObject<int>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                Task.Run(() =>
                {
                    N.Trigger_NodeInterviewStarted();
                });
            });

            NodeEventMap.Add("interview completed", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").ToObject<int>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                Task.Run(() =>
                {
                    N.Trigger_NodeInterviewCompleted();
                });
            });

            NodeEventMap.Add("interview failed", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                NodeInterviewFailedEventArgs FII = JO.SelectToken("event.args").ToObject<NodeInterviewFailedEventArgs>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                Task.Run(() =>
                {
                    N.Trigger_NodeInterviewFailed(FII);
                });
            });
        }

        private void MapControllerEvents()
        {
            ControllerEventMap.Add("firmware update finished", (JO) =>
            {
                ControllerFirmwareUpdateResultArgs Result = JO.SelectToken("event.result").ToObject<ControllerFirmwareUpdateResultArgs>();
                
                Task.Run(() =>
                {
                    this.Controller.Trigger_FirmwareUpdateFinished(Result);
                });

            });


            ControllerEventMap.Add("firmware update progress", (JO) =>
            {
                ControllerFirmwareUpdateProgressArgs Progress = JO.SelectToken("event.progress").ToObject<ControllerFirmwareUpdateProgressArgs>();
               
                Task.Run(() =>
                {
                    this.Controller.Trigger_FirmwareUpdateProgress(Progress);
                });
            });

            ControllerEventMap.Add("nvm backup progress", (JO) =>
            {
                int Read = JO.SelectToken("event.bytesRead").ToObject<int>();
                int Total = JO.SelectToken("event.total").ToObject<int>();
               
                Task.Run(() =>
                {
                    this.Controller.Trigger_BackupNVMProgress(Read, Total);
                });
            });

            ControllerEventMap.Add("nvm convert progress", (JO) =>
            {
                int Read = JO.SelectToken("event.bytesRead").ToObject<int>();
                int Total = JO.SelectToken("event.total").ToObject<int>();
               
                Task.Run(() =>
                {
                    this.Controller.Trigger_ConvertRestoreNVMProgress(Read, Total);
                });
            });

            ControllerEventMap.Add("nvm restore progress", (JO) =>
            {
                int Written = JO.SelectToken("event.bytesWritten").ToObject<int>();
                int Total = JO.SelectToken("event.total").ToObject<int>();
               
                Task.Run(() =>
                {
                    this.Controller.Trigger_RestoreNVMProgressSub(Written, Total);
                });
            });

            ControllerEventMap.Add("statistics updated", (JO) =>
            {
                ControllerStatisticsUpdatedArgs CS = JO.SelectToken("event.statistics").ToObject<ControllerStatisticsUpdatedArgs>();
               
                Task.Run(() =>
                {
                    this.Controller.Trigger_StatisticsUpdated(CS);
                });
            });

            ControllerEventMap.Add("inclusion aborted", (JO) =>
            {
                Task.Run(() =>
                {
                    this.Controller.Trigger_InclusionAborted();
                });
            });

            ControllerEventMap.Add("inclusion started", (JO) =>
            {
                bool Secure = JO.SelectToken("event.secure").ToObject<bool>();
               
                Task.Run(() =>
                {
                    this.Controller.Trigger_InclusionStarted(Secure);
                });
            });

            ControllerEventMap.Add("inclusion stopped", (JO) =>
            {
                Task.Run(() =>
                {
                    this.Controller.Trigger_InclusionStopped();
                });
            });

            ControllerEventMap.Add("exclusion started", (JO) =>
            {
                Task.Run(() =>
                {
                    this.Controller.Trigger_ExclusionStarted();
                });
            });

            ControllerEventMap.Add("exclusion stopped", (JO) =>
            {
                Task.Run(() =>
                {
                    this.Controller.Trigger_ExclusionStopped();
                });
            });

            ControllerEventMap.Add("node removed", (JO) =>
            {
                int NID = JO.SelectToken("event.node.nodeId").ToObject<int>();
                Enums.RemoveNodeReason Reason = JO.SelectToken("event.reason").ToObject<Enums.RemoveNodeReason>();
   
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                this.Controller.Nodes.RemoveNodeFromCollection(NID);
               
                Task.Run(() =>
                {
                    this.Controller.Trigger_NodeRemoved(N, Reason);
                });

               
            });

            ControllerEventMap.Add("node added", (JO) =>
            {
                int NID = JO.SelectToken("event.node.nodeId").ToObject<int>();
                InclusionResultArgs IR = JO.SelectToken("event.result").ToObject<InclusionResultArgs>();

                ZWaveNode NN = new ZWaveNode(this);
                NN.id = NID;

                this.Controller.Nodes.AddNodeToCollection(NN);
                
                Task.Run(() =>
                {
                    this.Controller.Trigger_NodeAdded(NN, IR);
                });
         
            });

            ControllerEventMap.Add("node found", (JO) =>
            {
                int NID = JO.SelectToken("event.node.nodeId").ToObject<int>();

                Task.Run(() =>
                {
                    this.Controller.Trigger_NodeFound(NID);
                });

            });

            ControllerEventMap.Add("grant security classes", (JO) =>
            {
                Task.Run(() =>
                 {
                     InclusionGrant RIG = JO.SelectToken("event.requested").ToObject<InclusionGrant>();
                     InclusionGrant SIG = this.Controller.Trigger_GrantSecurityClasses(RIG);

                     Dictionary<string, object> Request = new Dictionary<string, object>();
                     Request.Add("messageId", Guid.NewGuid().ToString());
                     Request.Add("command", Enums.Commands.GrantSecurityClasses);
                     Request.Add("inclusionGrant", SIG);

                     string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
                     ClientWebSocket.SendInstant(RequestPL);
                 });

            });

            ControllerEventMap.Add("validate dsk and enter pin", (JO) =>
            {
                Task.Run(() =>
                {
                    string DSK = this.Controller.Trigger_ValidateDSK(JO.SelectToken("event.dsk").ToObject<string>());

                    Dictionary<string, object> Request = new Dictionary<string, object>();
                    Request.Add("messageId", Guid.NewGuid().ToString());
                    Request.Add("command", Enums.Commands.ValidateDSK);
                    Request.Add("pin", DSK);

                    string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
                    ClientWebSocket.SendInstant(RequestPL);
                });
            });

            ControllerEventMap.Add("rebuild routes progress", (JO) =>
            {
                Dictionary<string, string> Progress = JO.SelectToken("event.progress").ToObject<Dictionary<string, string>>();

                var Pending = Progress.Where((D) => D.Value.Equals("pending"));
                var Done = Progress.Where((D) => D.Value.Equals("done"));
                var Skipped = Progress.Where((D) => D.Value.Equals("skipped"));
                var Failed = Progress.Where((D) => D.Value.Equals("failed"));

                RebuildRoutesProgressArgs Args = new RebuildRoutesProgressArgs();
                Args.HealedNodes = Done.Select(x => Convert.ToInt32(x.Key)).ToArray();
                Args.FailedNodes = Failed.Select(x => Convert.ToInt32(x.Key)).ToArray();
                Args.SkippedNodes = Skipped.Select(x => Convert.ToInt32(x.Key)).ToArray();
                Args.PendingNodes = Pending.Select(x => Convert.ToInt32(x.Key)).ToArray();

                Task.Run(() =>
                {
                    this.Controller.Trigger_RebuildRoutesProgress(Args);
                });
            });

            ControllerEventMap.Add("rebuild routes done", (JO) =>
            {
                Dictionary<string, string> Result = JO.SelectToken("event.result").ToObject<Dictionary<string, string>>();

                var Done = Result.Where((D) => D.Value.Equals("done"));
                var Skipped = Result.Where((D) => D.Value.Equals("skipped"));
                var Failed = Result.Where((D) => D.Value.Equals("failed"));

                RebuildRoutesDoneArgs Args = new RebuildRoutesDoneArgs();
                Args.HealedNodes = Done.Select(x => Convert.ToInt32(x.Key)).ToArray();
                Args.FailedNodes = Failed.Select(x => Convert.ToInt32(x.Key)).ToArray();
                Args.SkippedNodes = Skipped.Select(x => Convert.ToInt32(x.Key)).ToArray();

                Task.Run(() =>
                {
                    this.Controller.Trigger_RebuildRoutesDone(Args);
                });

            });
        }

        private void MapServerEvents()
        {
            DriverEventMap.Add("logging", (JO) =>
            {
                LoggingEventArgs Args = JO.SelectToken("event").ToObject<LoggingEventArgs>();

                Task.Run(() =>
                {
                    Trigger_LoggingEvent(Args);
                });
            });
        }

        private void MapEvents()
        {
            NodeEventMap = new Dictionary<string, Action<JObject>>();
            MapNodeEvents();

            ControllerEventMap = new Dictionary<string, Action<JObject>>();
            MapControllerEvents();

            DriverEventMap = new Dictionary<string, Action<JObject>>();
            MapServerEvents();
        }
        
        // Client Mode
        public Driver(Uri Server, int SchemaVersion = 0, int ServerErrorThrottleTime = 10000)
        {
            Newtonsoft.Json.JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            settings.Converters.Add(new ZWJSSJsonConverter(this));
            _jsonSerializer = JsonSerializer.Create(settings);

            if (SchemaVersion > 0)
            {
                SchemaVersionID = new Semver.SemVersion(1, SchemaVersion, 0);
            }

            Callbacks = new Dictionary<Guid, Action<JObject>>();
            MapEvents();
            
            this.WSAddress = Server;
            this.Host = false;
            this.ServerErrorThrottleTime = ServerErrorThrottleTime;

            InternalPrep();
        }

        // Host Mode
        public Driver(string SerialPort, ZWaveOptions Options, int ServerCommunicationPort = 50001, int ServerErrorThrottleTime = 10000)
        {

            if (UsedPorts.Contains(ServerCommunicationPort))
            {
                throw new Exception(string.Format("Web Socket Port: {0} already in use", ServerCommunicationPort));
            }

            UsedPorts.Add(ServerCommunicationPort);
            
            Newtonsoft.Json.JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            settings.Converters.Add(new ZWJSSJsonConverter(this));
            _jsonSerializer = JsonSerializer.Create(settings);

            Callbacks = new Dictionary<Guid, Action<JObject>>();
            MapEvents();
            
            this.SerialPort = SerialPort;
            this.Options = Options;
            this.ServerCommunicationPort = ServerCommunicationPort;
            this.WSAddress = new Uri("ws://localhost:" + ServerCommunicationPort);
            this.Host = true;
            this.ServerErrorThrottleTime = ServerErrorThrottleTime;
            this._server = new Server();

            InternalPrep();
        }

        // Prep
        private void InternalPrep()
        {
            if (this.Host)
            {
                _server.Start(SerialPort, Options, ServerCommunicationPort);
                _server.Exited += Server_Exited;
                _server.FatalError += Server_FatalError;
            }

            var Factory = new Func<ClientWebSocket>(() => new ClientWebSocket
            {
                Options = { KeepAliveInterval = TimeSpan.FromSeconds(5) }
            });

            ClientWebSocket = new Websocket.Client.WebsocketClient(this.WSAddress, Factory);
           

            ClientWebSocket.MessageReceived.Subscribe((Message) =>
            {
                WebsocketClient_MessageReceived(ClientWebSocket, Message);
            });

            ClientWebSocket.DisconnectionHappened.Subscribe((DisconnectionInfo) =>
            {
                if (!RequestedExit)
                {
                    if (LastError == DateTime.MinValue || (DateTime.Now - LastError).TotalMilliseconds > ServerErrorThrottleTime)
                    {
                        LastError = DateTime.Now;

                        if (!Inited)
                        {
                            StartUpError?.Invoke($"Could not connect to the server, Connection will continue to try: {DisconnectionInfo?.Exception?.Message}");
                        }
                        else
                        {
                            ConnectionLost?.Invoke($"Connection to the server was lost. Connection will attempt to be restored: {DisconnectionInfo?.Exception?.Message}");
                        }
                    }
                }
               

            });

            ClientWebSocket.ReconnectTimeout = null;
            ClientWebSocket.ErrorReconnectTimeout = TimeSpan.FromSeconds(1);

        }

        // OBSOLETE
        // Server Process Exit
        private void Server_Exited()
        {

            if (!RequestedExit)
            {
                Inited = false;
                Controller.Nodes = null;
                Controller = null;

                DestroySocket();
                SettleCallbacksError();
                
                if(UnexpectedHostExit != null)
                {
                    if (UnexpectedHostExit.Invoke())
                    {
                        Restart();
                    }
                }

               
            }
        }
        
        // Start Driver
        public void Start()
        {
            RequestedExit = false;
            ClientWebSocket.Start();
        }

        private void DestroySocket()
        {
            if (ClientWebSocket != null)
            {
                if (ClientWebSocket.IsRunning)
                {
                    ClientWebSocket.Stop(WebSocketCloseStatus.NormalClosure, "Destroy");
                }

                if (Host)
                {
                    UsedPorts.Remove(WSAddress.Port);
                }

                ClientWebSocket.Dispose();
                ClientWebSocket = null;
            }
        }

        private void DestroyServer()
        {
            _server?.Terminate();
        }

        public void Destroy()
        {
            RequestedExit = true;
            Inited = false;
            if(Controller != null)
            {
                Controller.Nodes = null; // Is this necessary?
                Controller = null;
            }
            
            DestroySocket();
            DestroyServer(); 
        }

        async internal void Restart()
        {
            Destroy();

            await Task.Delay(5000);
            InternalPrep();
            Start();
        }

        private void SettleCallbacksError()
        {
            // Signal waiting callbacks
            Guid[] Keys = Callbacks.Keys.ToArray();
            foreach (Guid ID in Keys)
            {
                JObject JO = new JObject();
                JO.Add("success", false);
                JO.Add("zwaveErrorCode", Enums.ErrorCodes.WSConnectionError);
                JO.Add("zwaveErrorMessage", "The Server process unexpectedly terminted. It is unknown if the command was successfull, assuming false. Subscribe to the 'UnexpectedHostExit' event of the driver to restart the Driver Runtime");

                // Guard against race condition
                try
                {
                    Callbacks[ID].Invoke(JO);
                    Callbacks.Remove(ID);
                }
                catch (Exception Error)
                {
                    continue;
                }

            }
        }
        
        private void Server_FatalError()
        {
            RequestedExit = true;
            Inited = false;
            Controller.Nodes = null;
            Controller = null;
            DestroySocket();
            DestroyServer();
            StartUpError?.Invoke("Fatal ZWave Server Error.");
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
                        RequestedExit = true;
                        DestroySocket();
                        DestroyServer();
                        StartUpError?.Invoke("Client and Server schema mismatch");
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
                    Controller C = JO.SelectToken("result.state.controller").ToObject<Controller>(_jsonSerializer);
                    
                    
                    ZWaveNode[] Nodes = JO.SelectToken("result.state.nodes").ToObject<ZWaveNode[]>(_jsonSerializer);
                    C.deviceConfig = Nodes.FirstOrDefault((N) => N.isControllerNode).deviceConfig;
                    Nodes = Nodes.Where((N) => !N.isControllerNode).ToArray();

                    this.Controller = C;
                    this.Controller.Nodes = new NodesCollection(Nodes);

                    Inited = true;

                    this.Utils = new Utils(this);

                    DriverReady?.Invoke();
                }
            }
        }

        public Task<CMDResult> ZWJSS_StartListeningLogs()
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

        public Task<CMDResult> ZWJSS_StopListeningLogs()
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

        public Task<CMDResult> HardReset()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                Result.SetResult(Res);

                Restart();
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();

            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.HardReset);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> SoftReset()
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
            Request.Add("command", Enums.Commands.SoftReset);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        // Proces Message
        private void WebsocketClient_MessageReceived(object sender, ResponseMessage Message)
        {
            

            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debug.WriteLine(Message.Text);
             
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

                    if (Semver.SemVersion.Parse(_ZWaveJSServerVersion, Semver.SemVersionStyles.Strict).Major != SchemaVersionID.Major)
                    {
                        StartUpError?.Invoke("The Server Schema version is not compatible with the library version");
                        return;
                    }

                    if (Semver.SemVersion.Parse(_ZWaveJSServerVersion, Semver.SemVersionStyles.Strict).ComparePrecedenceTo(SchemaVersionID) < 0)
                    {
                        StartUpError?.Invoke("The Server Schema version is lower than what was requested by the library");
                        return;
             
                    }

                    Guid CBID = Guid.NewGuid();
                    Callbacks.Add(CBID, SetAPIVersionCB);

                    Dictionary<string, object> Request = new Dictionary<string, object>();
                    Request.Add("messageId", CBID.ToString());
                    Request.Add("command", Enums.Commands.SetAPIVersion);
                    Request.Add("schemaVersion", SchemaVersionID.Minor);

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

    }
}
