﻿using Microsoft.Extensions.DependencyInjection;
using TestMaker.TestService.Domain.Services;
using TestMaker.TestService.Infrastructure.Repositories.Questions;
using TestMaker.TestService.Infrastructure.Repositories.Sections;
using TestMaker.TestService.Infrastructure.Repositories.Tests;
using TestMaker.TestService.Infrastructure.Repositories.UserQuestions;
using TestMaker.TestService.Infrastructure.Services;

namespace TestMaker.TestService.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddTransientInfrastructure(this IServiceCollection service)
        {
            service.AddAutoMapperProfiles();

            // Repositories
            service.AddTransient<ITestsRepository, TestsRepository>();
            service.AddTransient<ISectionsRepository, SectionsRepository>();
            service.AddTransient<IQuestionsRepository, QuestionsRepository>();
            service.AddTransient<IUserQuestionsRepository, UserQuestionsRepository>();

            service.AddTransient<ITestsService, TestsService>();
            service.AddTransient<ISectionsService, SectionsService>();
            service.AddTransient<IQuestionsService, QuestionsService>();
            return service;
        }
    }
}
