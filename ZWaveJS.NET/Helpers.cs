using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Reflection;

namespace ZWaveJS.NET
{
    public  class Helpers
    {

        private const string MACOSBIN = "server-macos.psi";
        private const string WINBIN = "server-win.psi";

        internal static Enums.Platform RunningPlatform()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                    if (Directory.Exists("/Applications")
                        & Directory.Exists("/System")
                        & Directory.Exists("/Users")
                        & Directory.Exists("/Volumes"))
                        return Enums.Platform.Mac;
                    else
                        return Enums.Platform.Linux;

                case PlatformID.MacOSX:
                    return Enums.Platform.Mac;

                default:
                    return Enums.Platform.Windows;
            }
        }

        public static Task<bool> DownloadPSI()
        {
            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();
            if (!File.Exists("server.psi"))
            {
                string URI = "https://github.com/zwave-js/ZWaveJS.NET/releases/download/{V}/{F}";
                URI = URI.Replace("{V}", Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);

                switch (RunningPlatform())
                {
                    case Enums.Platform.Windows:
                        URI = URI.Replace("{F}", WINBIN);
                        break;

                    case Enums.Platform.Mac:
                        URI = URI.Replace("{F}", MACOSBIN);
                        break;
                }

                WebClient WC = new WebClient();
                WC.DownloadFileCompleted += (s, e) => {
                    Result.SetResult(true);
                };
                WC.DownloadFileAsync(new Uri(URI), "server.psi");
            }
            else
            {
                Result.SetResult(true);
            }

            return Result.Task;
        } 
    }
}
