using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace ZWaveJS.NET
{
    internal class Server
    {

        private static Process ServerProcess;

        public delegate void FatalErrorEvent();
        public static event FatalErrorEvent FatalError;

        public delegate void NoneFatalErrorEvent();
        public static event NoneFatalErrorEvent NoneFatalError;


        internal static void Start(string SerialPort, ZWaveOptions Config, int WSPort)
        {
            if (!File.Exists("server.psi"))
            {
                throw new FileNotFoundException("No Platform Snapshot Image found (server.psi)");
            }

            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

            JsonSerializerSettings JSS = new JsonSerializerSettings();
            JSS.NullValueHandling = NullValueHandling.Ignore;
            string _Config = JsonConvert.SerializeObject(Config, JSS);

            ProcessStartInfo PSI = new ProcessStartInfo();
            PSI.RedirectStandardError = true;


#if NET45
            PSI.EnvironmentVariables.Add("CONFIG", _Config);
            PSI.EnvironmentVariables.Add("SERIAL_PORT", SerialPort);
            PSI.EnvironmentVariables.Add("WS_PORT", WSPort.ToString());
#endif

#if NETSTANDARD2_0
            PSI.Environment.Add("CONFIG", _Config);
            PSI.Environment.Add("SERIAL_PORT", SerialPort);
            PSI.Environment.Add("WS_PORT", WSPort.ToString());
#endif


            PSI.FileName = "server.psi";
            PSI.UseShellExecute = false;
            
            PSI.WindowStyle = ProcessWindowStyle.Hidden;
            PSI.CreateNoWindow = true;
            ServerProcess = new Process();
            ServerProcess.EnableRaisingEvents = true;
            ServerProcess.ErrorDataReceived += ServerProcess_ErrorDataReceived;
            ServerProcess.StartInfo = PSI;
            ServerProcess.Start();
            ServerProcess.BeginErrorReadLine();

        }

       

        private static void ServerProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            int Code = Convert.ToInt32(e.Data);

            switch (Code)
            {
                case 1:
                    FatalError?.Invoke();
                    if (!ServerProcess.HasExited)
                    {
                        ServerProcess.Kill();
                    }
                    break;

                case 2:
                    NoneFatalError?.Invoke();
                    if (!ServerProcess.HasExited)
                    {
                        ServerProcess.Kill();
                    }
                    break;

            }
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            ServerProcess.Kill();
        }
    }
}
