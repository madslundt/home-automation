using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Src.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureAndValidate<T>(this IServiceCollection @this, IConfiguration config) where T : class, new()
        {
            var collection = @this.Configure<T>(config.GetSection(typeof(T).Name));
            using (var scope = @this.BuildServiceProvider().CreateScope())
            {
                var settings = scope.ServiceProvider.GetRequiredService<IOptions<T>>();
                var configErrors = settings.Value.ValidationErrors().ToArray();
                if (configErrors.Any())
                {
                    var aggrErrors = string.Join(",", configErrors);
                    var count = configErrors.Length;
                    var configType = typeof(T).Name;
                    throw new ApplicationException(
                        $"Found {count} configuration error(s) in {configType}: {aggrErrors}");
                }
            }
            return collection;
        }
    }
}
