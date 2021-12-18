# ZWaveJS.NET

ZWaveJS.NET is a class library developed in .NET Core 3.1, it exposes the zwave-js Driver in .NET opening up the ability to use its runtime directly in .NET applications.

## Getting Started.

The library is in 2 parts: The assembly file its self (dll), and an accompanying **server.psi** file.  

The library uses [zwave-js-server](https://github.com/zwave-js/zwave-js-server) - but your environment does not need **node** or **npm** installed, this is what **server.psi** contains.  

Its an executable that is ran silently/hidden, and it contains everything necessary for .NET to work with zwave-js.  
**server.psi** files are platform specific, but the assembly isn't - it will run on windows, OSX and Linux, and the platform specifics i.e **node** are contained in **server.psi**.

