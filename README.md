![Image](./Readme.png)  

# ZWaveJS.NET

![Nuget](https://img.shields.io/static/v1?label=license&message=MIT&color=green)
![Nuget](https://img.shields.io/nuget/v/zwavejs.net)
[![DeepScan grade](https://deepscan.io/api/teams/17652/projects/21245/branches/606186/badge/grade.svg)](https://deepscan.io/dashboard#view=project&tid=17652&pid=21245&bid=606186)
![GitHub issues](https://img.shields.io/github/issues-raw/zwave-js/zwavejs.net)
![GitHub closed issues](https://img.shields.io/github/issues-closed-raw/zwave-js/zwavejs.net)


ZWaveJS.NET is a class library developed for the .NET framework family, that opens up the Zwave JS Driver in .NET, allowing its full runtime to be used directly in .NET applications.  

## Supported Targets
- NET6.0
- NET7.0
- NET8.0
- NET9.0
- NET10.0
- netstandard2.1

The library strictly follows the structure of the zwave-js API. 

Examples:  

```c#
Driver.Controller.BeginRebuildingRoutes()
Driver.Controller.Nodes.Get(4).GetDefinedValueIDs()
Driver.Controller.Nodes.Get(4).SetValue(ValueID ValueID, object Value, SetValueAPIOptions Options = null)
Driver.Controller.Nodes.Get(4).GetEndpoint(2).InvokeCCAPI(int CommandClass, string Method, params object[] Params)
```  

## Features

The library contains most of the ZWave JS API whilst the code base is structured in such a way, enabling access to new methods, can be achieved within minutes.

## Getting Started.

The library can operate in 2 ways: Client or Self Hosted.  

**Client**  
The library will connect to an already running instance of [zwave-js-server](https://github.com/zwave-js/zwave-js-server).  

**Self Hosted**  
The library will host its own zwave-js instance.  
You might ask, if in this mode, **nodejs** and **npm** is needed on the host system - it is not!

This is all possible with an accompanying file - **server.psi**. (Platform Support Image)  

Its an executable that is running silently/hidden,  
and it contains everything necessary for .NET to work with zwave-js.  

**server.psi** files are platform specific, but the assembly isn't - it will run on windows, OSX and Linux, and the platform specifics i.e **node** are contained in **server.psi**.

## Building a platform specific binary.

To build an image for your platform (Note this will require Node & NPM on the machine building the image):
 - Clone the repo
 - cd to **./PSI**
 - run `npm install && npm run buld`
 - rename **./dist/server** to **./dist/server.psi**, and distrubute the image with the library.

**server.psi** is not needed, if using the library in Client Mode.

## Installing.

All releases will be published to nuget, so search for **ZWaveJS.NET** and install it, the **nupkg** file will also be attached to the release here, on Github, along with the platform PSI files.

## Brief Example
```c#
using ZWaveJS.NET;

static Driver _Driver;
static void Main(string[] args)
{
    // Set S0, S2 encryption keys, enable logging, adjust network timeouts so on and so forth.
     ZWaveOptions Options = new  ZWaveOptions();

    // Create Driver Instance
    _Driver = new Driver("COM7", Options);

    // Subscribe to driver ready, error, and connection events
    _Driver.DriverReady += DriverReady;
    _Driver.ServerConnectionError += HandleConnectionErrors;
	_Driver.ZWaveJSError += HandleZWErrors;
   
    _Driver.Start();
}

private void HandleZWErrors(int ErrorCode, string Message)
{
    // Do something with the error
}

private void HandleConnectionErrors(string ErrorCode, string Message, Action<bool, int?> Retry)
{
    // Do something with the error, and restart if supported, setting a new timeout
    if(Retry != null)
    {
        Retry(true,15)
    }
}

private  void DriverReady()
{
    // Update a value
    ValueID VID = new ValueID();
    VID.commandClass = 135;
    VID.property = "value";
    VID.endpoint = 0;

    // Support for set Value Options
    SetValueAPIOptions SVO = new  SetValueAPIOptions();
    SVO.transitionDuration = "2s";
    SVO.volume = 30;

    // All methods return a task, as to not block the UI
    _Driver.Controller.Nodes.Get(4).SetValue(VID, 200, SVO).ContinueWith((res) => {
        if (res.Result.Success)
	    {
            SetValueResult SVR = res.Result.ResultPayloadAs<SetValueResult>();
        }
    });

    // Subscribe to value updates on a node
    _Driver.Controller.Nodes.Get(3).ValueUpdated += ValueUpdated;

    // Or All of them
    ZWaveJS.NET.ZWaveNode[] Nodes = _Driver.Controller.Nodes.Collection;
    foreach(ZWaveNode Node in Nodes)
    {
        Node.ValueUpdated += ValueUpdated;
    }

     // Other Node methods
    _Driver.Controller.Nodes.Get(4).GetDefinedValueIDs().ContinueWith((res) => {
        if(res.Result.Success)
        {
            ValueID[] ValueIDs = res.Result.ResultPayloadAs<ValueID[]>();
        }
	    else
	    {
            // See res.Result.Message and res.Result.ErrorCode
        }
    });

    // Subscribe to new nodes being added to the network
    _Driver.Controller.NodeAdded += NodeAdded;

   
}

private static void ValueUpdated(ZWaveNode Node, ValueUpdatedArgs Args)
{
   // Do something with Args
}

private static void NodeAdded(ZWaveNode Node, InclusionResultArgs Args)
{
    // Do something with the Node.
    Node.ValueUpdated += ValueUpdated;
}
```

## License 

MIT License

Copyright (c) 2021 Marcus Davies

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
