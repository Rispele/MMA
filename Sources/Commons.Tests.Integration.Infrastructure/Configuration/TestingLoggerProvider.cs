using Microsoft.Extensions.Logging;

namespace Commons.Tests.Integration.Infrastructure.Configuration;

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