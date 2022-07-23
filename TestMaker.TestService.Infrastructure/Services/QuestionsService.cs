using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using TestMaker.Common.Models;
using TestMaker.TestService.Domain.Models;
using TestMaker.TestService.Domain.Models.Quersion;
using TestMaker.TestService.Domain.Models.Question;
using TestMaker.TestService.Domain.Models.Question.QuestionTypes;
using TestMaker.TestService.Domain.Services;
using TestMaker.TestService.Infrastructure.Entities;
using TestMaker.TestService.Infrastructure.Repositories.Questions;

namespace TestMaker.TestService.Infrastructure.Services
{
    public class QuestionsService : IQuestionsService
    {
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IMapper _mapper;

        public QuestionsService(IQuestionsRepository questionsRepository, IMapper mapper)
        {
            _questionsRepository = questionsRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<QuestionForDetails>> CreateQuestionAsync(QuestionForCreating question)
        {
            var entity = _mapper.Map<Question>(question);

            await _questionsRepository.CreateAsync(entity);

            return await GetQuestionAsync(entity.QuestionId);
        }

        public async Task<ServiceResult> DeleteQuestionAsync(Guid questionId)
        {
            var question = await _questionsRepository.GetAsync(questionId);
            if (question == null || question.IsDeleted == true)
            {
                return new ServiceNotFoundResult<Section>(questionId.ToString());
            }
            await _questionsRepository.DeleteAsync(questionId);
            return new ServiceResult();
        }

        public async Task<ServiceResult<QuestionForDetails>> EditQuestionAsync(QuestionForEditing question)
        {
            var entity = _mapper.Map<Question>(question);

            var result = await _questionsRepository.GetAsync(question.QuestionId, true);
            if (result == null || result.IsDeleted == true)
            {
                return new ServiceNotFoundResult<QuestionForDetails>(question.QuestionId.ToString());
            }

            await _questionsRepository.UpdateAsync(entity);
            return await GetQuestionAsync(entity.QuestionId);
        }

        public async Task<ServiceResult<QuestionForDetails>> GetQuestionAsync(Guid questionId)
        {
            var question = await _questionsRepository.GetAsync(questionId);

            if (question == null)
                return new ServiceNotFoundResult<QuestionForDetails>(questionId.ToString());

            return await Task.FromResult(new ServiceResult<QuestionForDetails>(_mapper.Map<QuestionForDetails>(question)));
        }

        public async Task<ServiceResult<GetPaginationResult<QuestionForList>>> GetQuestionsAsync(GetQuestionsParams getQuestionsParams)
        {
            Expression<Func<Question, bool>> predicate = x => x.IsDeleted == getQuestionsParams.IsDeleted &&
                (getQuestionsParams.SectionId == null || getQuestionsParams.SectionId == x.SectionId) &&
                (getQuestionsParams.Content == string.Empty || x.ContentAsJson.Contains(getQuestionsParams.Content) || x.ContentAsJson.Contains(HttpUtility.HtmlEncode(getQuestionsParams.Content)));

            var questions = (await _questionsRepository.GetAsync(predicate, getQuestionsParams.Skip, getQuestionsParams.Take))
                .Select(question =>
                {
                    var questionForList =  _mapper.Map<QuestionForList>(question);
                    switch (question.Type)
                        {
                            case (int)QuestionType.MultipleChoiceQuestion:
                                var multipleChoiceQuestion = _mapper.Map<MultipleChoiceQuestion>(question);
                                questionForList.Name = multipleChoiceQuestion.Content.Question;
                                break;
                            case (int)QuestionType.BlankFillingQuestion:
                                var blankFillingQuestion = _mapper.Map<BlankFillingQuestion>(question);
                                questionForList.Name = blankFillingQuestion.Content.Question;
                                break;
                            case (int)QuestionType.SortingQuestion:
                                var sortingQuestion = _mapper.Map<SortingQuestion>(question);
                                questionForList.Name = sortingQuestion.Content.Question;
                                break;
                            case (int)QuestionType.MatchingQuestion:
                                var matchingQuestion = _mapper.Map<MatchingQuestion>(question);
                                questionForList.Name = matchingQuestion.Content.Question;
                                break;
                            default:
                                break;
                        }
                    return questionForList;
                });

            var count = await _questionsRepository.CountAsync(predicate);

            var result = new GetPaginationResult<QuestionForList>
            {
                Data = questions.ToList(),
                Page = getQuestionsParams.Page,
                Take = getQuestionsParams.Take,
                TotalRecord = count
            };

            return new ServiceResult<GetPaginationResult<QuestionForList>>(result);
        }
    }
}
