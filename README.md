# Serilog.Sinks.Console
[![Build status](https://ci.appveyor.com/api/projects/status/w1w3m1wyk3in1c96/branch/master?svg=true)](https://ci.appveyor.com/project/serilog/serilog-sinks-console/branch/master) [![NuGet Version](http://img.shields.io/nuget/v/Serilog.Sinks.Console.svg?style=flat)](https://www.nuget.org/packages/Serilog.Sinks.Console/) [![Documentation](https://img.shields.io/badge/docs-wiki-yellow.svg)](https://github.com/serilog/serilog/wiki) [![Join the chat at https://gitter.im/serilog/serilog](https://img.shields.io/gitter/room/serilog/serilog.svg)](https://gitter.im/serilog/serilog) [![Help](https://img.shields.io/badge/stackoverflow-serilog-orange.svg)](http://stackoverflow.com/questions/tagged/serilog)

A simple Serilog sink that writes log events as text to `STDOUT`.

For **colored console** output, check out [Serilog.Sinks.Literate](https://github.com/serilog/serilog-sinks-literate), which is designed for interactive use.

### Getting started

To use the console sink, first install the [NuGet package](https://nuget.org/packages/serilog.sinks.console):

```powershell
Install-Package Serilog.Sinks.Console
```

Then enable the sink using `WriteTo.Console()`:

```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
    
Log.Information("Hello, world!");
```

Log events will be printed to `STDOUT`:

```
2016-07-08 12:50:51 [Information] Hello, world!
```

### Output templates

The format of events to the console can be modified using the `outputTemplate` configuration parameter:

```csharp
    .WriteTo.Console(
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
```

The default template, shown in the example above, uses built-in properties like `Timestamp` and `Level`. Properties from events, including those attached using [enrichers](https://github.com/serilog/serilog/wiki/Enrichment), can also appear in the output template.

For more compact level names, use a format such as `{Level:u3}` or `{Level:w3}` for three-character upper- or lowercase level names, respectively.

### JSON output

To write JSON instead of plain text, pass an appropriate `ITextFormatter` to the `Console()` configuration method:

```csharp
    .WriteTo.Console(new JsonFormatter())
```

[Serilog.Formatting.Compact](https://github.com/serilog/serilog.formatting.compact) provides alternatives to the default `JsonFormatter`.

### XML `<appSettings>` configuration

To use the console sink with the [Serilog.Settings.AppSettings](https://github.com/serilog/serilog-settings-appsettings) package, first install that package if you haven't already done so:

```powershell
Install-Package Serilog.Settings.AppSettings
```

Instead of configuring the logger in code, call `ReadFrom.AppSettings()`:

```csharp
var log = new LoggerConfiguration()
    .ReadFrom.AppSettings()
    .CreateLogger();
```

In your application's `App.config` or `Web.config` file, specify the console sink assembly under the `<appSettings>` node:

```xml
<configuration>
  <appSettings>
    <add key="serilog:using:Console" value="Serilog.Sinks.Console" />
    <add key="serilog:write-to:Console" />
```

### JSON `appsettings.json` configuration

To use the console sink with _Microsoft.Extensions.Configuration_, for example with ASP.NET Core or .NET Core, use the [Serilog.Settings.Configuration](https://github.com/serilog/serilog-settings-configuration) package. First install that package if you have not already done so:

```powershell
Install-Package Serilog.Settings.Configuration
```

Instead of configuring the sink directly in code, call `ReadFrom.Configuration()`:

```csharp
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();
```

In your `appsettings.json` file, under the `Serilog` node, :

```json
{
  "Serilog": {
    "WriteTo": [{"Name": "Console"}]
  }
}
```

_Copyright &copy; 2016 Serilog Contributors - Provided under the [Apache License, Version 2.0](http://apache.org/licenses/LICENSE-2.0.html)._
