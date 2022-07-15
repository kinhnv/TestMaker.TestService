using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TestMaker.Common.Mongodb;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TestMaker.Common.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddBearerAuthentication(this IServiceCollection service, ConfigurationManager configuration)
        {
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

        public static IServiceCollection AddMongoContext(this IServiceCollection service, string connectionString)
        {
            service.AddSingleton<IMongoContext, MongoContext>(x =>
            {
                return new MongoContext(new MongoDbSettings(connectionString));
            });

            return service;
        }
    }
}