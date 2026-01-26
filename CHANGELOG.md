- v5.0.0

  - Versions
    - ZWave JS Driver Version: 15.20.0
    - ZWave JS Server Version: 3.5.0 (Schema Version 44)

  - Breaking Changes
    - Dropped frameworks, the following frameworks are now as follows:
      - net6.0
      - net7.0
      - net8.0
      - net9.0
      - net10.0
      - netstandard2.1

    - Moved the S2 callbacks to the ```ZWaveOptions``` class
      This falls inline with the Driver API Settings object, So  ```InclusionOptions``` no longer has these properties.  
      Additionally, running the library in Client mode, now requires the Driver construct take an argument of the callbacks object.  
      this also applies to ```ZWaveOptions.FromSerialized```   
      
      A null check is executed during any inclusion that require these callbacks.

    - Driver class init signature changes
      - ```public Driver(string SerialPort, ZWaveOptions Options, int ServerCommunicationPort = 50001)```
      - ```public Driver(Uri Server, InclusionUserCallbacks S2Callbacks, int SchemaVersion = 0)```

    - Re-engineered error/connection handling.  
       All Driver/Server error handling, is now handled through the **ServerConnectionError** event.  
       This event has the following signature:

       ```csharp
       ServerConnectionError(string ErrorCode, string Message, Action<bool, int?> Retry)
       ```

       The possible error codes via this event are as follows:

       ```csharp
        ZWDNET-ER-00 : Unknown Error  (Supports Retry)
        ZWDNET-ER-01 : Connection Timeout (Supports Retry)
        ZWDNET-ER-02 : Schema Mismatch
        ZWDNET-ER-03 : Fatal Error During Server Start up
       ```

       Where the error code, supports a retry, ```Retry``` will not be ```null```  
       Arguments: **Should Retry**, **New Timeout Value** (if <1, defaults to 15s)

       Effectively, the host application, is now respoabile for reconnection attempts

    - All interaction error codes have been updated  
      ```csharp
      ZWDNET-ER-04 : S2 Call backs missing
      ZWDNET-ER-05 : Invalid Strategy
      ZWDNET-ER-06 : Missing Security Keys
      ZWDNET-ER-07 : Invalid Key Length
      ZWDNET-ER-08 : Missing API key (Commercial use)
      ZWDNET-ER-09 : Use of incorrect override
      ```
    - The ```Controller.Nodes.AsArray()``` method has been replaced with a property of ```Controller.Nodes.Array```

    - The following methods/events have been renamed
      - ```ZWJSS_SetRawConfigParameterValue``` -> ```SetRawConfigParameterValue```
      - ```ZWJSS_StartListeningLogs``` -> ```StartListeningLogs```
      - ```ZWJSS_StopListeningLogs``` -> ```StopListeningLogs```
      - ```ZWJSS_LoggingEvent``` -> ```LoggingEvent```

  - Internal Changes.
    - All responses to method calls are now dispatched asynchronously on the thread pool, so user code triggered by these responses cannot block the WebSocket message handler.
    - Previously, the node, controller, and driver callbacks each created their own task after completing their prep work. Now the task is created upfront, and both the prep work and the callback execute inside that single task
    - Various optimisations to the code base for easiyer maintenance.
    - Reverted to using the actively maintained Websocket.Client library. Previously, we were relying on an outdated source‑based code copy

  - New Features.
    - Added the ability to set the PSI root folder via ```Server.PSIRoot```, this is to address some OSX quirks, with App Bundles.  
      it should only be the folder path, and not the executable name.
    - Exposed further Zwave Options
        - ```preferences```
        - ```attempts.smartStartInclusion```
        - ```attempts.firmwareUpdateOTW```
    - Added ```Driver.IsHostedMode``` property
    - Added ```Driver.ServerSchemaVersion``` property
    - Added ```Controller.sdkVersion``` property
    - Added ```Controller.firmwareVersion``` property
    - Added ```Controller.HomeIDAsHex``` property
    - Implemented ```INotifyPropertyChanged``` support for the below.  
    this improves support for MVVM and allows UI bindings to update automatically.
        - ```ZWaveNode.statistics```
        - ```ZWaveNode.status```
        - ```ZWaveNode.ready```
        - ```Controller.statistics```
        - ```Controller.isRebuildingRoutes```  
        
- v4.0.0

  - Versions
    - ZWave JS Driver Version: 15.20.0
    - ZWave JS Server Version: 3.5.0 (Schema Version 44)

  - Breaking Changes
    - Removed support for **NET45**  
      The supported frameworks are as follows: **NET 48**, **NET 5.0**, **NET 6.0**, **NET 7.0**, **NETSTANDARD 2.0**, **NETSTANDARD 2.1**
    - The **NodeStatistics** arg on the ZWaveNode class event **StatisticsUpdated**  has been renamed to **NodeStatisticsUpdatedArgs**
    - The **ControllerStatistics** arg on the Controller class event **StatisticsUpdated**  has been renamed to **ControllerStatisticsUpdatedArgs**
    - The **InclusionResult** argument has been renamed to **InclusionResultArgs**
    - The **ValueUpdated** event now uses a dedicated class for the args parameter
    - The **ValueNotification** event now uses a dedicated class for the args parameter
    - The **NodeRemoved** event now contains a reason Enum as to why it was removed.
    - The **NetworkHealDone** and **NetworkHealProgress** events now use dedicated classes for the args parameter
    - The **BeginExclusion** method now requires an Exclusion Options instance
    - The Node **BeginFirmwareUpdate** method has been renamed to **UpdateFrimware**, and requires a class instance.
    - The Node **FirmwareUpdateProgress** event now passes an args parameter
    - The Node **FirmwareUpdateFinished** event now passes an args parameter
    - Setting the Node **name**, **location** and **keepAwake** values is now only possible with methods for each.
      This is to address some unintentional communication between the Driver runtime and the lib.  
    - The method **GetAllEndpoints** has been removed, and is replaced with an **endpoints** property
    - The Controller property **isHealNetworkActive** has been renamed to **isRebuildingRoutes**
    - The Controller methods of **HealNode**, **BeginHealingNetwork**, **StopHealingNetwork** have been renamed to:  
      **RebuildNodeRoutes**, **RebuildNodeRoutes**, **StopRebuildingRoutes**
      This inccludes the associated events

  - New Features  
    - Added **SetRawConfigParameterValue** method to the ZWaveNode class.
    - Added **RefreshValues** method to the ZWaveNode class.
    - Added **RefreshCCValues** method to the ZWaveNode class.
    - Added **WaitForWakeUp** method to the ZWaveNode class.
    - Added **Ping**, method to the ZwaveNode class.
    - Added **StartListeningLogs**, **StopListeningLogs** methods and the associated events to the Driver class.
    - Added **ValueAdded**, **ValueRemoved** events. **ValueRemoved** was never added until now, **ValueAdded**, previously used the **ValueUpdated** event
    - Added **endpointLabel** to the Endpoint class
    - Added **Interview** method, to the ZWaveNode class - this should only be used if  "disableOnNodeAdded" is set to true
    - Allow specifying Refresh Info options, when re-interviewing a node.
    - Added **FirmwareUpdateOTW** method (and supporting events) to update the Controller Firmware
    - Added **SetRFRegion**, **GetRFRegion**  methods to the Controller class
    - Added **SetPowerlevel**, **GetPowerlevel**  methods to the Controller class
    - Added **GetAvailableFirmwareUpdates** methods to the Controller class
    - Added **FirmwareUpdateOTA** method to the Controller class, to update a node with the fetched Updates via **GetAvailableFirmwareUpdates**
    - Added **HardReset** method to the Driver class  
            **Warning!!!** This will Reset your controller, and will result in a clean network with no included nodes.  
    - Added **SoftReset** method to the Driver class  
    - Added the **ccSpecific** property to the **ValueMetadata** class
    - Added the ability to add new server methods to the library during runtime.  
      This is helpful if you want to use a method that is not yet implemented, or to support an older version of an exetrnal server



  - Internal changes
    - Switched to an alternative websocket client package
    - Massive structural / performance improvements
    - Better (hopefully) recovery/connection error handling
    - Redeveloped the Demo Application / Debug App
    - Updated some of the defaults in the Zwave Options to mirror the Driver defaults
    - Internal logic to ensure the server satifies the specified expected schema requested by the library


- v3.1.0
  
  - Versions
    - ZWave JS Driver Version: 9.3.0
    - ZWave JS Server Version: 1.17.0 (Schema Version 17)

  - Internal changes
    - The **CFGLogConfig** and **CFGStorage** classes can now be set exclusively.
    - The child classes of **ZWaveOptions** are now instanciated with default values when calling their constructors.
    - The **DownloadPSI** method now pulls down version locked binaries, to remove the potential for incompatible Binary/library combinations

  - New Features  
    - Added ARM prebuilt binary (Debian, RPi)
    - The **DownloadPSI** method - now has an optional override, allwowing the PSI to be focibly downloaded, i.e to ensure you have the correct version.

  - Fixes
    - Any inclusion or replace node method now checks that Security Keys are present if needed.
    - Handle unexpected WS Disconnects.

- v3.0.0

  - Versions
    - ZWave JS Driver Version: 9.3.0
    - ZWave JS Server Version: 1.17.0 (Schema Version 17)

  - Breaking Changes
    - The libary has been retargeted for **.NET Standard 2.0** and **.NET 4.5** to support a wider varitey of frameworks
    - All ZWave methods, now return a task encapsulating a **CMDResult** instance, containing the response payload (if any) including the **success** property and any error message.
    - The **NodeInterviewFailed** event now returns a **NodeInterviewFailedEventArgs** instance and no longer a generic **JObject** instance
    - The controller events **ValidateDSK** and **GrantSecurityClasses** have been removed.
    - **BeginInclusion** and **ReplaceFailedNode** now require an **InclusionOptions** instance (which contains User callbacks)
    - The **NodeRemoved** event now returns the **ZWaveNode** instance and no longer just it's ID.
    - The **NodeAdded** event now also returns an instance of **InclusionResult**.
    - **SetValueOptions** has been renamed to **SetValueAPIOptions**
    - **ValueMetaData** has been renamed to **ValueMetadata**

  - New Features
    - Added **BackupNVMRaw** method and associated user callbacks
    - Added **RestoreNVM** method and associated user callbacks
    - Added **CheckLifelineHealth** method and associated user callbacks
    - Added **Smart Start** methods and associated user callbacks
    - Added **Multicast** support

- v2.0.0

  - Versions
    - ZWave JS Driver Version: 8.10.0
    - ZWave JS Server Version: 1.14.0 (Schema Version 14)

  - Breaking Changes
    - **values** and **index** properties are now removed from the ZWaveNode class 
    - **nodeId** is now **id** on the ZWaveNode class
    - **highestSecurityClass** property has been replaced with the correct method of **GetHighestSecurityClass** on the ZWaveNode class

    The updates above are to better align the API with the ZWave JS API and its documentation.

  - Fixes
    - Node Name and location not being set up on driver init.
    - Node status is now kept in sync.

  - New Features
    - Added methods for firmware updating and associated event handlers
    - Added **Dead** event handler
    - Added **GetAllEndpoints** method
    - Added **GetEndpointCount** method
    - Added **HasSecurityClass** method
    - Added **StatisticsUpdated** event handler for both the controller and nodes
    - Added **statistics** property to both the controller and its nodes
    - Added **SupportsCCAPI** method

  - Internal changes
    - Split Node and Controller event dictionaries to isolate statistic events

  - New Features
    - Bump ZWave JS Driver

- v1.1.0

  - Versions
    - ZWave JS Driver Version: 8.9.1
    - ZWave JS Server Version: 1.14.0 (Schema Version 14)

  - New Features
    - Added **RemoveFailedNode** method
    - Added **ReplaceFailedNode** method
    - Added **inclusion aborted** event handler
    - Added ability to override the schema on which to connect to a zwave-js-server instance.  
      This allows backwards compatibility with older server versions.

  - Fixes
    - Webclient instance is now correctly disposed, after downloading the PSI.
    - Fixed platform detection logic
    - Fixed throwing exception on server process exit.

- v1.0.0

  - Versions
    - ZWave JS Driver Version: 8.9.1
    - ZWave JS Server Version: 1.14.0 (Schema Version 14)

  - Breaking Changes
    - **endpoints** object is no longer accessible on the **ZWaveNode** class, instead, they are acessed via  
      **ZWaveNode.GetEndpoint(int Index)**
    - **InvokeCCAPI** no longer accepts an **endpoint**, instead **InvokeCCAPI** is now called on the **ZWaveNode** class itself,  
      or an instance of **Endpoint** as obtained by **ZWaveNode.GetEndpoint(int Index)**

      These 2 changes now mirror the zwave-js API with regards to endpoint access.

      Examples:

      ```c#
      Driver.Controller.Nodes.Get(4).InvokeCCAPI(int CommandClass, string Method, params object[] Params)
      Driver.Controller.Nodes.Get(4).GetEndpoint(2).InvokeCCAPI(int CommandClass, string Method, params object[] Params)
      ```  
  - New Features
    - Added ability to set a nodes name
    - Added ability to set a nodes location
    - Added ability to set a flag on a node to keep it awake.
    - Added the zwave-js **getValue** method

  - Fixes
    - Fix property setter loop 

  - Changes
    - Bump ZWave JS Driver
    - Bump ZWave JS Server
    - Synchronise **isHealNetworkActive**


- v0.1.0

  - Versions
    - ZWave JS Driver Version: 8.8.3
    - ZWave JS Server Version: 1.13.0 (Scheme Version 13)

  - Initial Release
