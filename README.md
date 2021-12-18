# ZWaveJS.NET

ZWaveJS.NET is a class library developed in .NET Core 3.1, it exposes the zwave-js Driver in .NET opening up the ability to use its runtime directly in .NET applications.

## Getting Started.

The library is in 2 parts: The assembly file its self (dll), and an accompanying **server.psi** file.  

The library uses [zwave-js-server](https://github.com/zwave-js/zwave-js-server) - but your environment does not need **node** or **npm** installed, this is what **server.psi** contains.  

Its an executable that is ran silently/hidden, and it contains everything necessary for .NET to work with zwave-js.  
**server.psi** files are platform specific, but the assembly isn't - it will run on windows, OSX and Linux, and the platform specifics i.e **node** are contained in **server.psi**.

The class library contains most of the methods you will need, from including a secure device, to removing it.

## Current implementation milestones 
 - [x] Inclusion (Currently only Unsecured/S0)
 - [x] Exclusion
 - [x] Updating Values
 - [ ] CCAPI Invoke
 - [x] Value Updated Event Subscription
 - [x] Notification Event Subscription
 - [x] Node Added/Removed Event Subscription
 - [x] Inclusion Started, Stopped Event Subscription
 - [x] Exclusion Started, Stopped Event Subscription
 - [x] Node Ready, Asleep, Awake Event Subscription
 - [ ] Heal Network
 - [ ] Heal Network Progress Event Subscription
 - [ ] Heal Node
 - [ ] Update Firmware
 - [ ] Update Firmware Progress Event Subscription


## Brief Example
```c#
static ZWaveJS.NET.Driver _Driver;
static void Main(string[] args)
{
    // Set encryption keys, enable logging, adjust network timeouts so on and so forth.
     ZWaveJS.NET.ZWaveOptions Options = new  ZWaveJS.NET.ZWaveOptions();

    _Driver = new Driver("COM7", Options);

    _Driver.DriverReady += _Driver_DriverReady;
    _Driver.NodeReady += _Driver_NodeReady;
    _Driver.NodeAdded += _Driver_NodeAdded;
    _Driver.NodeRemoved += _Driver_NodeRemoved;
    _Driver.NodeAsleep += _Driver_NodeAsleep;
    _Driver.NodeAwake += _Driver_NodeAwake;
    _Driver.NodeInterviewStarted += _Driver_NodeInterviewStarted;
    _Driver.NodeInterviewFailed += _Driver_NodeInterviewFailed;
    _Driver.NodeInterviewCompleted += _Driver_NodeInterviewCompleted;
    _Driver.Notification += _Driver_Notification;
    _Driver.ValueUpdated += _Driver_ValueUpdated;
    _Driver.InclusionStarted += _Driver_InclusionStarted;
    _Driver.InclusionStopped += _Driver_InclusionStopped;
    _Driver.ExclusionStarted += _Driver_ExclusionStarted;
    _Driver.ExclusionStopped += _Driver_ExclusionStopped;

    _Driver.Start();
}

private static void _Driver_DriverReady(ZWaveJS.NET.Controller Controller, ZWaveJS.NET.ZWaveNode[] Nodes)
{
    ZWaveJS.NET.ValueID VID = new ZWaveJS.NET.ValueID();
    VID.commandClass = 135;
    VID.property = "value";
    VID.endpoint = 0;

    // Support for set Value Options
    ZWaveJS.NET.SetValueOptions SVO = new  ZWaveJS.NET.SetValueOptions();
    SVO.transitionDuration = "2s";
    SVO.volume = 30;

    // Node, VauleID, Value, SetValueOptions (Optional) : All methods returns a task, as to not block the UI
    _Driver.SetValue(3, VID, 200, SVO).ContinueWith((res) => {
        if (res.Result) {
            Console.WriteLine("Value Updated");
        }
    });
}
```