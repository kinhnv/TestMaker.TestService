using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Common.Models;
using TestMaker.TestService.Domain.Models;
using TestMaker.TestService.Domain.Models.Question;
using TestMaker.TestService.Domain.Models.Question.QuestionTypes;
using TestMaker.TestService.Domain.Models.Test;
using TestMaker.TestService.Domain.Services;
using TestMaker.TestService.Infrastructure.Entities;
using TestMaker.TestService.Infrastructure.Repositories.Sections;
using TestMaker.TestService.Infrastructure.Repositories.Tests;
using static TestMaker.TestService.Domain.Models.Test.PreparedTest;
using static TestMaker.TestService.Domain.Models.Test.PreparedTest.PreparedSection;

namespace TestMaker.TestService.Infrastructure.Services
{
    public class TestsService : ITestsService
    {
        #region Fields
        private readonly ITestsRepository _testsRepository;
        private readonly ISectionsRepository _sectionsRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Ctrls
        public TestsService(
            ITestsRepository repository,
            ISectionsRepository sectionsRepository, IMapper mapper)
        {
            _testsRepository = repository;
            _sectionsRepository = sectionsRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        public async Task<ServiceResult<PreparedTest>> PrepareTestAsync(Guid testId)
        {
            var data = await _testsRepository.GetPrepareTestAsync(testId);

            if (data == null || data.Count == 0)
                return new ServiceResult<PreparedTest>("Not found any event and candidate");

            var testData = data.First().Test;

            var sections = data.GroupBy(x => x.Section.SectionId)
                .Select(g => new
                {
                    SectionId = g.First().Section.SectionId,
                    Questions = g.Select(x => x.Question)
                });

            return new ServiceResult<PreparedTest>(new PreparedTest
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
            });
        }

        public async Task<ServiceResult<IEnumerable<CorrectAnswer>>> GetCorrectAnswersAsync(Guid testId)
        {
            var questions = await _testsRepository.GetQuestionsByTestIdAsync(testId);

            return new ServiceResult<IEnumerable<CorrectAnswer>>(questions.Select(question =>
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
            }));
        }

        public async Task<ServiceResult<TestForDetails>> GetTestAsync(Guid testId)
        {
            var test = await _testsRepository.GetAsync(testId);

            if (test == null)
                return new ServiceNotFoundResult<TestForDetails>(testId.ToString());

            return new ServiceResult<TestForDetails>(await Task.FromResult(_mapper.Map<TestForDetails>(test)));
        }

        public async Task<ServiceResult<GetPaginationResult<TestForList>>> GetTestsAsync()
        {
            var result = (await _testsRepository.GetAsync()).Select(test => _mapper.Map<TestForList>(test));

            return new ServiceResult<GetPaginationResult<TestForList>>(new GetPaginationResult<TestForList>
            {
                Data = result.ToList(),
                Page = 1,
                Take = 10,
                TotalPage = 100
            });
        }

        public async Task<ServiceResult<TestForDetails>> CreateTestAsync(TestForCreating test)
        {
            var entity = _mapper.Map<Test>(test);

            await _testsRepository.CreateAsync(entity);

            return await GetTestAsync(entity.TestId);
        }

        public async Task<ServiceResult> DeleteTestAsync(Guid testId)
        {
            var test = await _testsRepository.GetAsync(testId);
            if (test == null)
            {
                return new ServiceNotFoundResult<Test>(testId.ToString());
            }
            var sections = await _sectionsRepository.GetAsync(section => section.TestId == testId && section.IsDeleted == false);
            if (sections?.Any() != true)
            {
                test.IsDeleted = true;
            }
            else
            {
                return new ServiceResult("There are some section is not deleted");
            }
            await EditTestAsync(_mapper.Map<TestForEditing>(test));
            return new ServiceResult();
        }

        public async Task<ServiceResult> EditTestAsync(TestForEditing test)
        {
            var entity = _mapper.Map<Test>(test);
            await _testsRepository.UpdateAsync(entity);
            return new ServiceResult();
        }

        public async Task<ServiceResult<IEnumerable<SelectOption>>> GetTestsAsSelectOptionsAsync()
        {
            return new ServiceResult<IEnumerable<SelectOption>> ((await _testsRepository.GetAsync()).Select(x => new SelectOption
            {
                Title = x.Name,
                Value = x.TestId.ToString()
            }));
        }
        #endregion
    }
}
