using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.WebSockets;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;



namespace ZWaveJS.NET
{
    public class Driver
    {
        internal static WSClient Client;
        internal static Dictionary<Guid, Action<JObject>> Callbacks;
        private Dictionary<string, Action<JObject>> NodeEventMap;
        private Dictionary<string, Action<JObject>> ControllerEventMap;
        private static int SchemaVersionID = 17;
        internal static CustomBooleanJsonConverter BoolConverter;

        private string SerialPort;
        private ZWaveOptions Options;
        private Uri WSAddress;
        private bool Host = true;

        internal static bool Inited = false;

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
                NodeStatistics NS = JsonConvert.DeserializeObject<NodeStatistics>(JO.SelectToken("event.statistics").ToString());
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_StatisticsUpdated(NS);
            });

            NodeEventMap.Add("firmware update finished", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                int Status = JO.SelectToken("event.status").Value<int>();
                int Wait = 0;

                if(JO.SelectToken("event.waitTime") != null)
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
                JObject IJO = JO.SelectToken("event.args").Value<JObject>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_ValueUpdated(IJO);
            });

            NodeEventMap.Add("value added", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                JObject IJO = JO.SelectToken("event.args").Value<JObject>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_ValueUpdated(IJO);
            });

            NodeEventMap.Add("value notification", (JO) =>
            {
                int NID = JO.SelectToken("event.nodeId").Value<int>();
                JObject IJO = JO.SelectToken("event.args").Value<JObject>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_ValueNotification(IJO);
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
                ZWaveNode NNI = JsonConvert.DeserializeObject<ZWaveNode>(JO.SelectToken("event.nodeState").ToString(),BoolConverter);

                ZWaveNode N = this.Controller.Nodes.Get(NID);
                this.Controller.Nodes.ReplaceInformation(NNI);
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
                ControllerStatistics CS = JsonConvert.DeserializeObject<ControllerStatistics>(JO.SelectToken("event.statistics").ToString());
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
                this.Controller.Trigger_NodeAdded(NN,IR);
            });

            ControllerEventMap.Add("grant security classes", (JO) =>
            {
                InclusionGrant RIG = JsonConvert.DeserializeObject<InclusionGrant>(JO.SelectToken("event.requested").ToString());
                InclusionGrant SIG = this.Controller.Trigger_GrantSecurityClasses(RIG);

                Dictionary<string, object> Request = new Dictionary<string, object>();
                Request.Add("messageId", Guid.NewGuid().ToString());
                Request.Add("command", Enums.Commands.GrantSecurityClasses);
                Request.Add("inclusionGrant", SIG);

                string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
                Client.Send(RequestPL);
            });

            ControllerEventMap.Add("validate dsk and enter pin", (JO) =>
            {
                string DSK = this.Controller.Trigger_ValidateDSK(JO.SelectToken("event.dsk").Value<string>());

                Dictionary<string, object> Request = new Dictionary<string, object>();
                Request.Add("messageId", Guid.NewGuid().ToString());
                Request.Add("command", Enums.Commands.ValidateDSK);
                Request.Add("pin", DSK);

                string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
                Client.Send(RequestPL);
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

        private void MapEvents()
        {
            NodeEventMap = new Dictionary<string, Action<JObject>>();
            MapNodeEvents();

            ControllerEventMap = new Dictionary<string, Action<JObject>>();
            MapControllerEvents();
        }

        public void Destroy()
        {
            Client.Stop();
            Server.Terminate();
        }

        // Client Mode
        public Driver(Uri Server, int SchemaVersion = 0)
        {
            if(SchemaVersion > 0)
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
            Server.NoneFatalError += Server_NoneFatalError;

            this.SerialPort = SerialPort;
            this.Options = Options;
            this.WSAddress = new Uri("ws://127.0.0.1:" + ServerCommunicationPort);
            this.Host = true;

            InternalPrep();

        }

        private void InternalPrep()
        {
            if (this.Host)
            {
                Server.Start(SerialPort, Options, ServerCommunicationPort);
            }
            
            Client = new WSClient(this.WSAddress);
            Client.MessageReceivedEvent += ProcessMessage;
        }

        private void Server_NoneFatalError()
        {
            Client.Stop();

            Task.Run(async () =>
            {
                System.Threading.Thread.Sleep(1000);
                InternalPrep();
                Start();
            });

            
        }

        private void Server_FatalError()
        {
            Client.Stop();
            Task.Run(async () =>
            {
                StartupErrorEvent?.Invoke("Driver could not start.");
            });
            
        }

        // Start Driver
        public void Start()
        {
            Client.Start();
        }

        // Proces Message
        private void ProcessMessage(WebSocketMessageType _Type, byte[] Data)
        {

            if (_Type == WebSocketMessageType.Text)
            {
                string Content = System.Text.Encoding.UTF8.GetString(Data);
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debug.WriteLine(Content);
                }

                JObject JO = JObject.Parse(Content);

                string Type = JO.Value<string>("type");
                Guid MessageID = JO.ContainsKey("messageId") ? Guid.Parse(JO.Value<string>("messageId")) : Guid.Empty;

                if (MessageID != Guid.Empty)
                {
                    if (Callbacks.ContainsKey(MessageID))
                    {
                        Callbacks[MessageID].Invoke(JO);
                        Callbacks.Remove(MessageID);
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

                    Client.Send(RequestPL);

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

                    }
                    return;
                }
            }
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

                Client.Send(RequestPL);
            }
            else
            {
                string ErrorCode = JO.Value<string>("errorCode");
                switch (ErrorCode)
                {
                    case "schema_incompatible":
                        StartupErrorEvent?.Invoke("Client and Server schema Mismatch");
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
                    ZWaveNode[] Nodes = JsonConvert.DeserializeObject<ZWaveNode[]>(JO.SelectToken("result.state.nodes").ToString(), BoolConverter);

                    this.Controller = C;
                    this.Controller.Nodes = new NodesCollection(Nodes);

                    Inited = true;

                    DriverReady?.Invoke();
                }
            }
        }
    }
}
