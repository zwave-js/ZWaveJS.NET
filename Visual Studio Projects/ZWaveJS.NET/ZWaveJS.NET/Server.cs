using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace ZWaveJS.NET
{
    internal class Server
    {

        private static Process ServerProcess;

        internal delegate void FatalErrorEvent();
        internal static event FatalErrorEvent FatalError;

        internal delegate void ProcessdExitedEvent();
        internal static event ProcessdExitedEvent Exited;

        internal static void Terminate()
        {
            if (ServerProcess != null && !ServerProcess.HasExited)
            {
                ServerProcess.StandardInput.WriteLine("KILL");
                ServerProcess.Dispose();
            }
        }

        internal static void Start(string SerialPort, ZWaveOptions Config, int WSPort)
        {


            Process[] Zombies = Process.GetProcessesByName("server.psi");
            foreach(Process Zombie in Zombies)
            {
                Zombie.Kill();
            }

            if (!File.Exists("server.psi"))
            {
                throw new FileNotFoundException("No Platform Snapshot Image (server.psi) found");
            }

            JsonSerializerSettings JSS = new JsonSerializerSettings();
            JSS.NullValueHandling = NullValueHandling.Ignore;
            string _Config = JsonConvert.SerializeObject(Config, JSS);

            ProcessStartInfo PSI = new ProcessStartInfo();
            PSI.RedirectStandardError = true;
            PSI.RedirectStandardInput = true;

#if FWULOCAL
            PSI.EnvironmentVariables.Add("ZWAVEJS_FW_SERVICE_URL", "http://localhost:8787");
#endif
            
            PSI.EnvironmentVariables.Add("CONFIG", _Config);
            PSI.EnvironmentVariables.Add("SERIAL_PORT", SerialPort);
            PSI.EnvironmentVariables.Add("WS_PORT", WSPort.ToString());
            PSI.EnvironmentVariables.Add("NODE_ENV", "production");

            PSI.FileName = "server.psi";
            PSI.UseShellExecute = false;
            PSI.WindowStyle = ProcessWindowStyle.Hidden;
            PSI.CreateNoWindow = true;
            ServerProcess = new Process();
            ServerProcess.EnableRaisingEvents = true;
            ServerProcess.ErrorDataReceived += ServerProcess_ErrorDataReceived;
            ServerProcess.Exited += ServerProcess_Exited;
            
            ServerProcess.StartInfo = PSI;
            ServerProcess.Start();
            ServerProcess.BeginErrorReadLine();

          
        }

        private static void ServerProcess_Exited(object sender, EventArgs e)
        {
            // Exited?.Invoke(); I think this will be indirectly handled by the socket client now
            ServerProcess.Dispose();
        }

        private static void ServerProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            int Code;
            if (int.TryParse(e.Data, out Code))
            {
                switch (Code)
                {
                    case 1:
                        FatalError?.Invoke();
                        break;
                }
            }


        }
    }
}
