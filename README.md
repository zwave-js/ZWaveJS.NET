# ZWaveJS.NET


![Nuget](https://img.shields.io/static/v1?label=license&message=MIT&color=green)
![Nuget](https://img.shields.io/nuget/v/zwavejs.net)
[![Language grade: JavaScript](https://img.shields.io/lgtm/grade/javascript/g/zwave-js/ZWaveJS.NET.svg?logo=lgtm&logoWidth=18)](https://lgtm.com/projects/g/zwave-js/ZWaveJS.NET/context:javascript)


ZWaveJS.NET is a class library developed in .NET Core 3.1, that exposes the zwave-js Driver in .NET, opening up the ability to use its runtime directly in .NET applications.  

The library closely follows the structure of the zwave-js API. 

Examples:  

```c#
Driver.Controller.BeginHealingNetwork()
Driver.Controller.Nodes.Get(4).GetDefinedValueIDs()
Driver.Controller.Nodes.Get(4).SetValue(ValueID ValueID, object Value, SetValueOptions Options = null)
Driver.Controller.Nodes.Get(4).GetEndpoint(2).InvokeCCAPI(int CommandClass, string Method, params object[] Params)
```  

## Getting Started.

The library can operate in 2 ways: Client or Self Hosted.  

**Client**  
The library will connect to an already running instance of [zwave-js-server](https://github.com/zwave-js/zwave-js-server). (Schema Version >= 14)

**Self Hosted**  
The library will host its own zwave-js instance.  
You might ask, if in this mode, **nodejs** and **npm** is needed on the host system - it is not!

This is all possible with an accompanying file - **server.psi**.

Its an executable that is running silently/hidden,  
and it contains everything necessary for .NET to work with zwave-js.  

**server.psi** files are platform specific, but the assembly isn't - it will run on windows, OSX and Linux, and the platform specifics i.e **node** are contained in **server.psi**.

## Running On Linux.
We can't automate an image build that is guaranteed to work across distros, but you can build one with ease.  

To build an image for your platform:
 - Clone the repo
 - cd to **./PSI**
 - run `yarn install --immutable`
 - and finally `yarn run build`
 - rename **dist/server** to **server.psi**, and distrubute with your application/dll.

Every release will include a set of PSI images, so download the one for your platform, and rename it to **server.psi**, and ensure its in the same location as the dll.

There is also a Helper method that pulls down the correct image if one is needed **ZWaveJS.NET.Helpers.DownloadPSI()**  

**server.psi** is not needed, if using the library in Client Mode.

The class library contains most of the methods you will need, from including a secure device, to removing it.

## Installing.

All releases will be published to nuget, so search for **ZWaveJS.NET** and install it, the **nupkg** file will also be attached to the release here, on Github, along with the platform PSI files.

## Current implementation milestones 

 - [ ] Controller
   - [x] Controller Info
   - [x] Inclusion (Unsecured, S0 & S2 Security)
   - [x] S2 Security inclusion Event hooks (needed to guide users in a UI for pairing S2 devices)
   - [x] Exclusion
   - [x] Inclusion, Exclusion Started, Stopped Event Subscription
   - [x] Node Added/Removed Event Subscription
   - [x] Heal Network
   - [x] Heal Network Progress Event Subscription
   - [x] Heal Node
   - [ ] Remove Failed Node
   - [ ] Replace Failed Node
   - [ ] Smart Start

 - [ ] Node
   - [x] Node Info
   - [x] Set name & location
   - [x] Updating Values
   - [x] Polling Values
   - [x] Fetching Value
   - [x] CCAPI Invoke (and its endpoints)
   - [x] Obtain Value IDs
   - [x] Obtain Value Meta Data
   - [x] Node Ready, Asleep, Awake Event Subscription
   - [x] Value Updated Event Subscription
   - [x] Notification Event Subscription
   - [x] Value Notification Event Subscription
   - [x] Interview Node
   - [x] Interview Events (Started, Completed, Failed)
   - [ ] Update Firmware
   - [ ] Update Firmware Progress Event Subscription
   - [ ] Association Management
  
  ## Brief Example
```c#
static ZWaveJS.NET.Driver _Driver;
static void Main(string[] args)
{
    // Set encryption keys, enable logging, adjust network timeouts so on and so forth.
     ZWaveJS.NET.ZWaveOptions Options = new  ZWaveJS.NET.ZWaveOptions();

    // Create Driver Instance
    _Driver = new Driver("COM7", Options);

    // Subscribe to driver ready
    _Driver.DriverReady += _Driver_DriverReady;
   
    _Driver.Start();
}

private static void _Driver_DriverReady()
{
    // Update a value
    ZWaveJS.NET.ValueID VID = new ZWaveJS.NET.ValueID();
    VID.commandClass = 135;
    VID.property = "value";
    VID.endpoint = 0;

    // Support for set Value Options
    ZWaveJS.NET.SetValueOptions SVO = new  ZWaveJS.NET.SetValueOptions();
    SVO.transitionDuration = "2s";
    SVO.volume = 30;

    // All methods returns a task, as to not block the UI
    _Driver.Controller.Nodes.Get(4).SetValue(VID, 200, SVO).ContinueWith((res) => {
        if (res.Result) {
            Console.WriteLine("Value Updated");
        }
    });

    // Listen for Value Updates on a node
    _Driver.Controller.Nodes.Get(3).ValueUpdated += Program_ValueUpdated;
    _Driver.Controller.Nodes.Get(3).Notification += Program_Notification;

    // Or All of them
    ZWaveJS.NET.ZWaveNode[] Nodes = _Driver.Controller.Nodes.AsArray();
    foreach(ZWaveJS.NET.ZWaveNode Node in Nodes)
    {
        Node.ValueUpdated += Program_ValueUpdated;
        Node.Notification += Program_Notification;
    }
    
    // Other Node methods
    _Driver.Controller.Nodes.Get(4).GetDefinedValueIDs().ContinueWith((res) => {
        // Do something with Value ID's (res.Result)
    });
}

private static void Program_ValueUpdated(ZWaveNode Node, JObject Args)
{
   // Do something with Args
}

private static void Program_Notification(ZWaveNode Node, int ccId, JObject Args)
{
   // Do something with Args
}
```
