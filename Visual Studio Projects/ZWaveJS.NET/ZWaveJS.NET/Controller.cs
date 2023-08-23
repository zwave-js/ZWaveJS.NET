﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public delegate void StatisticsUpdatedEvent(ControllerStatisticsUpdatedArgs Args);
        public event StatisticsUpdatedEvent StatisticsUpdated;
        internal void Trigger_StatisticsUpdated(ControllerStatisticsUpdatedArgs Args)
        {
            this.statistics = Args;
            StatisticsUpdated?.Invoke(Args);
        }

        public delegate void HealNetworkProgressEvent(NetworkHealProgressArgs Args);
        public event HealNetworkProgressEvent HealNetworkProgress;
        internal void Trigger_HealNetworkProgress(NetworkHealProgressArgs Args)
        {
             HealNetworkProgress?.Invoke(Args);
        }

        public delegate void HealNetworkDoneEvent(NetworkHealDoneArgs Args);
        public event HealNetworkDoneEvent HealNetworkDone;
        internal void Trigger_HealNetworkDone(NetworkHealDoneArgs Args)
        {
            this.isHealNetworkActive = false;
            HealNetworkDone?.Invoke(Args);
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

        public delegate void NodeRemovedEvent(ZWaveNode Node, Enums.RemoveNodeReason Reason);
        public event NodeRemovedEvent NodeRemoved;
        internal void Trigger_NodeRemoved(ZWaveNode Node, Enums.RemoveNodeReason Reason)
        {
            NodeRemoved?.Invoke(Node, Reason);
        }

        public delegate void NodeAddedEvent(ZWaveNode Node, InclusionResultArgs Args);
        public event NodeAddedEvent NodeAdded;
        internal void Trigger_NodeAdded(ZWaveNode Node, InclusionResultArgs Args)
        {
            NodeAdded?.Invoke(Node, Args);
        }

        public delegate void FirmwareUpdateFinishedEvent(ControllerFirmwareUpdateResultArgs Args);
        public event FirmwareUpdateFinishedEvent FirmwareUpdateFinished;
        internal void Trigger_FirmwareUpdateFinished(ControllerFirmwareUpdateResultArgs Args)
        {
            FirmwareUpdateFinished?.Invoke(Args);
        }
        
        public delegate void FirmwareUpdateProgressEvent(ControllerFirmwareUpdateProgressArgs Args);
        public event FirmwareUpdateProgressEvent FirmwareUpdateProgress;
        internal void Trigger_FirmwareUpdateProgress(ControllerFirmwareUpdateProgressArgs Args)
        {
            FirmwareUpdateProgress?.Invoke(Args);
        }

        public Task<CMDResult> GetRFRegion()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);

                if (Res.Success)
                {
                    int Region = JO.SelectToken("result.region").Value<int>();
                    Res.SetPayload((Enums.RFRegion)Region);
                }
                Result.SetResult(Res);

            });
            
            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.GetRFRegion);
            
            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> SetRFRegion(Enums.RFRegion Region)
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
            Request.Add("command", Enums.Commands.SetRFRegion);
            Request.Add("region", Region);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> GetPowerLevel()
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);

                if (Res.Success)
                {
                    PowerLevel Level = JO.SelectToken("result").Value<PowerLevel>();
                    Res.SetPayload(Level);
                }
                Result.SetResult(Res);

            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.GetPowerlevel);


            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> SetPowerLevel(decimal PowerLevel, decimal Measured0dBm)
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
            Request.Add("command", Enums.Commands.SetPowerlevel);
            Request.Add("powerlevel", PowerLevel);
            Request.Add("measured0dBm", Measured0dBm);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

       



        public VirtualNode GetMulticastGroup(int[] Nodes)
        {
            VirtualNode VN = new VirtualNode(Nodes);
            return VN;
        }

        // FIXME
        public Task<CMDResult> FirmwareUpdateOTW(byte[] Data, string FileFormat)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);

                if (Res.Success)
                {
                    SmartStartProvisioningEntry[] Entries = JsonConvert.DeserializeObject<SmartStartProvisioningEntry[]>(JO.SelectToken("result.entries").ToString());
                    Res.SetPayload(Entries);
                }
                Result.SetResult(Res);

            });


            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.FirmwareUpdateOTW);
            Request.Add("file", Data);
            Request.Add("fileFormat", FileFormat);
            
            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> GetProvisioningEntries()
        {
            Guid ID = Guid.NewGuid();
            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            Driver.Instance.Callbacks.Add(ID, (JO) =>
             {
                 CMDResult Res = new CMDResult(JO);

                 if (Res.Success)
                 {
                     SmartStartProvisioningEntry[] Entries = JsonConvert.DeserializeObject<SmartStartProvisioningEntry[]>(JO.SelectToken("result.entries").ToString());
                     Res.SetPayload(Entries);
                 }
                 Result.SetResult(Res);

             });


            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.GetProvisioningEntries);



            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> RemoveAssociations(AssociationAddress Source, int Group, AssociationAddress[] Targets)
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
            Request.Add("command", Enums.Commands.RemoveAssociations);
            Request.Add("nodeId", Source.nodeId);
            Request.Add("endpoint", Source.endpoint);
            Request.Add("group", Group);
            Request.Add("associations", Targets);


            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
           Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> AddAssociations(AssociationAddress Source, int Group, AssociationAddress[] Targets)
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
            Request.Add("command", Enums.Commands.AddAssociations);
            Request.Add("nodeId", Source.nodeId);
            Request.Add("endpoint", Source.endpoint);
            Request.Add("group", Group);
            Request.Add("associations", Targets);


            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
           Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> GetAssociations(int Node, int Endpoint)
        {
            Guid ID = Guid.NewGuid();
            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
           Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Dictionary<int, AssociationAddress[]> Associations = JsonConvert.DeserializeObject<Dictionary<int, AssociationAddress[]>>(JO.SelectToken("result.associations").ToString());
                    Res.SetPayload(Associations);
                }

                Result.SetResult(Res);

            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.GetAssociations);
            Request.Add("nodeId", Node);
            Request.Add("endpoint", Endpoint);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
           Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> GetAssociationGroups(int Node, int Endpoint)
        {
            Guid ID = Guid.NewGuid();
            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
           Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    Dictionary<int,AssociationGroup> Groups = JsonConvert.DeserializeObject<Dictionary<int, AssociationGroup>>(JO.SelectToken("result.groups").ToString());
                    Res.SetPayload(Groups);
                }

                Result.SetResult(Res);

            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.GetAssociationGroups);
            Request.Add("nodeId",Node);
            Request.Add("endpoint", Endpoint);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
           Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> RestoreNVM(byte[] NVMData, ConvertRestoreNVMProgress ConvertProgress = null, RestoreNVMProgress RestoreProgress = null)
        {
            ConvertRestoreNVMProgressSub = ConvertProgress;
            RestoreNVMProgressSub = RestoreProgress;

            Guid ID = Guid.NewGuid();
            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
           Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                   Driver.Instance.Restart();
                }
                Result.SetResult(Res);

            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.RestoreNVM);
            Request.Add("nvmData", Convert.ToBase64String(NVMData));

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
           Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> BackupNVMRaw(BackupNVMProgress OnProgress = null)
        {
            BackupNVMProgressSub = OnProgress;

            Guid ID = Guid.NewGuid();
            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
           Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    string B64 = JO.SelectToken("result.nvmData").ToString();
                    Res.SetPayload(Convert.FromBase64String(B64));
                }

                Result.SetResult(Res);
                
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.BackUpNVM);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
           Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> ReplaceFailedNode(int NodeID, InclusionOptions Options)
        {
            ValidateDSKAndEnterPINSub = null;
            GrantSecurityClassesSub = null;
            AbortSub = null;

            Guid ID = Guid.NewGuid();
            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();
            
            switch (Options.strategy)
            {
                case Enums.InclusionStrategy.Default:
                    CMDResult Res = new CMDResult(Enums.ErrorCodes.InvalidStrategy, "Invalid Strategy for 'ReplaceFailedNode' Valid Strategies are : [Insecure, Security_S0, Security_S2]", false);
                    Result.SetResult(Res);
                    return Result.Task;

                case Enums.InclusionStrategy.Security_S2:
                    ValidateDSKAndEnterPINSub = Options.userCallbacks?.validateDSKAndEnterPIN ?? null;
                    GrantSecurityClassesSub = Options.userCallbacks?.grantSecurityClasses ?? null;
                    AbortSub = Options.userCallbacks?.abort ?? null;
                    break;

            }
            
            if (Options.strategy == Enums.InclusionStrategy.Security_S2)
            {
                if (ValidateDSKAndEnterPINSub == null || GrantSecurityClassesSub == null || AbortSub == null)
                {
                    CMDResult Res = new CMDResult(Enums.ErrorCodes.MissingS2Callbacks, "S2 Security require userCallbacks to be provided [validateDSKAndEnterPIN, grantSecurityClasses, abort]", false);
                    Result.SetResult(Res);
                    return Result.Task;
                }

                if (Driver.Instance.Options != null &&Driver.Instance.Options.MissingKeys(true, false))
                {
                    CMDResult Res = new CMDResult(Enums.ErrorCodes.MissingKeys, "Missing Security Keys in Options", false);
                    Result.SetResult(Res);
                    return Result.Task;
                }
            }

            if(Options.strategy == Enums.InclusionStrategy.Security_S0)
            {
                if (Driver.Instance.Options != null &&Driver.Instance.Options.MissingKeys(false, true))
                {
                    CMDResult Res = new CMDResult(Enums.ErrorCodes.MissingKeys, "Missing Security Keys in Options", false);
                    Result.SetResult(Res);
                    return Result.Task;
                }
            }

            if (Driver.Instance.Options != null && !Driver.Instance.Options.CheckKeyLength())
            {
                CMDResult Res = new CMDResult(Enums.ErrorCodes.InvalidkeyLength, "Invalid Key length. All Security Keys must be a 32 character hexadecimal string (representing 16 bytes)", false);
                Result.SetResult(Res);
                return Result.Task;
            }

            
           Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                Result.SetResult(Res);
            });

            Dictionary<string, object> _Options = new Dictionary<string, object>();
            _Options.Add("strategy", (int)Options.strategy);

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.ReplaceFailedNode);
            Request.Add("nodeId", NodeID);
            Request.Add("options", _Options);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
           Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> RemoveFailedNode(int NodeID)
        {
            Guid ID = Guid.NewGuid();
            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

           Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult RES = new CMDResult(JO);
                Result.SetResult(RES);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();

            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.RemoveFailedNode);
            Request.Add("nodeId", NodeID);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
           Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> HealNode(int NodeID)
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
            Request.Add("command", Enums.Commands.HealNode);
            Request.Add("nodeId", NodeID);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
           Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> BeginHealingNetwork()
        {
            Guid ID = Guid.NewGuid();
            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

           Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    this.isHealNetworkActive = true;
                }

                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();

            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.BeginHealingNetwork);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
           Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> StopHealingNetwork()
        {
            
            Guid ID = Guid.NewGuid();
            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

           Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    this.isHealNetworkActive = false;
                }

                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();

            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.StopHealingNetwork);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
           Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> BeginInclusion(InclusionOptions Options)
        {
            ValidateDSKAndEnterPINSub = null;
            GrantSecurityClassesSub = null;
            AbortSub = null;

            Guid ID = Guid.NewGuid();
            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            switch (Options.strategy)
            {
                case Enums.InclusionStrategy.Default:
                case Enums.InclusionStrategy.Security_S2:
                    ValidateDSKAndEnterPINSub = Options.userCallbacks?.validateDSKAndEnterPIN ?? null;
                    GrantSecurityClassesSub = Options.userCallbacks?.grantSecurityClasses ?? null;
                    AbortSub = Options.userCallbacks?.abort ?? null;
                    break;
            }
            
            if (Options.strategy == Enums.InclusionStrategy.Default)
            {

                if (ValidateDSKAndEnterPINSub == null || GrantSecurityClassesSub == null || AbortSub == null)
                {
                    CMDResult Res = new CMDResult(Enums.ErrorCodes.MissingS2Callbacks, "S2 Security require userCallbacks to be provided [validateDSKAndEnterPIN, grantSecurityClasses, abort]", false);
                    Result.SetResult(Res);
                    return Result.Task;
                }

                if (Driver.Instance.Options != null &&Driver.Instance.Options.MissingKeys(true, true))
                {
                    CMDResult Res = new CMDResult(Enums.ErrorCodes.MissingKeys, "Missing Security Keys in Options", false);
                    Result.SetResult(Res);
                    return Result.Task;
                }
            }

            if (Options.strategy == Enums.InclusionStrategy.Security_S2)
            {

                if (ValidateDSKAndEnterPINSub == null || GrantSecurityClassesSub == null || AbortSub == null)
                {
                    CMDResult Res = new CMDResult(Enums.ErrorCodes.MissingS2Callbacks, "S2 Security require userCallbacks to be provided [validateDSKAndEnterPIN, grantSecurityClasses, abort]", false);
                    Result.SetResult(Res);
                    return Result.Task;
                }

                if (Driver.Instance.Options != null &&Driver.Instance.Options.MissingKeys(true, false))
                {
                    CMDResult Res = new CMDResult(Enums.ErrorCodes.MissingKeys, "Missing Security Keys in Options", false);
                    Result.SetResult(Res);
                    return Result.Task;
                }
            }

            if (Options.strategy == Enums.InclusionStrategy.Security_S0)
            {
                if (Driver.Instance.Options != null &&Driver.Instance.Options.MissingKeys(false, true))
                {
                    CMDResult Res = new CMDResult(Enums.ErrorCodes.MissingKeys, "Missing Security Keys in Options", false);
                    Result.SetResult(Res);
                    return Result.Task;
                }
            }



            if (Driver.Instance.Options != null && !Driver.Instance.Options.CheckKeyLength())
            {
                CMDResult Res = new CMDResult(Enums.ErrorCodes.InvalidkeyLength, "Invalid Key length. All Security Keys must be a 32 character hexadecimal string (representing 16 bytes)", false);
                Result.SetResult(Res);
                return Result.Task;
            }

           Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                Result.SetResult(Res);
            });

            Dictionary<string, object> _Options = new Dictionary<string, object>();
            _Options.Add("strategy", (int)Options.strategy);
            _Options.Add("forceSecurity", Options.forceSecurity);

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.BeginInclusion);
            Request.Add("options", _Options);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
           Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> StopInclusion()
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
            Request.Add("command", Enums.Commands.StopInclusion);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
           Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }
        
        public Task<CMDResult> UnprovisionSmartStartNode(int NodeID)
        {
            return _UnprovisionSmartStartNode(NodeID);
        }

        public Task<CMDResult> UnprovisionSmartStartNode(string DSK)
        {
            return _UnprovisionSmartStartNode(DSK);
        }

        private Task<CMDResult> _UnprovisionSmartStartNode(object dskOrNodeId)
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
            Request.Add("command", Enums.Commands.UnprovisionSmartStartNode);
            Request.Add("dskOrNodeId", dskOrNodeId);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
           Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }


        public Task<CMDResult> ProvisionSmartStartNode(string QRCode)
        {
            Guid ID = Guid.NewGuid();
            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            if(Driver.Instance.Options != null &&Driver.Instance.Options.MissingKeys(true,true))
            {
                CMDResult Res = new CMDResult(Enums.ErrorCodes.MissingKeys, "Missing Security Keys in Options", false);
                Result.SetResult(Res);
                return Result.Task;
            }

            if (Driver.Instance.Options != null && !Driver.Instance.Options.CheckKeyLength())
            {
                CMDResult Res = new CMDResult(Enums.ErrorCodes.InvalidkeyLength, "Invalid Key length. All Security Keys must be a 32 character hexadecimal string (representing 16 bytes)", false);
                Result.SetResult(Res);
                return Result.Task;
            }

           Driver.Instance.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();

            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.ProvisionSmartStartNode);
            Request.Add("entry", QRCode);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
           Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> BeginExclusion(ExclusionOptions Options)
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
            Request.Add("command", Enums.Commands.BeginExclusion);
            Request.Add("options", Options);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        public Task<CMDResult> StopExclusion()
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
            Request.Add("command", Enums.Commands.StopExclusion);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
           Driver.Instance.ClientWebSocket.SendInstant(RequestPL);

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

    }
}
