using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZWaveJS.NET;


namespace ConsoleApp1
{
    internal class Program
    {
        static Driver D;
        static void Main(string[] args)
        {
           ZWaveOptions options = new ZWaveOptions();
            options.logConfig.enabled = true;
            D = new Driver("COM3", options);
            D.DriverReady += D_DriverReady;
           
        
            D.Start();
      

            Console.Read();
           
          
           
        }

        private static void D_DriverReady()
        {


            D.Controller.Nodes.Get(2).ValueUpdated += Program_ValueUpdated;
            D.Controller.Nodes.Get(2).ValueNotification += Program_ValueNotification;





        }

        private static void Program_ValueNotification(ZWaveNode Node, ValueNotificationArgs Args)
        {
           Console.Write(Newtonsoft.Json.JsonConvert.SerializeObject(Args, Newtonsoft.Json.Formatting.Indented));
        }

        private static void Program_ValueUpdated(ZWaveNode Node, ValueUpdatedArgs Args)
        {

            Console.Write(Newtonsoft.Json.JsonConvert.SerializeObject(Args, Newtonsoft.Json.Formatting.Indented));
        }

        private static void Controller_NodeAdded(ZWaveNode Node, InclusionResult Result)
        {
            Console.WriteLine("Added");
        }

        private static void Controller_StatisticsUpdated(ControllerStatisticsUpdatedArgs Args)
        {
            Console.WriteLine(Args.messagesTX);
            Console.WriteLine(D.Controller.statistics.messagesTX);
        }

       
    }
}
