using System;
using System.Collections.Generic;
using System.Text;
using System.Net.WebSockets;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace ZWaveJS.NET
{
    class WSClient
    {
        public delegate void MessageReceived(WebSocketMessageType Type, byte[] Object);
        public event MessageReceived MessageReceivedEvent;
        private Task RecieveTask;

        private CancellationTokenSource CTS;
        private CancellationToken Token;

        ClientWebSocket _Socket;
        Uri _Host;

        public WSClient(Uri Host)
        {
            CTS = new CancellationTokenSource();
            Token = CTS.Token;
            _Host = Host;
            
        }

        public void Stop()
        {
            if(CTS != null)
            {
                CTS.Cancel();
            }
        }

        public async void Start()
        {
        Start:

            try
            {
                _Socket = new ClientWebSocket();
                await _Socket.ConnectAsync(_Host, Token);
            }
            catch (Exception Error)
            {
                _Socket?.Dispose();
                _Socket = null;

                System.Threading.Thread.Sleep(1000);
                goto Start;
            }



            RecieveTask = Task.Run(async () =>
            {

                byte[] Buf = new byte[1024 * 8];
                ArraySegment<byte> AS = new ArraySegment<byte>(Buf);


                while (_Socket.State != WebSocketState.Closed)
                {

                    WebSocketReceiveResult result = null;
                    using (MemoryStream MS = new MemoryStream())
                    {
                        do
                        {
                            try
                            {
                                result = await _Socket.ReceiveAsync(AS, Token).ConfigureAwait(false);
                                if (result.Count > 0)
                                {
                                    MS.Write(AS.Array, AS.Offset, result.Count);
                                }
                            }
                            catch (Exception Error)
                            {
                                if (Error is OperationCanceledException)
                                {
                                    await _Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Close", CancellationToken.None);
                                    goto Exit;
                                }
                            }

                        }
                        while (!result.EndOfMessage);
                        MessageReceivedEvent?.Invoke(result.MessageType, MS.ToArray());
                    }


                }


            Exit:
                return;

            });
        }

        public void Send(string Payload)
        {
            byte[] Bytes = Encoding.UTF8.GetBytes(Payload);
            var buffer = new ArraySegment<byte>(Bytes);
            _Socket.SendAsync(buffer, WebSocketMessageType.Text, true, System.Threading.CancellationToken.None);
        }
    }
}
