# ZWaveJS.NET

ZWaveJS.NET is a class library developed in .NET Core 3.1, it exposes the zwave-js Driver in .NET opening up the ability to use its runtime directly in .NET applications.

## Getting Started.

The library is in 2 parts: The assembly file its self (dll), and an accompanying **server.psi** file.  

The library uses [zwave-js-server](https://github.com/zwave-js/zwave-js-server) - but your environment does not need **node** or **npm** installed, this is what **server.psi** contains.  

Its an executable that is ran silently/hidden, and it contains the nessassry logic for .NET to work with zwave-js.
