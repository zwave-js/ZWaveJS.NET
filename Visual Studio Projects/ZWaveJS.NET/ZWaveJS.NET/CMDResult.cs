using System;

using Newtonsoft.Json.Linq;

namespace ZWaveJS.NET
{
    public class CMDResult
    {
        internal CMDResult(string ErrorCode, string Message, bool Success)
        {
            this.Success = Success;
            this.ErrorCode = ErrorCode;
            this.Message = Message;
        }
        internal CMDResult(JObject Res)
        {
            this.Success = Res.Value<bool>("success");

            if (Res.ContainsKey("result"))
            {
                bool? success = Res.SelectToken("result.success")?.Value<bool?>();
                if (success.HasValue)
                    SetPayload(success.Value);
            }

            if(Res.ContainsKey("zwaveErrorCode"))
                this.ErrorCode = Res.Value<string>("zwaveErrorCode");

            if (Res.ContainsKey("zwaveErrorMessage"))
                this.Message = Res.Value<string>("zwaveErrorMessage");
        }

        public T ResultPayloadAs<T>()
        {
            return ResultPayload is T value ? value : default!;
        }

        internal void SetPayload(object Payload)
        {
            this.ResultPayload = Payload;
            this.PayloadType = Payload.GetType();
        }

        public bool Success { get; internal set; }
        public string Message { get; internal set; }
        public string ErrorCode { get; internal set; }
        public object ResultPayload { get; private set; }
        public Type PayloadType { get; private set; }

    }
}
