ZWaveJS.NET.Driver _Driver;



ZWaveJS.NET.ZWaveOptions Options = new ZWaveJS.NET.ZWaveOptions();
Options.enableSoftReset = false;
Options.securityKeys = new ZWaveJS.NET.CFGSecurityKeys {
    S0_Legacy = "ad0172b487f3ed99563c2fb4f75fab27", S2_AccessControl = "10862fc59a9400701234e1cf1e9e1e78", S2_Authenticated = "9b92f91e7c6704de617ac1df3b8e1f15", S2_Unauthenticated = "87e534e7312c131ca8aad8959625e6fe"
};

_Driver = new ZWaveJS.NET.Driver("COM3", Options);
_Driver.StartUpError += _Driver_StartUpError;
_Driver.ConnectionLost += _Driver_ConnectionLost;
_Driver.DriverReady += _Driver_DriverReady;

_Driver.Start();

while (true)
{
    Console.ReadLine();
}

async void _Driver_DriverReady()
{
 
}

void _Driver_ConnectionLost(string Message)
{
  Console.WriteLine($"Connection Lost : {Message}");
}

void _Driver_StartUpError(string Message)
{
    Console.WriteLine($"Start Up Error : {Message}");
}