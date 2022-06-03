using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZWaveJS.NET;

namespace Scratch_Pad
{
    class Program
    {
        static Driver _Driver;
        static void Main(string[] args)
        {


           

            ZWaveOptions ZWO = new ZWaveOptions();
            _Driver = new Driver("COM3", ZWO);
            _Driver.DriverReady += _Driver_DriverReady;
           
            _Driver.Start();

            Console.ReadLine();
        }

        private static void _Driver_StartupErrorEvent(string Message)
        {
            Console.WriteLine(Message);
        }

        private static void _Driver_DriverReady()
        {
          
            _Driver.Controller.GetProvisioningEntries().ContinueWith((R) =>
            {
                _Driver.Controller.UnprovisionSmartStartNode(((SmartStartProvisioningEntry[])R.Result.ResultPayload)[0].dsk).ContinueWith((D) => {

                 
                
                });
            });
           
        }

        private static void Program_NodeDead(ZWaveNode Node)
        {
          
        }
    }
}
