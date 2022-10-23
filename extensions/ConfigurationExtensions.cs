using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Marti.Core.Extensions;

public static class ConfigurationExtensions
{
    public static void Add<T>(this ConfigurationManager configuration, IServiceCollection collection, string key) where T : class
    {
        var instance = Activator.CreateInstance<T>();
        configuration.GetSection(key).Bind(instance);
        collection.AddSingleton(instance);
    }
}