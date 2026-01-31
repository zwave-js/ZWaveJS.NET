using System;
using System.IO.Ports;

namespace ZWaveJS.NET
{
    public class Lib
    {
        public static string[] SerialPorts()
        {
            return SerialPort.GetPortNames();
        }

    }
}


