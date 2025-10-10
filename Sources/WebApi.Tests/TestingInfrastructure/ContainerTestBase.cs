using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Tests.TestingInfrastructure;

public class ContainerTestBase
{
    private IServiceScope scope;
    private IServiceProvider serviceProvider;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // var serviceCollection = ConfigureContainerInner();

        // ConfigureContainer(serviceCollection);

        serviceProvider = WebApiTestsContext.ServiceProvider;
        scope = serviceProvider.CreateScope();

        InjectObjects();
    }

    [OneTimeTearDown]
    public virtual void OneTimeTearDown()
    {
        scope.Dispose();

        if (serviceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    // protected virtual void ConfigureContainer(IServiceCollection serviceCollection)
    // {
    // }

    private void InjectObjects()
    {
        var fieldsToInject = new List<FieldInfo>();

        var currentType = GetType();
        while (currentType != typeof(ContainerTestBase))
        {
            var fields = currentType!.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            fieldsToInject.AddRange(fields.Where(f => f.GetCustomAttribute<InjectAttribute>() != null));

            currentType = currentType.BaseType;
        }

        fieldsToInject.ForEach(field => field.SetValue(this, scope.ServiceProvider.GetRequiredService(field.FieldType)));
    }
}