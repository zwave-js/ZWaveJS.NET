using System;
using ZWaveJS.NET;
using System.Reflection;
using System.IO;

namespace Test_Console_App
{
    internal class Program
    {
        static Driver D;
        static void Main(string[] args)
        {
            ZWaveOptions Options = Newtonsoft.Json.JsonConvert.DeserializeObject<ZWaveOptions>(File.ReadAllText("DriverSettings.json"));
            D = new Driver("/dev/tty.usbmodem21101", Options);
            D.DriverReady += D_DriverReady;
            D.Start();

            Console.Read();
        }

        private static void D_DriverReady()
        {
            InclusionOptions O = new InclusionOptions();
            O.userCallbacks.grantSecurityClasses = Grant;
        }

        private static InclusionGrant Grant(InclusionGrant IG)
        {
            return IG;
        }
    }
}
