## v0.2.0

 - Breaking Changes
  - **endpoints** object is no longer accessible on the **ZWaveNode** class, and instead, they are acessed via **ZWaveNode.GetEndpoint(int Index)**
  - **InvokeCCAPI** no longer accepts an **endpoint**, instead **InvokeCCAPI** is now called the **ZWaveNode** class itsself,  
    or an instance of **Endpoint** as obtained by **ZWaveNode.GetEndpoint(int Index)**

 - Changes
  - Bump ZWave JS to 8.9.1


## v0.1.0
 - Initial Release
 - ZWave JS Driver Version: 8.8.3
