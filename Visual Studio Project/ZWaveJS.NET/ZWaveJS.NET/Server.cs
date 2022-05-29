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
        private static int Restarts = 0;

        public delegate void FatalErrorEvent();
        public static event FatalErrorEvent FatalError;

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

            if (!System.Diagnostics.Debugger.IsAttached)
            {
                PSI.WindowStyle = ProcessWindowStyle.Hidden;
                PSI.CreateNoWindow = true;
            }
            
            ServerProcess = new Process();
            ServerProcess.EnableRaisingEvents = true;
            ServerProcess.Exited += ServerProcess_Exited;
            ServerProcess.StartInfo = PSI;
            ServerProcess.Start();
        }

        private static void ServerProcess_Exited(object sender, EventArgs e)
        {
            switch (ServerProcess.ExitCode)
            {
                case 1:
                    FatalError?.Invoke();
                    break;

                case 2:
                    if (Restarts < 5)
                    {
                        Restarts++;
                        ServerProcess.Start(); // again
                    }
                    else
                    {
                        FatalError?.Invoke();
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
