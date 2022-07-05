using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace TestMaker.Common.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddBearerAuthentication(this IServiceCollection service, ConfigurationManager configuration)
        {
            service.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = configuration["Server:IdentityServer"];

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                    options.RequireHttpsMetadata = false;
                });
            return service;
        }
    }
}