using Microsoft.Extensions.Logging;
using NUnit.Framework.Internal;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace IntegrationTestInfrastructure.Configuration;

public class TestingLogger : ILogger
{
    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        TestExecutionContext.CurrentContext.OutWriter.WriteLine($"{logLevel}: {formatter(state, exception)}");

        if (exception != null)
        {
            TestExecutionContext.CurrentContext.OutWriter.WriteLine($"Exception: {exception}");
        }
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
}