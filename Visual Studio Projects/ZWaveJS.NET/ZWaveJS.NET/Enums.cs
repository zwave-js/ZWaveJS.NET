namespace ZWaveJS.NET
{
    public class Enums
    {
       public enum QRCodeVersion
        {
            S2 = 0,
            SmartStart = 1,
        }

        public enum Protocols
        {
            ZWave = 0,
            ZWaveLongRange = 1,
        }

        public enum ConfigValueFormat
        {
            SignedInteger = 0x00,
            UnsignedInteger = 0x01,
            Enumerated = 0x02,
            BitField = 0x03
        }

        public enum SetValueStatus
        {
           
            NoDeviceSupport = 0x00,
            Working = 0x01,
            Fail = 0x02,
            EndpointNotFound = 0x03,
            NotImplemented = 0x04,
            InvalidValue = 0x05,
            SuccessUnsupervised = 0xfe,
            Success = 0xff
        }


        public enum UsageEnvironment
        {
            Non_Commercial,
            Commercial

        }

        public enum RFRegion
        {
            Europe = 0x00,
            USA = 0x01,
            Australia_NewZealand = 0x02,
            Hong_Kong = 0x03,
            India = 0x05,
            Israel = 0x06,
            Russia = 0x07,
            China = 0x08,
            USA_Long_Range = 0x09,
            Japan = 0x20,
            Korea = 0x21,
            Unknown = 0xfe,
            EU = 0xff,
        }

        public enum NodeFirmwareUpdateStatus
        {
            Error_Timeout = -1,
            Error_Checksum = 0,
            Error_TransmissionFailed = 1,
            Error_InvalidManufacturerID = 2,
            Error_InvalidFirmwareID = 3,
            Error_InvalidFirmwareTarget = 4,
            Error_InvalidHeaderInformation = 5,
            Error_InvalidHeaderFormat = 6,
            Error_InsufficientMemory = 7,
            Error_InvalidHardwareVersion = 8,
            OK_WaitingForActivation = 0xfd,
            OK_NoRestart = 0xfe,
            OK_RestartPending = 0xff,
        }

        public enum ControllerFirmwareUpdateStatus
        {
            Error_Timeout = 0,
            Error_RetryLimitReached,
            Error_Aborted,
            Error_NotSupported,
            OK = 0xff
        }

        public enum SecurityBootstrapFailure
        {
            UserCanceled,
            NoKeysConfigured,
            S2NoUserCallbacks,
            Timeout,
            ParameterMismatch,
            NodeCanceled,
            S2IncorrectPIN,
            S2WrongSecurityLevel,
            S0Downgrade,
            Unknown
        }



        internal class ErrorCodes
        {
            public const string MissingS2Callbacks = "ZWJS.NET.ERR.001";
            public const string InvalidStrategy = "ZWJS.NET.ERR.002";
            public const string MissingKeys = "ZWJS.NET.ERR.003";
            public const string InvalidkeyLength = "ZWJS.NET.ERR.004";
            public const string WSConnectionError = "ZWJS.NET.ERR.005";
            public const string CommercialAPIKey = "ZWJS.NET.ERR.006";
            public const string WrongOverride = "ZWJS.NET.ERR.007";
        }

        internal class Commands
        {

            public const string RemoveAssociations = "controller.remove_associations";
            public const string AddAssociations = "controller.add_associations";
            public const string GetAssociationGroups = "controller.get_association_groups";
            public const string GetAssociations = "controller.get_associations";
            public const string GetProvisioningEntries = "controller.get_provisioning_entries";
            public const string UnprovisionSmartStartNode = "controller.unprovision_smart_start_node";
            public const string ProvisionSmartStartNode = "controller.provision_smart_start_node";
            public const string SetAPIVersion = "set_api_schema";
            public const string StartListetning = "start_listening";
            public const string SetValue = "node.set_value";
            public const string GetValue = "node.get_value";
            public const string PollValue = "node.poll_value";
            public const string SetRawConfigParameterValue = "node.set_raw_config_parameter_value";
            public const string RefreshValues = "node.refresh_values";
            public const string RefreshCCValues = "node.refresh_cc_values";
            public const string GetDefinedValueIDs = "node.get_defined_value_ids";
            public const string GetValueMetadata = "node.get_value_metadata";
            public const string BeginInclusion = "controller.begin_inclusion";
            public const string StopInclusion = "controller.stop_inclusion";
            public const string BeginExclusion = "controller.begin_exclusion";
            public const string StopExclusion = "controller.stop_exclusion";
            public const string InvokeCCAPI = "endpoint.invoke_cc_api";
            public const string SupportsCCAPI = "endpoint.supports_cc_api";
            public const string GrantSecurityClasses = "controller.grant_security_classes";
            public const string ValidateDSK = "controller.validate_dsk_and_enter_pin";
            public const string RefreshInfo = "node.refresh_info";
            public const string BeginRebuildingRoutes = "controller.begin_rebuilding_routes";
            public const string StopRebuildingRoutes = "controller.stop_rebuilding_routes";
            public const string RebuildNodeRoutes = "controller.rebuild_node_routes";
            public const string SetName = "node.set_name";
            public const string SetLocation = "node.set_location";
            public const string KeepNodeAwake = "node.set_keep_awake";
            public const string RemoveFailedNode = "controller.remove_failed_node";
            public const string ReplaceFailedNode = "controller.replace_failed_node";
            public const string UpdateFirmware = "node.update_firmware";
            public const string AbortFirmwareUpdate = "node.abort_firmware_update";
            public const string HasSecurityClass = "node.has_security_class";
            public const string GetHighestSecurityClass = "node.get_highest_security_class";
            public const string GetEndpointCount = "node.get_endpoint_count";
            public const string BackUpNVM = "controller.backup_nvm_raw";
            public const string RestoreNVM = "controller.restore_nvm";
            public const string CheckLifelineHealth = "node.check_lifeline_health";
            public const string MCGetEndpointCount = "multicast_group.get_endpoint_count";
            public const string MCSetValue = "multicast_group.set_value";
            public const string MCGetDefinedValueIDs = "multicast_group.get_defined_value_ids";
            public const string MCSupportsCCAPI = "multicast_group.supports_cc_api";
            public const string MCInvokeCCAPI = "multicast_group.invoke_cc_api";
            public const string StartListeningLogs = "start_listening_logs";
            public const string StopListeningLogs = "stop_listening_logs";
            public const string WaitForWakeUp = "node.wait_for_wakeup";
            public const string Interview = "node.interview";
            public const string FirmwareUpdateOTW = "controller.firmware_update_otw";
            public const string SetRFRegion = "controller.set_rf_region";
            public const string GetRFRegion = "controller.get_rf_region";
            public const string SetPowerlevel = "controller.set_powerlevel";
            public const string GetPowerlevel = "controller.get_powerlevel";
            public const string Ping = "node.ping";
            public const string GetAvailableFirmwareUpdates = "controller.get_available_firmware_updates";
            public const string FirmwareUpdateOTA = "controller.firmware_update_ota";
            public const string HardReset = "driver.hard_reset";
            public const string SoftReset = "driver.soft_reset";
            public const string ParseQRCodeString = "utils.parse_qr_code_string";
        }

        public enum SecurityClass
        {
            
            S2_Unauthenticated,
            S2_Authenticated,
            S2_AccessControl,
            S0_Legacy = 7,
            Unsecured = -1
        }

        public enum LogLevel
        {
            Error = 0,
            Warn,
            Info,
            verbose = 4,
            Debug,
            Silly
        }

        public enum NodeStatus
        {
            Unknown,
            Asleep,
            Awake,
            Dead,
            Alive
        }

        public enum RemoveNodeReason
        {
            Excluded,
            ProxyExcluded,
            RemoveFailed,
            Replaced,
            ProxyReplaced,
            Reset,
            SmartStartFailed,
        }

       public enum ExclusionStrategy
        {
            ExcludeOnly,
            DisableProvisioningEntry,
            Unprovision
        }

        public enum InclusionStrategy
        {
            Default = 0,
            Insecure = 2,
            Security_S0,
            Security_S2
        }
    }
}
