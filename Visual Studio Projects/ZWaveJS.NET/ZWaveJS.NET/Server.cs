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
            if (!File.Exists("server.psi"))
            {
                throw new FileNotFoundException("No Platform Snapshot Image found (server.psi)");
            }

            JsonSerializerSettings JSS = new JsonSerializerSettings();
            JSS.NullValueHandling = NullValueHandling.Ignore;
            string _Config = JsonConvert.SerializeObject(Config, JSS);

            ProcessStartInfo PSI = new ProcessStartInfo();
            PSI.RedirectStandardError = true;
            PSI.RedirectStandardInput = true;
            
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
            
            ServerProcess.StartInfo = PSI;
            ServerProcess.Start();
            ServerProcess.BeginErrorReadLine();

          
        }

        private static void ServerProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
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
