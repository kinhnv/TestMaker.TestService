using Microsoft.Extensions.DependencyInjection;
using TM.TestService.Domain.Services;
using TM.TestService.Infrastructure.Repositories.Sections;
using TM.TestService.Infrastructure.Repositories.Tests;
using TM.TestService.Infrastructure.Services;

namespace TM.TestService.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddTransient(this IServiceCollection service)
        {
            service.AddAutoMapperProfiles();

            // Repositories
            service.AddTransient<ITestsRepository, TestsRepository>();
            service.AddTransient<ISectionsRepository, SectionsRepository>();

            service.AddTransient<ITestsService, TestsService>();
            service.AddTransient<ISectionsService, SectionsService>();
            service.AddTransient<IQuestionsService, QuestionsService>();
            return service;
        }
    }
}
