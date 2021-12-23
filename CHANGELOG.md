## v1.0.0 (TBC)

 - ZWave JS Driver Version: 8.9.1

 - Breaking Changes
   - **endpoints** object is no longer accessible on the **ZWaveNode** class, and instead, they are acessed via **ZWaveNode.GetEndpoint(int Index)**
   - **InvokeCCAPI** no longer accepts an **endpoint**, instead **InvokeCCAPI** is now called on the **ZWaveNode** class itself,  
     or an instance of **Endpoint** as obtained by **ZWaveNode.GetEndpoint(int Index)**

     This is more inline with the zwave-js API.

     Examples:

     ```c#
     Driver.Controller.Nodes.Get(4).InvokeCCAPI(int CommandClass, string Method, params object[] Params)
     Driver.Controller.Nodes.Get(4).GetEndpoint(2).InvokeCCAPI(int CommandClass, string Method, params object[] Params)
     ```  

 - Changes
   - Bump ZWave JS to 8.9.1


## v0.1.0

 - ZWave JS Driver Version: 8.8.3

 - Initial Release
