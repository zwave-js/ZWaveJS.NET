- v4.0.0

  - Versions
    - ZWave JS Driver Version: 11.13.0
    - ZWave JS Server Version: 1.31.0 (Schema Version 30)

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

  - New Features  
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

  - Internal changes
    - Switched to an alternative websocket client package
    - Massive structural / performance improvements
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
