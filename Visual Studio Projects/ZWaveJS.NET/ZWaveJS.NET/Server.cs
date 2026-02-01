using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZWaveJS.NET
{
    public class Server
    {

        private Process ServerProcess;

        internal delegate void ZwaveJSErrorEvent(int Code, string Message, bool Start);
        internal event ZwaveJSErrorEvent ZWaveSJError;

        internal delegate void ProcessdExitedEvent();
        internal event ProcessdExitedEvent Exited;
        public static string PSIRoot;

        internal void Terminate()
        {
            try
            {
                if (ServerProcess != null && !ServerProcess.HasExited)
                {
                    ServerProcess.StandardInput.WriteLine("KILL");
                    ServerProcess.Dispose();
                }
            }
            catch(Exception Error){}

        }

        internal void Start(string SerialPort, ZWaveOptions Config, int WSPort)
        {

            string ProcessName = string.Format("server.{0}.psi", WSPort);

            Process[] Zombies = Process.GetProcessesByName(ProcessName);
            foreach (Process Zombie in Zombies)
            {
                Zombie.Kill();
                Zombie.WaitForExit();
                File.Delete(ProcessName);
            }

            string PSIPath = "server.psi";
            string ProcessPath = ProcessName;
            if (!string.IsNullOrEmpty(PSIRoot))
            {
                PSIPath = Path.Join(PSIRoot, PSIPath);
                ProcessPath = Path.Join(PSIRoot, ProcessName);
            }

            if (!File.Exists(PSIPath))
            {
                throw new Exception("No Platform Support Image (server.psi) found");
            }

            File.Copy(PSIPath, ProcessPath, true);

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

            PSI.FileName = ProcessPath;
            PSI.UseShellExecute = false;
#if !DEBUG
            PSI.WindowStyle = ProcessWindowStyle.Hidden;
            PSI.CreateNoWindow = true;
#endif
            ServerProcess = new Process();
            ServerProcess.EnableRaisingEvents = true;
            ServerProcess.ErrorDataReceived += ServerProcess_ErrorDataReceived;
            ServerProcess.Exited += ServerProcess_Exited;

            ServerProcess.StartInfo = PSI;
            ServerProcess.Start();
            ServerProcess.BeginErrorReadLine();
        }

        private void ServerProcess_Exited(object sender, EventArgs e)
        {
            Exited?.Invoke();
            ServerProcess.Dispose();
        }

        private void ServerProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            JObject JO = JObject.Parse(e.Data);
            ZWaveSJError?.Invoke(JO.Value<int>("code"), JO.Value<string>("message"),JO.Value<bool>("start"));
        }
    }
}
