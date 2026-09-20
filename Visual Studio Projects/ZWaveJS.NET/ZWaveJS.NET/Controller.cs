using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static ZWaveJS.NET.Enums;
using System.Linq;

namespace ZWaveJS.NET
{
    public class Controller
    {
        private Driver _driver;
        internal Controller(Driver driver)
        {
            _driver = driver;
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

        public delegate void StatusChangedEvent(Enums.ControllerStatus Status);
        public event StatusChangedEvent StatusChanged;
        internal void Trigger_StatusChanged(Enums.ControllerStatus Status)
        {
            this.status = Status;
            StatusChanged?.Invoke(Status);
        }

        public delegate void RebuildRoutesProgressEvent(RebuildRoutesProgressArgs Args);
        public event RebuildRoutesProgressEvent RebuildRoutesProgress;
        internal void Trigger_RebuildRoutesProgress(RebuildRoutesProgressArgs Args)
        {
             RebuildRoutesProgress?.Invoke(Args);
        }

        public delegate void RebuildRoutesDoneEvent(RebuildRoutesDoneArgs Args);
        public event RebuildRoutesDoneEvent RebuildRoutesDone;
        internal void Trigger_RebuildRoutesDone(RebuildRoutesDoneArgs Args)
        {
            this.isRebuildingRoutes = false;
            RebuildRoutesDone?.Invoke(Args);
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

        public delegate void NodeFoundEvent(int NodeID);
        public event NodeFoundEvent NodeFound;
        internal void Trigger_NodeFound(int NodeID)
        {
            NodeFound?.Invoke(NodeID);
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

        // Checked as of : 3.5.0
        public Task<CMDResult> GetAvailableFirmwareUpdates(int NodeID, bool IncludePrereleases, UsageEnvironment Environment, string APIKey = null)
        {
            if (Environment == UsageEnvironment.Commercial && string.IsNullOrEmpty(APIKey))
            {
                CMDResult Res = new CMDResult(Enums.ErrorCodes.CommercialAPIKey, "A valid API license key is required for commercial use", false);
                return Task.FromResult(Res);
            }

            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.GetAvailableFirmwareUpdates },
                { "nodeId", NodeID },
                { "apiKey", APIKey ?? Driver.FWUSAPIKey },
                { "includePrereleases", IncludePrereleases }
            };

            return _driver.SendRequestAsync(request, (JO, Res) =>
            {
                if (Res.Success)
                {
                    FirmwareUpdateInfo[] FUI = JO.SelectToken("result.updates").ToObject<FirmwareUpdateInfo[]>();
                    Res.SetPayload(FUI);
                }
            });
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> FirmwareUpdateOTA(int NodeID, FirmwareUpdateInfo Update)
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.FirmwareUpdateOTA },
                { "nodeId", NodeID },
                { "updateInfo", Update }
            };

            return _driver.SendRequestAsync(request, (JO, Res) =>
            {
                if (Res.Success)
                {
                    NodeFirmwareUpdateResultArgs FUR = JO.SelectToken("result.result").ToObject<NodeFirmwareUpdateResultArgs>();
                    Res.SetPayload(FUR);
                }
            });
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> GetRFRegion()
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.GetRFRegion }
            };

            return _driver.SendRequestAsync(request, (JO, Res) =>
            {
                if (Res.Success)
                {
                    Enums.RFRegion Region = JO.SelectToken("result.region").ToObject<Enums.RFRegion>();
                    Res.SetPayload(Region);
                }
            });
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> SetRFRegion(Enums.RFRegion Region)
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.SetRFRegion },
                { "region", Region }
            };

            return _driver.SendRequestAsync(request);
        }
        
        // Checked as of : 3.5.0
        public Task<CMDResult> SetMaxLongRangePowerlevel(decimal Limit)
        {
            var request = new Dictionary<string, object>
            {
                { "limit", Limit },
                { "command", Enums.Commands.SetLRMaxPower }
            };

            return _driver.SendRequestAsync(request);
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> GetMaxLongRangePowerlevel()
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.GetLRMaxPower }
            };

            return _driver.SendRequestAsync(request, (JO, Res) =>
            {
                if (Res.Success)
                {
                    decimal Level = JO.SelectToken("result.limit").ToObject<decimal>();
                    Res.SetPayload(Level);
                }
            });
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> GetPowerLevel()
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.GetPowerlevel }
            };

            return _driver.SendRequestAsync(request, (JO, Res) =>
            {
                if (Res.Success)
                {
                    PowerLevel Level = JO.SelectToken("result").ToObject<PowerLevel>();
                    Res.SetPayload(Level);
                }
            });
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> SetPowerLevel(decimal PowerLevel, decimal Measured0dBm)
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.SetPowerlevel },
                { "powerlevel", PowerLevel },
                { "measured0dBm", Measured0dBm }
            };

            return _driver.SendRequestAsync(request);
        }

      


        // Checked as of : 3.5.0
        public Task<CMDResult> FirmwareUpdateOTW(FirmwareUpdate Update)
        {
            if (Update.firmwareTarget != null)
            {
                CMDResult Res = new CMDResult(Enums.ErrorCodes.WrongOverride, "Please use the override that DOES NOT include 'firmwareTarget'", false);
                return Task.FromResult(Res);
            }

            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.FirmwareUpdateOTW },
                { "file", Update.data },
                { "filename", Update.filename }
            };

            return _driver.SendRequestAsync(request, (JO, Res) =>
            {
                if (Res.Success)
                {
                    ControllerFirmwareUpdateResultArgs UpdateResult = JO.SelectToken("result.result").ToObject<ControllerFirmwareUpdateResultArgs>();
                    Res.SetPayload(UpdateResult);
                }
            });
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> GetProvisioningEntries()
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.GetProvisioningEntries }
            };

            return _driver.SendRequestAsync(request, (JO, Res) =>
            {
                if (Res.Success)
                {
                    SmartStartProvisioningEntry[] Entries = JO.SelectToken("result.entries").ToObject<SmartStartProvisioningEntry[]>();
                    Res.SetPayload(Entries);
                }
            });
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> ToggleRF(bool Enabled)
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.ToggleRF },
                { "enabled", Enabled }
            };

            return _driver.SendRequestAsync(request);
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> RemoveAssociations(AssociationAddress Source, int Group, AssociationAddress[] Targets)
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.RemoveAssociations },
                { "nodeId", Source.nodeId },
                { "endpoint", Source.endpoint },
                { "group", Group },
                { "associations", Targets }
            };

            return _driver.SendRequestAsync(request);
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> AddAssociations(AssociationAddress Source, int Group, AssociationAddress[] Targets)
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.AddAssociations },
                { "nodeId", Source.nodeId },
                { "endpoint", Source.endpoint },
                { "group", Group },
                { "associations", Targets }
            };

            return _driver.SendRequestAsync(request);
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> GetAssociations(int Node, int Endpoint)
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.GetAssociations },
                { "nodeId", Node },
                { "endpoint", Endpoint }
            };

            return _driver.SendRequestAsync(request, (JO, Res) =>
            {
                if (Res.Success)
                {
                    Dictionary<int, AssociationAddress[]> Associations = JO.SelectToken("result.associations").ToObject<Dictionary<int, AssociationAddress[]>>();
                    Res.SetPayload(Associations);
                }
            });
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> GetAssociationGroups(int Node, int Endpoint)
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.GetAssociationGroups },
                { "nodeId", Node },
                { "endpoint", Endpoint }
            };

            return _driver.SendRequestAsync(request, (JO, Res) =>
            {
                if (Res.Success)
                {
                    Dictionary<int, AssociationGroup> Groups = JO.SelectToken("result.groups").ToObject<Dictionary<int, AssociationGroup>>();
                    Res.SetPayload(Groups);
                }
            });
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> RestoreNVM(byte[] NVMData, ConvertRestoreNVMProgress ConvertProgress = null, RestoreNVMProgress RestoreProgress = null)
        {
            ConvertRestoreNVMProgressSub = ConvertProgress;
            RestoreNVMProgressSub = RestoreProgress;

            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.RestoreNVM },
                { "nvmData", Convert.ToBase64String(NVMData) }
            };

            return _driver.SendRequestAsync(request, (JO, Res) =>
            {
                if (Res.Success)
                {
                    _driver.Restart();
                }
            });
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> BackupNVMRaw(BackupNVMProgress OnProgress = null)
        {
            BackupNVMProgressSub = OnProgress;

            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.BackUpNVM }
            };

            return _driver.SendRequestAsync(request, (JO, Res) =>
            {
                if (Res.Success)
                {
                    string B64 = JO.SelectToken("result.nvmData").ToObject<string>();
                    Res.SetPayload(Convert.FromBase64String(B64));
                }
            });
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> ReplaceFailedNode(int NodeID, InclusionOptions Options)
        {
            ValidateDSKAndEnterPINSub = null;
            GrantSecurityClassesSub = null;
            AbortSub = null;

            switch (Options.strategy)
            {
                case Enums.InclusionStrategy.Default:
                    CMDResult invalid = new CMDResult(Enums.ErrorCodes.InvalidStrategy, "Invalid Strategy for 'ReplaceFailedNode' Valid Strategies are : [Insecure, Security_S0, Security_S2]", false);
                    return Task.FromResult(invalid);

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
                    return Task.FromResult(Res);
                }

                if (_driver.Options != null && _driver.Options.MissingKeys(true, false))
                {
                    CMDResult Res = new CMDResult(Enums.ErrorCodes.MissingKeys, "Missing Security Keys in Options", false);
                    return Task.FromResult(Res);
                }
            }

            if (Options.strategy == Enums.InclusionStrategy.Security_S0)
            {
                if (_driver.Options != null && _driver.Options.MissingKeys(false, true))
                {
                    CMDResult Res = new CMDResult(Enums.ErrorCodes.MissingKeys, "Missing Security Keys in Options", false);
                    return Task.FromResult(Res);
                }
            }

            if (_driver.Options != null && !_driver.Options.CheckKeyLength())
            {
                CMDResult Res = new CMDResult(Enums.ErrorCodes.InvalidkeyLength, "Invalid Key length. All Security Keys must be a 32 character hexadecimal string (representing 16 bytes)", false);
                return Task.FromResult(Res);
            }

            var optionsDict = new Dictionary<string, object>
            {
                { "strategy", (int)Options.strategy }
            };

            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.ReplaceFailedNode },
                { "nodeId", NodeID },
                { "options", optionsDict }
            };

            return _driver.SendRequestAsync(request);
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> RemoveFailedNode(int NodeID)
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.RemoveFailedNode },
                { "nodeId", NodeID }
            };

            return _driver.SendRequestAsync(request);
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> RebuildNodeRoutes(int NodeID)
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.RebuildNodeRoutes },
                { "nodeId", NodeID }
            };

            return _driver.SendRequestAsync(request);
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> BeginRebuildingRoutes(RebuildRoutesOptions Options)
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.BeginRebuildingRoutes },
                { "options", Options }
            };

            return _driver.SendRequestAsync(request, (JO, Res) =>
            {
                if (Res.Success && Res.ResultPayloadAs<bool>())
                {
                    this.isRebuildingRoutes = true;
                }
            });
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> StopRebuildingRoutes()
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.StopRebuildingRoutes }
            };

            return _driver.SendRequestAsync(request, (JO, Res) =>
            {
                if (Res.Success && Res.ResultPayloadAs<bool>())
                {
                    this.isRebuildingRoutes = false;
                }
            });
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> BeginInclusion(InclusionOptions Options)
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

            if (Options.strategy == Enums.InclusionStrategy.Default)
            {

                if (ValidateDSKAndEnterPINSub == null || GrantSecurityClassesSub == null || AbortSub == null)
                {
                    CMDResult Res = new CMDResult(Enums.ErrorCodes.MissingS2Callbacks, "S2 Security require userCallbacks to be provided [validateDSKAndEnterPIN, grantSecurityClasses, abort]", false);
                    return Task.FromResult(Res);
                }

                if (_driver.Options != null && _driver.Options.MissingKeys(true, true))
                {
                    CMDResult Res = new CMDResult(Enums.ErrorCodes.MissingKeys, "Missing Security Keys in Options", false);
                    return Task.FromResult(Res);
                }
            }

            if (Options.strategy == Enums.InclusionStrategy.Security_S2)
            {

                if (ValidateDSKAndEnterPINSub == null || GrantSecurityClassesSub == null || AbortSub == null)
                {
                    CMDResult Res = new CMDResult(Enums.ErrorCodes.MissingS2Callbacks, "S2 Security require userCallbacks to be provided [validateDSKAndEnterPIN, grantSecurityClasses, abort]", false);
                    return Task.FromResult(Res);
                }

                if (_driver.Options != null && _driver.Options.MissingKeys(true, false))
                {
                    CMDResult Res = new CMDResult(Enums.ErrorCodes.MissingKeys, "Missing Security Keys in Options", false);
                    return Task.FromResult(Res);
                }
            }

            if (Options.strategy == Enums.InclusionStrategy.Security_S0)
            {
                if (_driver.Options != null && _driver.Options.MissingKeys(false, true))
                {
                    CMDResult Res = new CMDResult(Enums.ErrorCodes.MissingKeys, "Missing Security Keys in Options", false);
                    return Task.FromResult(Res);
                }
            }
            
            if (_driver.Options != null && !_driver.Options.CheckKeyLength())
            {
                CMDResult Res = new CMDResult(Enums.ErrorCodes.InvalidkeyLength, "Invalid Key length. All Security Keys must be a 32 character hexadecimal string (representing 16 bytes)", false);
                return Task.FromResult(Res);
            }

            var optionsDict = new Dictionary<string, object>
            {
                { "strategy", (int)Options.strategy },
                { "forceSecurity", Options.forceSecurity }
            };

            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.BeginInclusion },
                { "options", optionsDict }
            };

            return _driver.SendRequestAsync(request);
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> StopInclusion()
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.StopInclusion }
            };

            return _driver.SendRequestAsync(request);
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> ProvisionSmartStartNode(SmartStartProvisioningEntry ProvisioningInformation)
        {
            if (_driver.Options != null && _driver.Options.MissingKeys(true, true))
            {
                CMDResult Res = new CMDResult(Enums.ErrorCodes.MissingKeys, "Missing Security Keys in Options", false);
                return Task.FromResult(Res);
            }

            if (_driver.Options != null && !_driver.Options.CheckKeyLength())
            {
                CMDResult Res = new CMDResult(Enums.ErrorCodes.InvalidkeyLength, "Invalid Key length. All Security Keys must be a 32 character hexadecimal string (representing 16 bytes)", false);
                return Task.FromResult(Res);
            }

            if (ProvisioningInformation.protocol == Protocols.ZWaveLongRange)
            {
                if (_driver.Options != null && _driver.Options.MissingLRKeys())
                {
                    CMDResult Res = new CMDResult(Enums.ErrorCodes.MissingKeys, "Missing LR Security Keys in Options", false);
                    return Task.FromResult(Res);
                }


                if (_driver.Options != null && !_driver.Options.CheckKeyLengthLR())
                {
                    CMDResult Res = new CMDResult(Enums.ErrorCodes.InvalidkeyLength, "Invalid Key length. All Security Keys must be a 32 character hexadecimal string (representing 16 bytes)", false);
                    return Task.FromResult(Res);
                }
            }

            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.ProvisionSmartStartNode },
                { "entry", ProvisioningInformation }
            };

            return _driver.SendRequestAsync(request);
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> BeginExclusion(ExclusionOptions Options)
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.BeginExclusion },
                { "options", Options }
            };

            return _driver.SendRequestAsync(request);
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> StopExclusion()
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.StopExclusion }
            };

            return _driver.SendRequestAsync(request);
        }
        
        // Checked as of : 3.5.0
        private Task<CMDResult> _UnprovisionSmartStartNode(object dskOrNodeId)
        {
            var request = new Dictionary<string, object>
            {
                { "command", Enums.Commands.UnprovisionSmartStartNode },
                { "dskOrNodeId", dskOrNodeId }
            };

            return _driver.SendRequestAsync(request);
        }
        
        // LOCAL
        public Task<CMDResult> UnprovisionSmartStartNode(int NodeID)
        {
            return _UnprovisionSmartStartNode(NodeID);
        }

        // LOCAL
        public Task<CMDResult> UnprovisionSmartStartNode(string DSK)
        {
            return _UnprovisionSmartStartNode(DSK);
        }
        
        // LOCAL
        public VirtualNode GetMulticastGroup(int[] Nodes)
        {
            VirtualNode VN = new VirtualNode(_driver, Nodes);
            return VN;
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
        public bool isRebuildingRoutes { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public ControllerStatistics statistics { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public DeviceConfig deviceConfig { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public Enums.RFRegion? rfRegion { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public bool supportsLongRange { get; internal set; }
        [Newtonsoft.Json.JsonProperty]
        public Enums.ControllerStatus status { get; internal set; }
    }
}
