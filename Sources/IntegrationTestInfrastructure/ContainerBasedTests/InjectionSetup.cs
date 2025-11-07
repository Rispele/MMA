using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Interfaces;
using SkbKontur.NUnit.Middlewares;

namespace IntegrationTestInfrastructure.ContainerBasedTests;

[UsedImplicitly]
public class InjectionSetup : ISetup
{
    public Task SetUpAsync(ITest test)
    {
        var serviceProvider = test.GetFromThisOrParentContext<IServiceProvider>();
        var scope = serviceProvider.CreateScope();

        test.Properties.Set(scope);
        
        var testFixture = test.Fixture ?? throw new InvalidOperationException("Test fixture is null");
        InjectObjects(scope, testFixture);
        
        return Task.CompletedTask;
    }

    public Task TearDownAsync(ITest test)
    {
        var scope = test.GetFromThisOrParentContext<IServiceScope>();
        
        scope.Dispose();
        
        return Task.CompletedTask;
    }

    private void InjectObjects(IServiceScope scope, object testFixture)
    {
        var fieldsToInject = new List<FieldInfo>();

        var currentType = testFixture.GetType();
        while (currentType != typeof(ContainerTestBase))
        {
            var fields = currentType!.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            fieldsToInject.AddRange(fields.Where(f => f.GetCustomAttribute<InjectAttribute>() != null));

            currentType = currentType.BaseType;
        }

        fieldsToInject.ForEach(field => field.SetValue(testFixture, scope.ServiceProvider.GetRequiredService(field.FieldType)));
    }
}