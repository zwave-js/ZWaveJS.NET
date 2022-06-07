using System;
using System.Collections.Generic;
using System.Text;

namespace ZWaveJS.NET
{
    public class Enums
    {
        internal enum Platform
        {
            Windows,
            Linux,
            LinuxARM,
            Mac
        }

        internal class ErrorCodes
        {
            public const string MissingS2Callbacks = "ZWJS.NET.ERR.001";
            public const string InvalidStrategy = "ZWJS.NET.ERR.002";
            public const string MissingKeys = "ZWJS.NET.ERR.003";
            public const string InvalidkeyLength = "ZWJS.NET.ERR.004";
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
            public const string BeginHealingNetwork = "controller.begin_healing_network";
            public const string StopHealingNetwork = "controller.stop_healing_network";
            public const string HealNode = "controller.heal_node";
            public const string SetName = "node.set_name";
            public const string SetLocation = "node.set_location";
            public const string KeepNodeAwake = "node.set_keep_awake";
            public const string RemoveFailedNode = "controller.remove_failed_node";
            public const string ReplaceFailedNode = "controller.replace_failed_node";
            public const string BeginFirmwareUpdate = "node.begin_firmware_update";
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

        public enum InclusionStrategy
        {
            Default = 0,
            Insecure = 2,
            Security_S0,
            Security_S2
        }
    }
}
