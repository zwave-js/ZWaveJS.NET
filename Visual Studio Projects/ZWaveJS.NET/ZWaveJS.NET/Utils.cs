using System;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Reflection;
using System.Collections.Generic;

namespace ZWaveJS.NET
{

    public class Utils
    {
        private Driver _driver;
        internal Utils(Driver Driver)
        {
            _driver = Driver;
        }

        // Checked as of : 3.5.0
        public Task<CMDResult> ParseQRCodeString(string QR)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            _driver.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    QRProvisioningInformation PQR = JO.SelectToken("result.qrProvisioningInformation").ToObject<QRProvisioningInformation>();
                    Res.SetPayload(PQR);
                }
                Result.SetResult(Res);
            });

            Dictionary<string, object> Request = new Dictionary<string, object>();
            Request.Add("messageId", ID);
            Request.Add("command", Enums.Commands.ParseQRCodeString);
            Request.Add("qr", QR);
            
            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
            _driver.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }
    }
}
