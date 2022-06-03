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
            InclusionOptions IO = new InclusionOptions();
            IO.strategy = Enums.InclusionStrategy.Security_S2;
            _Driver.Controller.ReplaceFailedNode(4, IO).ContinueWith((R) =>
            {
                string Break = ";";
            });
           
        }

        private static void Program_NodeDead(ZWaveNode Node)
        {
          
        }
    }
}
