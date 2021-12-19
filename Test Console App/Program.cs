using System;
using ZWaveJS.NET;

namespace Test_Console_App
{
    internal class Program
    {
        static Driver _Driver;
        static void Main(string[] args)
        {
            ZWaveJS.NET.Helpers.DownloadPSI().ContinueWith(R =>
            {

                ZWaveOptions Options = new ZWaveOptions();
                _Driver = new Driver("COM7", Options);

                _Driver.DriverReady += _Driver_DriverReady;
                _Driver.NodeReady += _Driver_NodeReady;
                _Driver.NodeAdded += _Driver_NodeAdded;
                _Driver.NodeRemoved += _Driver_NodeRemoved;
                _Driver.NodeAsleep += _Driver_NodeAsleep;
                _Driver.NodeAwake += _Driver_NodeAwake;
                _Driver.NodeInterviewStarted += _Driver_NodeInterviewStarted;
                _Driver.NodeInterviewFailed += _Driver_NodeInterviewFailed;
                _Driver.NodeInterviewCompleted += _Driver_NodeInterviewCompleted;
                _Driver.Notification += _Driver_Notification;
                _Driver.ValueUpdated += _Driver_ValueUpdated;
                _Driver.InclusionStarted += _Driver_InclusionStarted;
                _Driver.InclusionStopped += _Driver_InclusionStopped;
                _Driver.ExclusionStarted += _Driver_ExclusionStarted;
                _Driver.ExclusionStopped += _Driver_ExclusionStopped;



                _Driver.Start();

            });
            

            Console.Read();
        }

        private static void _Driver_ExclusionStopped()
        {
            Console.WriteLine("Exclusion Stopped");
        }

        private static void _Driver_ExclusionStarted()
        {
            Console.WriteLine("Exclusion Started");
        }

        private static void _Driver_InclusionStopped()
        {
            Console.WriteLine("Inclusion Stopped");
        }

        private static void _Driver_InclusionStarted(bool Secure)
        {
            Console.WriteLine("Inclusion Started: {0}", Secure);
        }

        private static void _Driver_ValueUpdated(int NodeID, Newtonsoft.Json.Linq.JObject Args)
        {
            Console.WriteLine("Value Updated: {0}, {1}", NodeID, Args.ToString());
        }

        private static void _Driver_Notification(int NodeID, int ccId, Newtonsoft.Json.Linq.JObject Args)
        {
            Console.WriteLine("Notification: {0}, {1}, {2}", NodeID, ccId, Args.ToString());
        }

        private static void _Driver_NodeInterviewCompleted(int NodeID)
        {
            Console.WriteLine("Node Interview Completed: {0}", NodeID);
        }

        private static void _Driver_NodeInterviewFailed(int NodeID)
        {
            Console.WriteLine("Node Interview Failed: {0}", NodeID);
        }

        private static void _Driver_NodeInterviewStarted(int NodeID)
        {
            Console.WriteLine("Node Interview Started: {0}", NodeID);
        }

        private static void _Driver_NodeAwake(int NodeID)
        {
            Console.WriteLine("Node Awake: {0}", NodeID);
        }

        private static void _Driver_NodeAsleep(int NodeID)
        {
            Console.WriteLine("Node Asleep: {0}", NodeID);
        }

        private static void _Driver_NodeRemoved(int NodeID)
        {
            Console.WriteLine("Node Removed: {0}", NodeID);
        }

        private static void _Driver_NodeAdded(int NodeID)
        {
            Console.WriteLine("Node Added: {0}", NodeID);
        }

        private static void _Driver_NodeReady(int NodeID, ZWaveNode Node)
        {
            Console.WriteLine("Node Ready: {0} - {1}", NodeID, Node.label);
        }

        private static void _Driver_DriverReady(Controller Controller, ZWaveNode[] Nodes)
        {
            Console.WriteLine("Driver Ready: {0} Nodes",Nodes.Length);



        }
    }


}
