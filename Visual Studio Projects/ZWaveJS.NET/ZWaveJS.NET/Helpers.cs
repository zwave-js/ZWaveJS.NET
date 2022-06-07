using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Reflection;

namespace ZWaveJS.NET
{
    public class Helpers
    {
        private const string MacOSBinary = "server-macos.psi";
        private const string WinBinary = "server-win.psi";
        private const string UnixBinary = "server-ubuntu.psi";
        private const string UnixBinaryARM = "server-debian-arm.psi";

        internal static Enums.Platform RunningPlatform()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                    if (Directory.Exists("/Applications") && Directory.Exists("/System") && Directory.Exists("/Users") && Directory.Exists("/Library"))
                    {
                        return Enums.Platform.Mac;
                    }
                    else if (typeof(string).Assembly.GetName().ProcessorArchitecture == ProcessorArchitecture.Arm)
                    {
                        return Enums.Platform.LinuxARM;
                    }
                    else
                    {
                        return Enums.Platform.Linux;
                    }

                case PlatformID.MacOSX:
                    return Enums.Platform.Mac;

                default:
                    return Enums.Platform.Windows;
            }
        }

        public static Task<bool> DownloadPSI(bool Force = false)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            TaskCompletionSource<bool> Result = new TaskCompletionSource<bool>();
            if (!File.Exists("server.psi") || Force)
            {
                Version V = Assembly.GetExecutingAssembly().GetName().Version;
                string URI = "https://github.com/zwave-js/ZWaveJS.NET/releases/{VER}/download/{FN}";
                URI = URI.Replace("{VER}", string.Format("v{0}.{1}.{2}",V.Major,V.Minor,V.Build));
                switch (RunningPlatform())
                {
                    case Enums.Platform.Windows:
                        URI = URI.Replace("{FN}", WinBinary);
                        break;

                    case Enums.Platform.Mac:
                        URI = URI.Replace("{FN}", MacOSBinary);
                        break;

                    case Enums.Platform.Linux:
                        URI = URI.Replace("{FN}", UnixBinary);
                        break;

                    case Enums.Platform.LinuxARM:
                        URI = URI.Replace("{FN}", UnixBinaryARM);
                        break;
                }

                using (WebClient WC = new WebClient())
                {
                    WC.DownloadFileCompleted += (s, e) =>
                    {
                        Result.SetResult(true);
                    };
                    WC.DownloadFileAsync(new Uri(URI), "server.psi");
                }
                   
            }
            else
            {
                Result.SetResult(true);
            }

            return Result.Task;
        }
    }
}
