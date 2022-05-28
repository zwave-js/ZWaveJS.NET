using System;
using System.Collections.Generic;
using System.Text;
using System.Net.WebSockets;
using System.IO;
using System.Linq;

namespace ZWaveJS.NET
{
    class WSClient
    {
        public delegate void MessageReceived(WebSocketMessageType Type, byte[] Object);
        public event MessageReceived MessageReceivedEvent;

        ClientWebSocket _Socket;
        Uri _Host;

        public WSClient(Uri Host)
        {
            _Host = Host;
            
        }

        public void Start()
        {

            new System.Threading.Tasks.Task(async () =>
            {
                _Socket = new ClientWebSocket();

                while (_Socket.State != WebSocketState.Open)
                {
                    try
                    {
                      
                        await _Socket.ConnectAsync(_Host, System.Threading.CancellationToken.None);
                        Run();
                    }
                    catch (Exception Error)
                    {
                        System.Threading.Thread.Sleep(1000);
                        _Socket = new ClientWebSocket();
                    }

                }
            }).Start();

        }

        private void Run()
        {
            new System.Threading.Tasks.Task(async () =>
            {

                byte[] Buf = new byte[4096];
                ArraySegment<byte> buffer = new ArraySegment<byte>(Buf);
                MemoryStream MS = new MemoryStream();
                int Read = 0;

                while (true)
                {
                    var Result = await _Socket.ReceiveAsync(buffer, System.Threading.CancellationToken.None);
                    MS.Write(buffer.Array, buffer.Offset, Result.Count);
                    Read += Result.Count;
                    if (Result.EndOfMessage)
                    {
                        MessageReceivedEvent?.Invoke(Result.MessageType, MS.ToArray().Take(Read).ToArray());
                        MS.SetLength(0);
                        //MS.Seek(0, SeekOrigin.Begin);
                        Read = 0;
                    }
                }
            }).Start();
        }

        public void Send(string Payload)
        {
            byte[] Bytes = Encoding.UTF8.GetBytes(Payload);
            var buffer = new ArraySegment<byte>(Bytes);
            _Socket.SendAsync(buffer, WebSocketMessageType.Text, true, System.Threading.CancellationToken.None);
        }
    }
}
