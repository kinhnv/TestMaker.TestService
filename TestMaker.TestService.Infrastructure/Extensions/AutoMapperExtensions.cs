using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TestMaker.TestService.Domain.Models.Question;
using TestMaker.TestService.Domain.Models.Question.QuestionTypes;
using TestMaker.TestService.Domain.Models.Section;
using TestMaker.TestService.Domain.Models.Test;
using TestMaker.TestService.Infrastructure.Entities;

namespace TestMaker.TestService.Infrastructure.Extensions
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
            CreateMap<Test, TestForEditing>();
            CreateMap<Test, TestForDetails>();

            CreateMap<Section, SectionForList>();
            CreateMap<SectionForCreating, Section>();
            CreateMap<SectionForEditing, Section>();
            CreateMap<Section, SectionForDetails>();

            CreateMap<QuestionForCreating, Question>();
            CreateMap<QuestionForEditing, Question>();
            CreateMap<Question, QuestionForList>();
            CreateMap<Question, QuestionForDetails>();

            CreateMap<Question, MultipleChoiceQuestion>();
            CreateMap<Question, BlankFillingQuestion>();
            CreateMap<Question, SortingQuestion>();
            CreateMap<Question, MatchingQuestion>();
        }
    }
}
