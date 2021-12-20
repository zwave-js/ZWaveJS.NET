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
                Options.securityKeys = new CFGSecurityKeys();

                Options.securityKeys.S0_Legacy = "###########################";
                Options.securityKeys.S2_Unauthenticated = "###########################";
                Options.securityKeys.S2_Authenticated = "###########################";
                Options.securityKeys.S2_AccessControl = "###########################";

                _Driver = new Driver("/dev/tty.usbmodem21201", Options);
                _Driver.DriverReady += _Driver_DriverReady;
                _Driver.Start();

            });
            

            Console.Read();
        }

        private static void _Driver_DriverReady()
        {
           
        }

      
    }


}
