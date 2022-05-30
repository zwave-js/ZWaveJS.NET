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
            _Driver = new Driver("COM4", ZWO);
            _Driver.DriverReady += _Driver_DriverReady;
            _Driver.StartupErrorEvent += _Driver_StartupErrorEvent;
            _Driver.Start();

            Console.ReadLine();
        }

        private static void _Driver_StartupErrorEvent(string Message)
        {
            Console.WriteLine(Message);
        }

        private static void _Driver_DriverReady()
        {

            _Driver.Controller.BackupNVMRaw((R, T) => {
                Console.WriteLine(string.Format("Read B: {0}, Total B: {1}", R, T));
            }).ContinueWith((R) => {
                Console.WriteLine(R.Result.Length);
                _Driver.Controller.RestoreNVM(R.Result, (CR, CT) => {
                    Console.WriteLine(string.Format("Read C: {0}, Tota C: {1}", CR, CT));
                }, (SR, ST) => {
                    Console.WriteLine(string.Format("Read S: {0}, Total S: {1}", SR, ST));
                });
            });
        }

        private static void Program_NodeDead(ZWaveNode Node)
        {
          
        }
    }
}
