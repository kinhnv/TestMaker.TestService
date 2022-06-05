using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.TestService.Domain.Models;
using TestMaker.TestService.Domain.Models.Question;
using TestMaker.TestService.Domain.Models.Question.QuestionTypes;
using TestMaker.TestService.Domain.Models.Test;
using TestMaker.TestService.Domain.Services;
using TestMaker.TestService.Infrastructure.Entities;
using TestMaker.TestService.Infrastructure.Repositories.Tests;
using static TestMaker.TestService.Domain.Models.Test.PreparedTest;
using static TestMaker.TestService.Domain.Models.Test.PreparedTest.PreparedSection;

namespace TestMaker.TestService.Infrastructure.Services
{
    public class TestsService : ITestsService
    {
        #region Fields
        private readonly ITestsRepository _testsRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Ctrls
        public TestsService(ITestsRepository repository, IMapper mapper)
        {
            _testsRepository = repository;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        public async Task<PreparedTest> PrepareTestAsync(Guid testId)
        {
            var data = await _testsRepository.GetPrepareTestAsync(testId);

            var testData = data.First().Test;

            var sections = data.GroupBy(x => x.Section.SectionId)
                .Select(g => new
                {
                    SectionId = g.First().Section.SectionId,
                    Questions = g.Select(x => x.Question)
                });

            return new PreparedTest
            {
                TestId = testData.TestId,
                Name = testData.Name,
                Description = testData.Description,
                Sections = sections.Select(s => new PreparedSection
                {
                    SectionId = s.SectionId,
                    Questions = s.Questions.Select(question =>
                    {
                        PreparedQuestion result = null;

                        switch (question.Type)
                        {
                            case (int)QuestionType.MultipleChoiceQuestion:
                                var multipleChoiceQuestion = _mapper.Map<MultipleChoiceQuestion>(question);
                                result = new PreparedQuestion
                                {
                                    QuestionId = multipleChoiceQuestion.QuestionId,
                                    Media = multipleChoiceQuestion.Media,
                                    Type = multipleChoiceQuestion.Type,
                                    QuestionAsJson = multipleChoiceQuestion.QuestionAsJson
                                };
                                break;
                            case (int)QuestionType.BlankFillingQuestion:
                                var blankFillingQuestion = _mapper.Map<BlankFillingQuestion>(question);
                                result = new PreparedQuestion
                                {
                                    QuestionId = blankFillingQuestion.QuestionId,
                                    Media = blankFillingQuestion.Media,
                                    Type = blankFillingQuestion.Type,
                                    QuestionAsJson = blankFillingQuestion.QuestionAsJson
                                };
                                break;
                            case (int)QuestionType.SortingQuestion:
                                var sortingQuestion = _mapper.Map<SortingQuestion>(question);
                                result = new PreparedQuestion
                                {
                                    QuestionId = sortingQuestion.QuestionId,
                                    Media = sortingQuestion.Media,
                                    Type = sortingQuestion.Type,
                                    QuestionAsJson = sortingQuestion.QuestionAsJson
                                };
                                break;
                            case (int)QuestionType.MatchingQuestion:
                                var matchingQuestion = _mapper.Map<MatchingQuestion>(question);
                                result = new PreparedQuestion
                                {
                                    QuestionId = matchingQuestion.QuestionId,
                                    Media = matchingQuestion.Media,
                                    Type = matchingQuestion.Type,
                                    QuestionAsJson = matchingQuestion.QuestionAsJson
                                };
                                break;
                        }

                        return result;
                    }).Where(x => x != null)
                })
            };
        }

        public async Task<IEnumerable<CorrectAnswer>> GetCorrectAnswersAsync(Guid testId)
        {
            var questions = await _testsRepository.GetQuestionsByTestIdAsync(testId);

            return questions.Select(question =>
            {
                var answerAsJson = string.Empty;

                switch (question.Type)
                {
                    case (int)QuestionType.MultipleChoiceQuestion:
                        var multipleChoiceQuestion = _mapper.Map<MultipleChoiceQuestion>(question);
                        answerAsJson = multipleChoiceQuestion.AnswerAsJson;
                        break;
                    case (int)QuestionType.BlankFillingQuestion:
                        var blankFillingQuestion = _mapper.Map<BlankFillingQuestion>(question);
                        answerAsJson = blankFillingQuestion.AnswerAsJson;
                        break;
                    case (int)QuestionType.SortingQuestion:
                        var sortingQuestion = _mapper.Map<SortingQuestion>(question);
                        answerAsJson = sortingQuestion.AnswerAsJson;
                        break;
                    case (int)QuestionType.MatchingQuestion:
                        var matchingQuestion = _mapper.Map<MatchingQuestion>(question);
                        answerAsJson = matchingQuestion.AnswerAsJson;
                        break;
                }

                return new CorrectAnswer
                {
                    QuestionId = question.QuestionId,
                    AnswerAsJson = answerAsJson
                };
            });
        }

        public async Task<TestForDetails> GetTestAsync(Guid testId)
        {
            var test = await _testsRepository.GetAsync(testId);

            if (test == null)
                return null;

            return await Task.FromResult(_mapper.Map<TestForDetails>(test));
        }

        public async Task<IEnumerable<TestForList>> GetTestsAsync()
        {
            var result = (await _testsRepository.GetAllAsync()).Select(test => _mapper.Map<TestForList>(test));

            return result;
        }

        public async Task<TestForDetails> CreateTestAsync(TestForCreating test)
        {
            var entity = _mapper.Map<Test>(test);

            var result = await _testsRepository.CreateAsync(entity);
            if (result)
                return await GetTestAsync(entity.TestId);
            else
                return null;
        }

        public async Task<bool> DeleteTestAsync(Guid testId)
        {
            return await _testsRepository.DeleteAsync(testId);
        }

        public async Task<bool> EditTestAsync(TestForEditing test)
        {
            var entity = _mapper.Map<Test>(test);

            return await _testsRepository.UpdateAsync(entity);
        }

        public async Task<IEnumerable<SelectOption>> GetTestsAsSelectOptionsAsync()
        {
            return (await _testsRepository.GetAllAsync()).Select(x => new SelectOption
            {
                Title = x.Name,
                Value = x.TestId.ToString()
            });
        }
        #endregion
    }
}
