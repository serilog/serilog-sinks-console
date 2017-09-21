# Serilog.Sinks.Console [![Build status](https://ci.appveyor.com/api/projects/status/w1w3m1wyk3in1c96/branch/master?svg=true)](https://ci.appveyor.com/project/serilog/serilog-sinks-console/branch/master) [![NuGet Version](http://img.shields.io/nuget/v/Serilog.Sinks.Console.svg?style=flat)](https://www.nuget.org/packages/Serilog.Sinks.Console/) [![Documentation](https://img.shields.io/badge/docs-wiki-yellow.svg)](https://github.com/serilog/serilog/wiki) [![Join the chat at https://gitter.im/serilog/serilog](https://img.shields.io/gitter/room/serilog/serilog.svg)](https://gitter.im/serilog/serilog) [![Help](https://img.shields.io/badge/stackoverflow-serilog-orange.svg)](http://stackoverflow.com/questions/tagged/serilog)

A Serilog sink that writes log events to the Windows Console or an ANSI terminal via standard output. Coloring and custom themes are supported, including ANSI 256-color themes on macOS, Linux and Windows 10. The default output is plain text; JSON formatting can be plugged in using a package such as [_Serilog.Formatting.Compact_](https://github.com/serilog/serilog-formatting-compact).

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
[12:50:51 INF] Hello, world!
```

### Themes

The sink will colorize output by default:

![Colorized Console](https://raw.githubusercontent.com/serilog/serilog-sinks-console/dev/assets/Screenshot.png)

Themes can be specified when configuring the sink:

```csharp
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
```

The following built-in themes are available:

 * `ConsoleTheme.None` - no styling
 * `SystemConsoleTheme.Literate` - styled to replicate _Serilog.Sinks.Literate_, using the `System.Console` coloring modes supported on all Windows/.NET targets; **this is the default when no theme is specified**
 * `SystemConsoleTheme.Grayscale` - a theme using only shades of gray, white, and black
 * `AnsiConsoleTheme.Literate` - an ANSI 16-color version of the "literate" theme; we expect to update this to use 256-colors for a more refined look in future
 * `AnsiConsoleTheme.Grayscale` - an ANSI 256-color version of the "grayscale" theme
 * `AnsiConsoleTheme.Code` - an ANSI 256-color Visual Studio Code-inspired theme

 Adding a new theme is straightforward; examples can be found in the [`SystemConsoleThemes`](https://github.com/serilog/serilog-sinks-console/blob/dev/src/Serilog.Sinks.Console/Sinks/SystemConsole/Themes/SystemConsoleThemes.cs) and [`AnsiConsoleThemes`](https://github.com/serilog/serilog-sinks-console/blob/dev/src/Serilog.Sinks.Console/Sinks/SystemConsole/Themes/AnsiConsoleThemes.cs) classes.

### Output templates

The format of events to the console can be modified using the `outputTemplate` configuration parameter:

```csharp
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
```

The default template, shown in the example above, uses built-in properties like `Timestamp` and `Level`. Properties from events, including those attached using [enrichers](https://github.com/serilog/serilog/wiki/Enrichment), can also appear in the output template.

### JSON output

The sink can write JSON  output instead of plain text. `CompactJsonFormatter` or `RenderedCompactJsonFormatter` from [Serilog.Formatting.Compact](https://github.com/serilog/serilog-formatting-compact) is recommended:

```powershell
Install-Package Serilog.Formatting.Compact
```

Pass a formatter to the `Console()` configuration method:

```csharp
    .WriteTo.Console(new CompactJsonFormatter())
```

Output theming is not available when custom formatters are used.

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

### Upgrading from _Serilog.Sinks.Console_ 2.x

To achieve output identical to version 2 of this sink, specify a formatter and output template explicitly:

```csharp
    .WriteTo.Console(new MessageTemplateTextFormatter(
        "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}",
        null))
```

This will bypass theming and use Serilog's built-in message template formatting.

### Contributing

Would you like to help make the Serilog console sink even better? We keep a list of issues that are approachable for newcomers under the [up-for-grabs](https://github.com/serilog/serilog-sinks-console/issues?labels=up-for-grabs&state=open) label. Before starting work on a pull request, we suggest commenting on, or raising, an issue on the issue tracker so that we can help and coordinate efforts.  For more details check out our [contributing guide](CONTRIBUTING.md).

When contributing please keep in mind our [Code of Conduct](CODE_OF_CONDUCT.md).


### Detailed build status

Branch  | AppVeyor | Travis
------------- | ------------- |-------------
dev | [![Build status](https://ci.appveyor.com/api/projects/status/w1w3m1wyk3in1c96/branch/dev?svg=true)](https://ci.appveyor.com/project/serilog/serilog-sinks-console/branch/dev)  | [![Build Status](https://travis-ci.org/serilog/serilog-sinks-console.svg?branch=dev)](https://travis-ci.org/serilog/serilog-sinks-console) 
master | [![Build status](https://ci.appveyor.com/api/projects/status/w1w3m1wyk3in1c96/branch/master?svg=true)](https://ci.appveyor.com/project/serilog/serilog-sinks-console/branch/master) | [![Build Status](https://travis-ci.org/serilog/serilog-sinks-console.svg?branch=master)](https://travis-ci.org/serilog/serilog-sinks-console) 


_Copyright &copy; 2017 Serilog Contributors - Provided under the [Apache License, Version 2.0](http://apache.org/licenses/LICENSE-2.0.html)._
