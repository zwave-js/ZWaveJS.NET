## v2.0.0

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

 - New Features
   - Added methods for firmware updating and associated event handlers
   - Added **Dead** event handler
   - Added **GetAllEndpoints** method
   - Added **GetEndpointCount** method
   - Added **HasSecurityClass** method
   - Added **StatisticsUpdated** event handler for both the controller and nodes
   - Added **SupportsCCAPI** method

 - Internal changes
   - Split Node and Controller event dictionaries to isolate statistic events

 - New Features
   - Bump ZWave JS Driver

## v1.1.0

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

## v1.0.0

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


## v0.1.0

 - Versions
   - ZWave JS Driver Version: 8.8.3
   - ZWave JS Server Version: 1.13.0 (Scheme Version 13)

 - Initial Release
