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

            ZWaveJS.NET.Helpers.DownloadPSI().ContinueWith((R) => {

                var B = "sadsdsa";
  

          });
        }

        private static void _Driver_StartupErrorEvent(string Message)
        {
            Console.WriteLine(Message);
        }

        private static void _Driver_DriverReady()
        {
            var ddd = new InclusionOptions();
            ddd.strategy = Enums.InclusionStrategy.Security_S0;
            _Driver.Controller.ReplaceFailedNode(4, ddd).ContinueWith((R) => {

                var Res = R;
            
            });
        }

        private static void Program_NodeDead(ZWaveNode Node)
        {
          
        }
    }
}
