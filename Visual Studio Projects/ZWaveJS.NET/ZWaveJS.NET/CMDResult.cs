﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            if(Res.ContainsKey("zwaveErrorCode"))
                this.ErrorCode = Res.Value<string>("zwaveErrorCode");

            if (Res.ContainsKey("zwaveErrorMessage"))
                this.Message = Res.Value<string>("zwaveErrorMessage");
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
