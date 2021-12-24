## v1.0.0 (Pending)

 - Versions
   - ZWave JS Driver Version: 8.9.1
   - ZWave JS Server Version: 1.14.0

 - Breaking Changes
   - **endpoints** object is no longer accessible on the **ZWaveNode** class, instead, they are acessed via **ZWaveNode.GetEndpoint(int Index)**
   - **InvokeCCAPI** no longer accepts an **endpoint**, instead **InvokeCCAPI** is now called on the **ZWaveNode** class itself,  
     or an instance of **Endpoint** as obtained by **ZWaveNode.GetEndpoint(int Index)**

     This is more inline with the zwave-js API.

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
   - ZWave JS Server Version: 1.13.0

 - Initial Release
