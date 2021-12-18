using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace ZWaveJS.Net
{
    public class Server
    {

        private static Process ServerProcess;
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
            PSI.Environment.Add("CONFIG", _Config);
            PSI.Environment.Add("SERIAL_PORT", SerialPort);
            PSI.Environment.Add("WS_PORT", WSPort.ToString());
            PSI.FileName = "server.psi";
            PSI.UseShellExecute = false;
            PSI.WindowStyle = ProcessWindowStyle.Hidden;
            PSI.CreateNoWindow = true;
            ServerProcess = Process.Start(PSI);

        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            ServerProcess.Kill(true);
        }
    }
}
