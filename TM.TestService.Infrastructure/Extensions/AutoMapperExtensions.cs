using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TM.TestService.Domain.Models.Question;
using TM.TestService.Domain.Models.Section;
using TM.TestService.Domain.Models.Test;
using TM.TestService.Infrastructure.Entities;

namespace TM.TestService.Infrastructure.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            return service;
        }
    }
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Test, TestForList>();
            CreateMap<TestForCreating, Test>();
            CreateMap<TestForEditing, Test>();
            CreateMap<Test, TestForDetails>();

            CreateMap<Section, SectionForList>();
            CreateMap<SectionForCreating, Section>();
            CreateMap<SectionForEditing, Section>();
            CreateMap<Section, SectionForDetails>();

            CreateMap<QuestionForCreating, Question>();
            CreateMap<QuestionForEditing, Question>();
            CreateMap<Question, QuestionForList>();
            CreateMap<Question, QuestionForDetails>();
        }
    }
}
