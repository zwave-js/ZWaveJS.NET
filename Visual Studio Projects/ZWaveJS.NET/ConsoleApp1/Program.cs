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
            Console.WriteLine("STart");

            Console.Read();
           
          
           
        }

        private static void D_DriverReady()
        {
            D.Controller.StatisticsUpdated += Controller_StatisticsUpdated;
          
                
        }

        private static void Controller_StatisticsUpdated(ControllerStatisticsUpdatedArgs Args)
        {
            Console.WriteLine(Args.messagesTX);
            Console.WriteLine(D.Controller.statistics.messagesTX);
        }

       
    }
}
