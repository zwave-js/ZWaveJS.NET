using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.WebSockets;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Websocket.Client;
using System.Runtime.CompilerServices;
using System.IO;

namespace ZWaveJS.NET
{
    public class Driver
    {
        // Global List of Socket Ports that are registered
        internal static List<int> UsedPorts = new List<int>();

        internal WebsocketClient ClientWebSocket;
        internal Dictionary<Guid, Action<JObject>> Callbacks;
        internal ZWaveOptions Options;
        internal const string FWUSAPIKey = "921f8000486fcc2744721cfc747aab2db8fc025b5d487cbf2eba76e88ff6f79a064644bf";
        internal InclusionUserCallbacks S2Callbacks;
        internal TaskCompletionSource<CMDResult> GetNewTaskCompletionSource(out Guid ID)
        {
            ID = Guid.NewGuid();
            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            return Result;
        }

        private Dictionary<string, Action<JObject>> NodeEventMap;
        private Dictionary<string, Action<JObject>> ControllerEventMap;
        private Dictionary<string, Action<JObject>> DriverEventMap;
        private int _schemaVersion = 44;
        private string SerialPort;
        private bool RequestedExit = false;
        private JsonSerializer _jsonSerializer;
        private Uri WSAddress;
        private bool Host = true;
        private Server _server;
        private DateTime ConnectStart;
        private bool Inited = false;


        public string ZWaveJSDriverVersion { get; internal set; }
        public string ZWaveJSServerVersion { get; internal set; }
        public int ServerSchemaVersion { get; internal set; }
        public int ServerCommunicationPort { get; private set; }
        public bool IsHostedMode { get { return Host; } }

        public Controller Controller { get; internal set; }
        public Utils Utils { get; internal set; }

        public ConfigManager ConfigManager { get; internal set; }

        public delegate void DriverReadyEvent();
        public event DriverReadyEvent DriverReady;

        public delegate void ServerConnectionErrorEvent(string ErrorCode, string Message, Action<bool, int?> Retry);
        public event ServerConnectionErrorEvent ServerConnectionError;

        public delegate void LoggingEventDelegate(LoggingEventArgs args);
        public event LoggingEventDelegate LoggingEvent;
        internal void Trigger_LoggingEvent(LoggingEventArgs args)
        {
            LoggingEvent?.Invoke(args);
        }

        private void MapNodeEvents()
        {

            NodeEventMap.Add("interview stage completed", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").ToObject<int>();
                string Stage = JO.SelectToken("event.stageName").ToObject<string>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                N.Trigger_InterviewStageCompleted(Stage);
            });

            NodeEventMap.Add("node info received", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").ToObject<int>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                N.Trigger_NodeInfo();
            });

            NodeEventMap.Add("check lifeline health progress", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").ToObject<int>();
                int Round = JO.SelectToken("event.round").ToObject<int>();
                int Total = JO.SelectToken("event.totalRounds").ToObject<int>();
                int LastRating = JO.SelectToken("event.lastRating").ToObject<int>();

                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_LifelineHealthCheckProgress(Round, Total, LastRating);
            });

            NodeEventMap.Add("statistics updated", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                NodeStatisticsUpdatedArgs NS = JO.SelectToken("event.statistics").ToObject<NodeStatisticsUpdatedArgs>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                N.Trigger_StatisticsUpdated(NS);
            });

            NodeEventMap.Add("firmware update finished", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                NodeFirmwareUpdateResultArgs Result = JO.SelectToken("event.result").ToObject<NodeFirmwareUpdateResultArgs>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                N.Trigger_FirmwareUpdateFinished(Result);
            });


            NodeEventMap.Add("firmware update progress", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                NodeFirmwareUpdateProgressArgs Progress = JO.SelectToken("event.progress").ToObject<NodeFirmwareUpdateProgressArgs>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                N.Trigger_FirmwareUpdateProgress(Progress);
            });

            NodeEventMap.Add("value updated", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                ValueUpdatedArgs Args = JO.SelectToken("event.args").ToObject<ValueUpdatedArgs>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                N.Trigger_ValueUpdated(Args);

            });

            NodeEventMap.Add("value added", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                ValueAddedArgs Args = JO.SelectToken("event.args").ToObject<ValueAddedArgs>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                N.Trigger_ValueAdded(Args);

            });

            NodeEventMap.Add("value removed", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                ValueRemovedArgs Args = JO.SelectToken("event.args").ToObject<ValueRemovedArgs>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                N.Trigger_ValueRemoved(Args);

            });

            NodeEventMap.Add("value notification", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                ValueNotificationArgs Args = JO.SelectToken("event.args").ToObject<ValueNotificationArgs>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                N.Trigger_ValueNotification(Args);

            });

            NodeEventMap.Add("notification", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").ToObject<int>();
                int CCID = JO.SelectToken("event.ccId").ToObject<int>();
                JObject IJO = JO.SelectToken("event.args").ToObject<JObject>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                N.Trigger_Notification(CCID, IJO);

            });

            NodeEventMap.Add("alive", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").ToObject<int>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                N.Trigger_NodeAlive();

            });

            NodeEventMap.Add("dead", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").ToObject<int>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                N.Trigger_NodeDead();

            });

            NodeEventMap.Add("wake up", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").ToObject<int>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                N.Trigger_NodeAwake();

            });

            NodeEventMap.Add("sleep", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").ToObject<int>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                N.Trigger_NodeAsleep();

            });

            NodeEventMap.Add("ready", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").ToObject<int>();
                ZWaveNode NNI = JO.SelectToken("event.nodeState").ToObject<ZWaveNode>(_jsonSerializer);

                ZWaveNode N = this.Controller.Nodes.Get(NID);
                this.Controller.Nodes.ReplaceInformation(NNI, N);

                N.Trigger_NodeReady();

            });

            NodeEventMap.Add("interview started", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").ToObject<int>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                N.Trigger_NodeInterviewStarted();

            });

            NodeEventMap.Add("interview completed", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").ToObject<int>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                N.Trigger_NodeInterviewCompleted();

            });

            NodeEventMap.Add("interview failed", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                NodeInterviewFailedEventArgs FII = JO.SelectToken("event.args").ToObject<NodeInterviewFailedEventArgs>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);

                N.Trigger_NodeInterviewFailed(FII);

            });

            NodeEventMap.Add("metadata updated", (JO) =>
           {
               int NID = JO.SelectToken("event.nodeId").Value<int>();
               MetadataUpdatedArgs Args = JO.SelectToken("event.args").ToObject<MetadataUpdatedArgs>();
               ZWaveNode N = this.Controller.Nodes.Get(NID);

               N.Trigger_MetadataUpdated(Args);

           });
        }

        private void MapControllerEvents()
        {
            ControllerEventMap.Add("firmware update finished", (JO) =>
            {
                ControllerFirmwareUpdateResultArgs Result = JO.SelectToken("event.result").ToObject<ControllerFirmwareUpdateResultArgs>();

                this.Controller.Trigger_FirmwareUpdateFinished(Result);


            });


            ControllerEventMap.Add("firmware update progress", (JO) =>
            {
                ControllerFirmwareUpdateProgressArgs Progress = JO.SelectToken("event.progress").ToObject<ControllerFirmwareUpdateProgressArgs>();

                this.Controller.Trigger_FirmwareUpdateProgress(Progress);

            });

            ControllerEventMap.Add("nvm backup progress", (JO) =>
            {
                int Read = JO.SelectToken("event.bytesRead").ToObject<int>();
                int Total = JO.SelectToken("event.total").ToObject<int>();

                this.Controller.Trigger_BackupNVMProgress(Read, Total);

            });

            ControllerEventMap.Add("nvm convert progress", (JO) =>
            {
                int Read = JO.SelectToken("event.bytesRead").ToObject<int>();
                int Total = JO.SelectToken("event.total").ToObject<int>();

                this.Controller.Trigger_ConvertRestoreNVMProgress(Read, Total);

            });

            ControllerEventMap.Add("nvm restore progress", (JO) =>
            {
                int Written = JO.SelectToken("event.bytesWritten").ToObject<int>();
                int Total = JO.SelectToken("event.total").ToObject<int>();

                this.Controller.Trigger_RestoreNVMProgressSub(Written, Total);

            });

            ControllerEventMap.Add("statistics updated", (JO) =>
            {
                ControllerStatisticsUpdatedArgs CS = JO.SelectToken("event.statistics").ToObject<ControllerStatisticsUpdatedArgs>();

                this.Controller.Trigger_StatisticsUpdated(CS);

            });

            ControllerEventMap.Add("inclusion aborted", (JO) =>
            {
                S2Callbacks?.abort?.Invoke();

            });

            ControllerEventMap.Add("inclusion started", (JO) =>
            {
                bool Secure = (JO.SelectToken("event.strategy").ToObject<Enums.InclusionStrategy>() != Enums.InclusionStrategy.Insecure);

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
                int NID = JO.SelectToken("event.node.nodeId").ToObject<int>();
                Enums.RemoveNodeReason Reason = JO.SelectToken("event.reason").ToObject<Enums.RemoveNodeReason>();

                ZWaveNode N = this.Controller.Nodes.Get(NID);
                this.Controller.Nodes.RemoveNodeFromCollection(NID);

                this.Controller.Trigger_NodeRemoved(N, Reason);

            });

            ControllerEventMap.Add("node added", (JO) =>
            {
                int NID = JO.SelectToken("event.node.nodeId").ToObject<int>();
                InclusionResultArgs IR = JO.SelectToken("event.result").ToObject<InclusionResultArgs>();

                ZWaveNode NN = new ZWaveNode(this);
                NN.id = NID;

                this.Controller.Nodes.AddNodeToCollection(NN);

                this.Controller.Trigger_NodeAdded(NN, IR);


            });

            ControllerEventMap.Add("node found", (JO) =>
            {
                int NID = JO.SelectToken("event.node.nodeId").ToObject<int>();

                this.Controller.Trigger_NodeFound(NID);


            });

            ControllerEventMap.Add("grant security classes", (JO) =>
            {

                InclusionGrant RIG = JO.SelectToken("event.requested").ToObject<InclusionGrant>();
                InclusionGrant SIG = S2Callbacks?.grantSecurityClasses?.Invoke(RIG);

                if (SIG == null)
                    return;


                Dictionary<string, object> Request = new Dictionary<string, object>();
                Request.Add("messageId", Guid.NewGuid().ToString());
                Request.Add("command", Enums.Commands.GrantSecurityClasses);
                Request.Add("inclusionGrant", SIG);

                string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
                ClientWebSocket.SendInstant(RequestPL);


            });

            ControllerEventMap.Add("validate dsk and enter pin", (JO) =>
            {

                string DSK = S2Callbacks?.validateDSKAndEnterPIN?.Invoke(JO.SelectToken("event.dsk").ToObject<string>());

                if (DSK == null)
                    return;

                Dictionary<string, object> Request = new Dictionary<string, object>();
                Request.Add("messageId", Guid.NewGuid().ToString());
                Request.Add("command", Enums.Commands.ValidateDSK);
                Request.Add("pin", DSK);

                string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
                ClientWebSocket.SendInstant(RequestPL);

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


                this.Controller.Trigger_RebuildRoutesProgress(Args);

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


                this.Controller.Trigger_RebuildRoutesDone(Args);


            });
        }

        private void MapServerEvents()
        {
            DriverEventMap.Add("logging", (JO) =>
            {
                LoggingEventArgs Args = JO.SelectToken("event").ToObject<LoggingEventArgs>();


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
            MapServerEvents();
        }

        // Client Mode
        public Driver(Uri Server, InclusionUserCallbacks S2Callbacks, int SchemaVersion = 0)
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
                _schemaVersion = SchemaVersion;
            }

            Callbacks = new Dictionary<Guid, Action<JObject>>();
            MapEvents();

            this.S2Callbacks = S2Callbacks;
            this.WSAddress = Server;
            this.Host = false;


        }

        // Host Mode
        public Driver(string SerialPort, ZWaveOptions Options, int ServerCommunicationPort = 50001)
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

            Callbacks = new Dictionary<Guid, Action<JObject>>();
            MapEvents();

            this.SerialPort = SerialPort;
            this.Options = Options;
            this.S2Callbacks = Options.inclusionUserCallbacks;
            this.ServerCommunicationPort = ServerCommunicationPort;
            this.WSAddress = new Uri("ws://localhost:" + ServerCommunicationPort);
            this.Host = true;
            this._server = new Server();

        }

        private async Task HandleDisconnectAsync(DisconnectionInfo info)
        {

            if (info.Type == DisconnectionType.ByUser)
            {
                return; // this is us
            }

            if (info.Type == DisconnectionType.Lost)
            {
                ServerConnectionError?.Invoke(Enums.ErrorCodes.Unknown, "Unexpectedly lost connection to the server.", (retry, timeout) =>
               {
                   SettleCallbacksError();

                   if (retry)
                   {
                       if (timeout.HasValue && timeout.Value > 0)
                       {
                           ClientWebSocket.ConnectTimeout = TimeSpan.FromSeconds(timeout.Value);
                       }
                       else
                       {
                           ClientWebSocket.ConnectTimeout = TimeSpan.FromSeconds(15);
                       }
                       Restart();
                   }
               });

                return;
            }

            TimeSpan elapsed = DateTime.UtcNow - ConnectStart;

            if (elapsed < ClientWebSocket.ConnectTimeout)
            {
                await Task.Delay(1000);
                ClientWebSocket.Reconnect();
                return;
            }

            if (!RequestedExit && info.Type == DisconnectionType.Error)
            {
                ServerConnectionError?.Invoke(
                    Enums.ErrorCodes.WSConnectionTimout,
                    "Could not connect to the ZWaveJS Websocket (timeout)",
                    (retry, timeout) =>
                    {
                        if (retry)
                        {
                            if (timeout.HasValue && timeout.Value > 0)
                            {
                                ClientWebSocket.ConnectTimeout = TimeSpan.FromSeconds(timeout.Value);
                            }
                            else
                            {
                                ClientWebSocket.ConnectTimeout = TimeSpan.FromSeconds(15);
                            }
                            ConnectStart = DateTime.UtcNow;
                            ClientWebSocket.Reconnect();
                        }
                    });
            }
        }


        // Server Process Exit Unexpected
        private void Server_Exited()
        {
            if (!RequestedExit)
            {
                Inited = false;
                Controller.Nodes = null;
                Controller = null;

                DestroySocket();

                ServerConnectionError?.Invoke(Enums.ErrorCodes.Unknown, "The Server process unexpectedly terminted.", (retry, timeout) =>
                {
                    SettleCallbacksError();

                    if (retry)
                    {
                        if (timeout.HasValue && timeout.Value > 0)
                        {
                            ClientWebSocket.ConnectTimeout = TimeSpan.FromSeconds(timeout.Value);
                        }
                        else
                        {
                            ClientWebSocket.ConnectTimeout = TimeSpan.FromSeconds(15);
                        }
                        Restart();
                    }
                });

            }
        }

        // Prep
        private void InternalPrep()
        {
            if (UsedPorts.Contains(ServerCommunicationPort))
            {
                throw new Exception(string.Format("Web Socket Port: {0} already in use by a driver instance.", ServerCommunicationPort));
            }

            if (this.Host)
            {

                _server.Start(SerialPort, Options, ServerCommunicationPort);
                _server.Exited += Server_Exited;
                _server.FatalError += Server_FatalError;
            }

            UsedPorts.Add(ServerCommunicationPort);

            var Factory = new Func<ClientWebSocket>(() => new ClientWebSocket
            {
                Options = {
                    KeepAliveInterval = TimeSpan.FromSeconds(5),
                    RemoteCertificateValidationCallback = (sender, cert, chain, errors) => true
                }
            });

            ClientWebSocket = new Websocket.Client.WebsocketClient(this.WSAddress, Factory);
            ClientWebSocket.ConnectTimeout = TimeSpan.FromSeconds(15);


            ClientWebSocket.MessageReceived.Subscribe((Message) =>
            {
                WebsocketClient_MessageReceived(ClientWebSocket, Message);
            });

            ClientWebSocket.DisconnectionHappened.Subscribe((info) =>
            {
                _ = HandleDisconnectAsync(info);
            });


            ClientWebSocket.ReconnectTimeout = null; // Dont attempt to reconnect when quite
            ClientWebSocket.ErrorReconnectTimeout = null; // disable automatic

        }

        // Start Driver
        public void Start()
        {
            if (ServerConnectionError == null)
            {
                throw new NullReferenceException("The consuming applciation, must subscribe to the Driver.ServerConnectionError event.");
            }

            try
            {
                InternalPrep();
            }
            catch (Exception Error)
            {
                ServerConnectionError?.Invoke(Enums.ErrorCodes.StartUpError, Error.Message, null);
                return;
            }

            RequestedExit = false;
            ConnectStart = DateTime.UtcNow;
            ClientWebSocket.Start();
        }

        private void DestroySocket()
        {
            if (ClientWebSocket != null)
            {
                if (ClientWebSocket.IsRunning)
                {
                    _ = ClientWebSocket.Stop(WebSocketCloseStatus.NormalClosure, "Destroy");
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
            if (Controller != null)
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
                JO.Add("zwaveErrorCode", Enums.ErrorCodes.Unknown);
                JO.Add("zwaveErrorMessage", "The Server process unexpectedly terminted. It is unknown if the command was successfull, assuming false. Subscribe to the 'ServerConnectionError' event of the driver to restart the Driver Runtime");

                // Guard against race condition
                try
                {
                    Callbacks[ID].Invoke(JO);
                    Callbacks.Remove(ID);
                }
                catch (Exception)
                {
                    continue;
                }

            }
        }

        private void Server_FatalError()
        {
            RequestedExit = true;
            Inited = false;
            if (Controller != null)
            {
                Controller.Nodes = null;
                Controller = null;
            }
            DestroySocket();
            DestroyServer();
            ServerConnectionError?.Invoke(Enums.ErrorCodes.StartUpError, "ZWaveJS Server (OR Driver) Error. Enable driver logging to find out more.", null);
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

                    this.Utils = new Utils(this);
                    this.ConfigManager = new ConfigManager(this);

                    Inited = true;

                    DriverReady?.Invoke();
                }
            }
        }

        public Task<CMDResult> StartListeningLogs()
        {
            Guid ID;
            TaskCompletionSource<CMDResult> Result = GetNewTaskCompletionSource(out ID);

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
            Guid ID;
            TaskCompletionSource<CMDResult> Result = GetNewTaskCompletionSource(out ID);

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
            Guid ID;
            TaskCompletionSource<CMDResult> Result = GetNewTaskCompletionSource(out ID);

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
            Guid ID;
            TaskCompletionSource<CMDResult> Result = GetNewTaskCompletionSource(out ID);

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
                        ServerConnectionError?.Invoke(Enums.ErrorCodes.SchemaMisMatch, "Client and Server schema mismatch", null);
                        break;

                }
            }
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
                    if (Callbacks.TryGetValue(MessageID, out var callback))
                    {
                        Callbacks.Remove(MessageID);
                        _ = Task.Run(() =>
                        {
                            try
                            {
                                callback(JO);
                            }
                            catch { }
                        });
                        return;
                    }
                    return;
                }

                if (Type == "version")
                {
                    ZWaveJSDriverVersion = JO.Value<string>("driverVersion");
                    ZWaveJSServerVersion = JO.Value<string>("serverVersion");
                    ServerSchemaVersion = JO.Value<int>("maxSchemaVersion");

                    Guid CBID = Guid.NewGuid();
                    Callbacks.Add(CBID, SetAPIVersionCB);

                    Dictionary<string, object> Request = new Dictionary<string, object>();
                    Request.Add("messageId", CBID.ToString());
                    Request.Add("command", Enums.Commands.SetAPIVersion);
                    Request.Add("schemaVersion", _schemaVersion);

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
                            if (NodeEventMap.TryGetValue(_Event, out var NodeCB))
                            {
                                _ = Task.Run(() =>
                                {
                                    try
                                    {
                                        NodeCB(JO);
                                    }
                                    catch { }
                                });
                            }
                            break;

                        case "controller":
                            if (ControllerEventMap.TryGetValue(_Event, out var ControllerCB))
                            {
                                _ = Task.Run(() =>
                                {
                                    try
                                    {
                                        ControllerCB(JO);
                                    }
                                    catch { }
                                });
                            }
                            break;

                        case "driver":
                            if (DriverEventMap.TryGetValue(_Event, out var DriverCB))
                            {
                                _ = Task.Run(() =>
                                {
                                    try
                                    {
                                        DriverCB(JO);
                                    }
                                    catch { }
                                });
                            }
                            break;

                    }
                    return;
                }
            }
        }

    }
}
