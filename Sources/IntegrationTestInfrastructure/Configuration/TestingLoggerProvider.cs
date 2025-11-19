using Microsoft.Extensions.Logging;

namespace IntegrationTestInfrastructure.Configuration;

public class TestingLoggerProvider : ILoggerProvider
{
    public void Dispose()
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new TestingLogger();
    }
}