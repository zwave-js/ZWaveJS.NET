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

            VirtualNode VN = _Driver.Controller.GetMulticastGroup(new int[] { 2,3,4,6,7});
            VN.GetDefinedValueIDs();
            VN.GetEndpointCount();
        



           

          
           
        }

        private static void Program_NodeDead(ZWaveNode Node)
        {
          
        }
    }
}
