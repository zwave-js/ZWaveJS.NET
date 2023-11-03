using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWaveJS.NET
{
    public class MethodFactory
    {
        public static Func<Dictionary<string, object>, Task<CMDResult>> CreateVOID(Driver Runtime, string ServerMethod)
        {
            return (x) => Execute(Runtime, ServerMethod, x);
        }

        public static Func<Dictionary<string, object>, Task<CMDResult>> CreatePRIMITIVE(Driver Runtime, string ServerMethod, string ObjectPath)
        {
            return (x) => Execute(Runtime, ServerMethod, x, ObjectPath);
        }

        public static Func<Dictionary<string, object>, Task<CMDResult>> CreateCLASS(Driver Runtime, string ServerMethod, Type MappedClass, string ObjectPath)
        {
            return (x) => Execute(Runtime, ServerMethod, x,MappedClass, ObjectPath);
        }

        private static Task<CMDResult> Execute(Driver Runtime, string ServerMethod, Dictionary<string, object> Args, string ObjectPath)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            Runtime.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    object Obj = JO.SelectToken(ObjectPath).ToObject<object>();
                    Res.SetPayload(Obj);
                }
                Result.SetResult(Res);

            });

            Args.Remove("messageId");
            Args.Remove("command");

            Args.Add("messageId", ID);
            Args.Add("command", ServerMethod);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Args);
            Runtime.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        private static Task<CMDResult> Execute(Driver Runtime, string ServerMethod, Dictionary<string, object> Args, Type MappedClass, string ObjectPath)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            Runtime.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                if (Res.Success)
                {
                    object Obj = JO.SelectToken(ObjectPath).ToObject(MappedClass);
                    Res.SetPayload(Obj);
                }
                Result.SetResult(Res);

            });

            Args.Remove("messageId");
            Args.Remove("command");

            Args.Add("messageId", ID);
            Args.Add("command", ServerMethod);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Args);
            Runtime.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }

        private static Task<CMDResult> Execute(Driver Runtime, string ServerMethod, Dictionary<string, object> Args)
        {
            Guid ID = Guid.NewGuid();

            TaskCompletionSource<CMDResult> Result = new TaskCompletionSource<CMDResult>();

            Runtime.Callbacks.Add(ID, (JO) =>
            {
                CMDResult Res = new CMDResult(JO);
                Result.SetResult(Res);

            });

            Args.Remove("messageId");
            Args.Remove("command");
            
            Args.Add("messageId", ID);
            Args.Add("command", ServerMethod);

            string RequestPL = Newtonsoft.Json.JsonConvert.SerializeObject(Args);
            Runtime.ClientWebSocket.SendInstant(RequestPL);

            return Result.Task;
        }
    }
}
