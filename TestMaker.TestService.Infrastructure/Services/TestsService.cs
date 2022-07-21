using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestMaker.Common.Models;
using TestMaker.TestService.Domain.Models;
using TestMaker.TestService.Domain.Models.Question;
using TestMaker.TestService.Domain.Models.Question.QuestionTypes;
using TestMaker.TestService.Domain.Models.Test;
using TestMaker.TestService.Domain.Services;
using TestMaker.TestService.Infrastructure.Entities;
using TestMaker.TestService.Infrastructure.Repositories.Questions;
using TestMaker.TestService.Infrastructure.Repositories.Sections;
using TestMaker.TestService.Infrastructure.Repositories.Tests;
using TestMaker.TestService.Infrastructure.Repositories.UserQuestions;
using static TestMaker.TestService.Domain.Models.Test.PreparedTest;
using static TestMaker.TestService.Domain.Models.Test.PreparedTest.PreparedSection;

namespace TestMaker.TestService.Infrastructure.Services
{
    public class TestsService : ITestsService
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ITestsRepository _testsRepository;
        private readonly ISectionsRepository _sectionsRepository;
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IUserQuestionsRepository _userquestionsRepository;
        #endregion

        #region Ctrls
        public TestsService(
            IMapper mapper,
            ITestsRepository repository,
            ISectionsRepository sectionsRepository, IQuestionsRepository questionsRepository, IUserQuestionsRepository userquestionsRepository)
        {
            _mapper = mapper;
            _testsRepository = repository;
            _sectionsRepository = sectionsRepository;
            _questionsRepository = questionsRepository;
            _userquestionsRepository = userquestionsRepository;
        }
        #endregion

        #region Methods

        public async Task<ServiceResult<PreparedTest>> PrepareTestAsync(PrepareTestParams @params)
        {
            var data = await _testsRepository.GetPrepareTestAsync(@params.TestId, @params.UserId);

            if (data == null || data.Count == 0)
                return new ServiceNotFoundResult<PreparedTest>(@params.TestId.ToString());

            var testData = data.First().Test;

            var sections = data.GroupBy(x => x.Section.SectionId)
                .Select(g => new
                {
                    SectionId = g.First().Section.SectionId,
                    Name = g.First().Section.Name,
                    Questions = g.Select(x => new
                    {
                        QuestionContent = x.Question,
                        Rank = x.Rank
                    }),
                });

            var preparedTest = new PreparedTest
            {
                TestId = testData.TestId,
                Name = testData.Name,
                Description = testData.Description,
                Sections = sections.Select(s => new PreparedSection
                {
                    SectionId = s.SectionId,
                    Name = s.Name,
                    Questions = s.Questions.Select(question =>
                    {
                        PreparedQuestion result = null;

                        switch (question.QuestionContent.Type)
                        {
                            case (int)QuestionType.MultipleChoiceQuestion:
                                var multipleChoiceQuestion = _mapper.Map<MultipleChoiceQuestion>(question.QuestionContent);
                                result = new PreparedQuestion
                                {
                                    QuestionId = multipleChoiceQuestion.QuestionId,
                                    Media = multipleChoiceQuestion.Media,
                                    Type = multipleChoiceQuestion.Type,
                                    QuestionAsJson = multipleChoiceQuestion.QuestionAsJson,
                                    Rank = question.Rank
                                };
                                break;
                            case (int)QuestionType.BlankFillingQuestion:
                                var blankFillingQuestion = _mapper.Map<BlankFillingQuestion>(question.QuestionContent);
                                result = new PreparedQuestion
                                {
                                    QuestionId = blankFillingQuestion.QuestionId,
                                    Media = blankFillingQuestion.Media,
                                    Type = blankFillingQuestion.Type,
                                    QuestionAsJson = blankFillingQuestion.QuestionAsJson,
                                    Rank = question.Rank
                                };
                                break;
                            case (int)QuestionType.SortingQuestion:
                                var sortingQuestion = _mapper.Map<SortingQuestion>(question.QuestionContent);
                                result = new PreparedQuestion
                                {
                                    QuestionId = sortingQuestion.QuestionId,
                                    Media = sortingQuestion.Media,
                                    Type = sortingQuestion.Type,
                                    QuestionAsJson = sortingQuestion.QuestionAsJson,
                                    Rank = question.Rank
                                };
                                break;
                            case (int)QuestionType.MatchingQuestion:
                                var matchingQuestion = _mapper.Map<MatchingQuestion>(question.QuestionContent);
                                result = new PreparedQuestion
                                {
                                    QuestionId = matchingQuestion.QuestionId,
                                    Media = matchingQuestion.Media,
                                    Type = matchingQuestion.Type,
                                    QuestionAsJson = matchingQuestion.QuestionAsJson,
                                    Rank = question.Rank
                                };
                                break;
                        }

                        return result;
                    }).Where(x => x != null)
                })
            };

            return new ServiceResult<PreparedTest>(preparedTest);
        }

        public async Task<ServiceResult<IEnumerable<CorrectAnswer>>> GetCorrectAnswersAsync(Guid testId)
        {
            var questions = await _testsRepository.GetQuestionsByTestIdAsync(testId);

            return new ServiceResult<IEnumerable<CorrectAnswer>>(questions.Select(question =>
            {
                var answerAsJson = string.Empty;
                var rationalesAsJson = string.Empty;

                switch (question.Type)
                {
                    case (int)QuestionType.MultipleChoiceQuestion:
                        var multipleChoiceQuestion = _mapper.Map<MultipleChoiceQuestion>(question);
                        answerAsJson = multipleChoiceQuestion.AnswerAsJson;
                        rationalesAsJson = multipleChoiceQuestion.AnswerAsJson;
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
                    AnswerAsJson = answerAsJson,
                    RationalesAsJson = rationalesAsJson
                };
            }));
        }

        public async Task<ServiceResult<CorrectAnswer>> GetCorrectAnswerAsync(Guid questionId)
        {
            var question = await _questionsRepository.GetAsync(questionId);

            if (question == null)
            {
                return new ServiceNotFoundResult<CorrectAnswer>(questionId);
            }

            var answerAsJson = string.Empty;
            var rationalesAsJson = string.Empty;

            switch (question.Type)
            {
                case (int)QuestionType.MultipleChoiceQuestion:
                    var multipleChoiceQuestion = _mapper.Map<MultipleChoiceQuestion>(question);
                    answerAsJson = multipleChoiceQuestion.AnswerAsJson;
                    rationalesAsJson = multipleChoiceQuestion.RationalesAsJson;
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


            return new ServiceResult<CorrectAnswer>(new CorrectAnswer
            {
                QuestionId = question.QuestionId,
                AnswerAsJson = answerAsJson,
                RationalesAsJson = rationalesAsJson
            });
        }

        public async Task<ServiceResult> SaveUserAnswers(Guid userId, IEnumerable<UserAnswer> userAnswers)
        {
            var newUserQuestions = new List<UserQuestion>();
            var oldUserQuestions = await _userquestionsRepository.GetAsync(x => userId == x.UserId);
            var questionIds = userAnswers.Select(x => x.QuestionId);
            var questions = await _questionsRepository.GetAsync(x => questionIds.Contains(x.QuestionId));

            foreach (var userAnswer in userAnswers)
            {
                var question = questions.SingleOrDefault(x => x.QuestionId == userAnswer.QuestionId);
                if (question == null)
                {
                    return new ServiceNotFoundResult<Question>(userAnswer.QuestionId);
                }
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

                var isCorrectAnswer = false;

                if (answerAsJson == userAnswer.AnswerAsJson)
                {
                    isCorrectAnswer = true;
                }

                var oldUserQuestion = oldUserQuestions.SingleOrDefault(x => x.QuestionId == question.QuestionId);
                if (oldUserQuestion != null)
                {
                    oldUserQuestion.Rank += (isCorrectAnswer ? 0.3 : -0.5);
                }
                else
                {
                    newUserQuestions.Add(new UserQuestion
                    {
                        UserId = userId,
                        QuestionId = question.QuestionId,
                        Rank = 0 + (isCorrectAnswer ? 0.3 : -0.5)
                    });
                }
            }

            await _userquestionsRepository.CreateAsync(newUserQuestions);
            await _userquestionsRepository.UpdateAsync(oldUserQuestions);

            return new ServiceResult();
        }

        public async Task<ServiceResult<TestForDetails>> GetTestAsync(Guid testId)
        {
            var test = await _testsRepository.GetAsync(testId);

            if (test == null)
                return new ServiceNotFoundResult<TestForDetails>(testId.ToString());

            return new ServiceResult<TestForDetails>(await Task.FromResult(_mapper.Map<TestForDetails>(test)));
        }

        public async Task<ServiceResult<GetPaginationResult<TestForList>>> GetTestsAsync(GetTestParams getTestParams)
        {
            Expression<Func<Test, bool>> predicate = x => x.IsDeleted == getTestParams.IsDeleted;

            var result = (await _testsRepository.GetAsync(predicate, getTestParams.Skip, getTestParams.Take))
                .Select(test => _mapper.Map<TestForList>(test));

            return new ServiceResult<GetPaginationResult<TestForList>>(new GetPaginationResult<TestForList>
            {
                Data = result.ToList(),
                Page = getTestParams.Page,
                Take = getTestParams.Take,
                TotalPage = await _testsRepository.CountAsync(predicate)
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
                return new ServiceNotFoundResult<TestForDetails>(testId.ToString());
            }
            var sections = await _sectionsRepository.GetAsync(section => section.TestId == testId && section.IsDeleted == false);
            if (sections?.Any() == true)
            {
                return new ServiceResult("There are some sections is not deleted");
            }

            await _testsRepository.DeleteAsync(testId);
            return new ServiceResult();
        }

        public async Task<ServiceResult<TestForDetails>> EditTestAsync(TestForEditing test)
        {
            var entity = _mapper.Map<Test>(test);
            var result = await _testsRepository.GetAsync(test.TestId, isNoTracked: true);
            if (result == null || result.IsDeleted == true)
            {
                return new ServiceNotFoundResult<TestForDetails>(test.TestId.ToString());
            }
            await _testsRepository.UpdateAsync(entity);

            return await GetTestAsync(entity.TestId);
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
