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
            Mac
        }

        internal class Commands
        {
            public const string SetAPIVersion = "set_api_schema";
            public const string StartListetning = "start_listening";
            public const string SetValue = "node.set_value";
            public const string GetValue = "node.get_value";
            public const string PollValue = "node.poll_value";
            public const string GetDefinedValueIDs = "node.get_defined_value_ids";
            public const string BeginInclusion = "controller.begin_inclusion";
            public const string StopInclusion = "controller.stop_inclusion";
            public const string BeginExclusion = "controller.begin_exclusion";
            public const string StopExclusion = "controller.stop_exclusion";
            public const string InvokeCCAPI = "endpoint.invoke_cc_api";

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
