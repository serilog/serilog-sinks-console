# Serilog.Sinks.Console

The console sink for Serilog.
 
[![Build status](https://ci.appveyor.com/api/projects/status/w1w3m1wyk3in1c96/branch/master?svg=true)](https://ci.appveyor.com/project/serilog/serilog-sinks-console/branch/master) [![NuGet Version](http://img.shields.io/nuget/v/Serilog.Sinks.Console.svg?style=flat)](https://www.nuget.org/packages/Serilog.Sinks.Console/)

Writes to the system console. The colored console sink's boring cousin.

To use the console sink, first install the NuGet package:

```powershell
Install-Package Serilog.Sinks.Console
```

```csharp
var log = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
```

* [Documentation](https://github.com/serilog/serilog/wiki)

Copyright &copy; 2016 Serilog Contributors - Provided under the [Apache License, Version 2.0](http://apache.org/licenses/LICENSE-2.0.html).
