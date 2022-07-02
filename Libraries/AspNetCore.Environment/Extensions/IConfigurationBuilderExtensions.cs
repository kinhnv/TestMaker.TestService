using AspNetCore.Environment.Configuration;
using AspNetCore.Environment.Options;
using AspNetCore.Environment.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace AspNetCore.Environment.Extensions
{
    public class AdditionalConfigurationSourceArray: List<AdditionalConfigurationSource>
    {

    }

    public static class IConfigurationBuilderExtensions
    {
        public static WebApplicationBuilder AddACS(this WebApplicationBuilder builder)
        {
            builder.WebHost.ConfigureAppConfiguration((hostingContext, config) =>
            {
                IConfigurationService service = new ConfigurationService(config);

                var source = builder.Configuration.GetSection("ACS").Get<AdditionalConfigurationSourceArray>();

                source.ForEach(acs =>
                {
                    service.AddSource(acs);
                });
            });
            return builder;
        }

        public static IServiceCollection AddModifiableOptions(this IServiceCollection service)
        {
            service.AddHttpContextAccessor();
            service.AddTransient(typeof(IModifiableOptions<>), typeof(ModifiableOptions<>));
            return service;
        }
    }
}
