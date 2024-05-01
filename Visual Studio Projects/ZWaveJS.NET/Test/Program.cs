using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZWaveJS.NET;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ZWaveJS.NET.ZWaveOptions ZWO = new ZWaveJS.NET.ZWaveOptions();

            ZWO.securityKeys.S0_Legacy = "c84de525ba1382374615f93427a18138";
            ZWO.securityKeys.S2_AccessControl = "3d18628e80743ebaf96d97707cffb139";
            ZWO.securityKeys.S2_Authenticated = "c2da28bc56ad5484fcf10efd2a1ed1c1";
            ZWO.securityKeys.S2_Unauthenticated = "69ded77863e0b64401f1ac12c4d15c86";

            ZWO.securityKeysLongRange.S2_AccessControl = "1e76388da0674c6d799e6f1f6bef0c91";
            ZWO.securityKeysLongRange.S2_Authenticated = "6b948cf3ea8679e57b7f89cce47bcb18";

            ZWO.logConfig.logToFile = false;
            ZWO.logConfig.enabled = true;
            ZWO.enableSoftReset = false;

            ZWaveJS.NET.Driver D = new ZWaveJS.NET.Driver("COM3", ZWO);

            D.DriverReady += () => {

                D.Utils.ParseQRCodeString("900143047006000000000000000000000000000000000000000000100025800002022000004000050000601800080201").ContinueWith((R1) =>
                {
                    QRProvisioningInformation QRI = R1.Result.ResultPayload as QRProvisioningInformation;

                    // Modify based on users choice (Protoccol)
                    // Then...
                    // D.Controller.ProvisionSmartStartNode(QRI).ContinueWith((R2) => { ... });
                });
            
            };

            D.Start();
            Console.ReadLine();


        }
    }
}
