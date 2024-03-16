using PublicApiGenerator;
using Shouldly;
using Xunit;

namespace Serilog.Sinks.Console.Tests.Approval;

public class ApiApprovalTests
{
    [Fact]
    public void PublicApi_Should_Not_Change_Unintentionally()
    {
        var assembly = typeof(ConsoleLoggerConfigurationExtensions).Assembly;
        var publicApi = assembly.GeneratePublicApi(new()
        {
            IncludeAssemblyAttributes = false,
            ExcludeAttributes = ["System.Diagnostics.DebuggerDisplayAttribute"],
        });

        publicApi.ShouldMatchApproved(options => options.WithFilenameGenerator((_, __, fileType, fileExtension) => $"{assembly.GetName().Name!}.{fileType}.{fileExtension}"));
    }
}
