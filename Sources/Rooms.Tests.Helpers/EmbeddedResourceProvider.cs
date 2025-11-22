using System.Reflection;

namespace Rooms.Tests.Helpers;

public static class EmbeddedResourceProvider
{
    public static byte[] GetEmbeddedResource(string resourceName)
    {
        using var resourceStream = GetEmbeddedResourceStream(resourceName);
        using var memoryStream = new MemoryStream();

        resourceStream.CopyTo(memoryStream);

        return memoryStream.ToArray();
    }

    public static Stream GetEmbeddedResourceStream(string resourceName)
    {
        var assembly = Assembly.GetCallingAssembly();

        var actualResourceName = GetResourceName(assembly, resourceName);

        return GetEmbeddedResourceStreamInner(assembly, actualResourceName);
    }

    private static Stream GetEmbeddedResourceStreamInner(Assembly assembly, string resourceName)
    {
        var stream = assembly.GetManifestResourceStream(resourceName);

        return stream ?? throw new FileNotFoundException($"Could not open stream for embedded resource [{resourceName}].");
    }

    private static string GetResourceName(Assembly assembly, string resourceName)
    {
        var resourceNames = assembly.GetManifestResourceNames();

        return resourceNames.FirstOrDefault(name => name.EndsWith(resourceName, StringComparison.OrdinalIgnoreCase))
               ?? throw new FileNotFoundException($"Could not find embedded resource [{resourceName}].");
    }
}