﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Websocket.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace ZWaveJS.NET
{
    public class Driver
    {
        internal static WebsocketClient Client;
        internal static Dictionary<Guid, Action<JObject>> Callbacks;
        private Dictionary<string, Action<JObject>> NodeEventMap;
        private Dictionary<string, Action<JObject>> ControllerEventMap;
        private static int SchemaVersionID = 14;
        internal static CustomBooleanJsonConverter BoolConverter;

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

        private void MapNodeEvents()
        {
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
                ZWaveNode NNI = JsonConvert.DeserializeObject<ZWaveNode>(JO.SelectToken("event.nodeState").ToString());

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
                JObject IJO = JO.SelectToken("event.args").Value<JObject>();
                ZWaveNode N = this.Controller.Nodes.Get(NID);
                N.Trigger_NodeInterviewFailed(IJO);
            });
        }

        private void MapControllerEvents()
        {
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
                this.Controller.Nodes.RemoveNodeFromCollection(NID);
                this.Controller.Trigger_NodeRemoved(NID);
            });

            ControllerEventMap.Add("node added", (JO) =>
            {
                int NID = JO.SelectToken("event.node.nodeId").Value<int>();

                ZWaveNode NN = new ZWaveNode();
                NN.id = NID;

                this.Controller.Nodes.AddNodeToCollection(NN);
                this.Controller.Trigger_NodeAdded(NN);
            });

            ControllerEventMap.Add("grant security classes", (JO) =>
            {
                Enums.SecurityClass[] RequestedClasses = JsonConvert.DeserializeObject<Enums.SecurityClass[]>(JO.SelectToken("event.requested.securityClasses").ToString());
                bool CSA = JO.SelectToken("event.requested.clientSideAuth").Value<bool>();

                InclusionGrant GSCs = this.Controller.Trigger_GrantSecurityClasses(RequestedClasses, CSA);

                Dictionary<string, object> Request = new Dictionary<string, object>();
                Request.Add("messageId", Guid.NewGuid().ToString());
                Request.Add("command", Enums.Commands.GrantSecurityClasses);
                Request.Add("inclusionGrant", GSCs);

                string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
                Client.Send(RequestPL);
            });

            ControllerEventMap.Add("validate dsk and enter pin", (JO) =>
            {
                string DSK = this.Controller.Trigger_ValidateDSK(JO.SelectToken("event.dsk").Value<string>());

                Dictionary<string, object> Request = new Dictionary<string, object>();
                Request.Add("messageId", Guid.NewGuid().ToString());
                Request.Add("command", Enums.Commands.ValidateDSK);
                Request.Add("pin", DSK); ;

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

        // Client Mode
        public Driver(Uri Server, int SchemaVersion = 0)
        {
            if(SchemaVersion > 0)
            {
                SchemaVersionID = SchemaVersion;
            }

            Callbacks = new Dictionary<Guid, Action<JObject>>();
            BoolConverter = new CustomBooleanJsonConverter();
            MapEvents();

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
                Server.FatalError += Server_FatalError;
                Server.Start(SerialPort, Options, ServerCommunicationPort);

                Client = new WebsocketClient(new Uri("ws://127.0.0.1:" + ServerCommunicationPort));
                Client.ReconnectTimeout = null;
                Client.ErrorReconnectTimeout = TimeSpan.Parse("00:00:05");

                Client.MessageReceived.Subscribe(ProcessMessage);
            }
            catch
            {
                throw;
            }
        }

        private void Server_FatalError()
        {
            throw new Exception("Unrecoverable server error");
        }

        // Start Driver
        public void Start()
        {
            Client.Start();
        }

        // Proces Message
        private void ProcessMessage(ResponseMessage IncomingMessage)
        {

            if (IncomingMessage.MessageType == System.Net.WebSockets.WebSocketMessageType.Text)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debug.WriteLine(IncomingMessage.Text);
                }

                JObject JO = JObject.Parse(IncomingMessage.Text);

                string Type = JO.Value<string>("type");
                string Source = JO.Value<string>("source");
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
                    string _Event = JO.SelectToken("event.event").Value<string>();

                    switch (Source)
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
