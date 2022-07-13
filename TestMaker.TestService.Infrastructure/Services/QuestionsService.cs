﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestMaker.Common.Models;
using TestMaker.TestService.Domain.Models;
using TestMaker.TestService.Domain.Models.Quersion;
using TestMaker.TestService.Domain.Models.Question;
using TestMaker.TestService.Domain.Services;
using TestMaker.TestService.Infrastructure.Entities;
using TestMaker.TestService.Infrastructure.Repositories.Questions;

namespace TestMaker.TestService.Infrastructure.Services
{
    public class QuestionsService : IQuestionsService
    {
        private readonly IUserQuestionsRepository _questionsRepository;
        private readonly IMapper _mapper;

        public QuestionsService(IUserQuestionsRepository questionsRepository, IMapper mapper)
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

            var result = await _questionsRepository.GetAsync(question.QuestionId);
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
                (getQuestionsParams.SectionId == null || getQuestionsParams.SectionId == x.SectionId);

            var quetsions = (await _questionsRepository.GetAsync(predicate, getQuestionsParams.Skip, getQuestionsParams.Take))
                .Select(section => _mapper.Map<QuestionForList>(section));
            var count = await _questionsRepository.CountAsync(predicate);
            var result = new GetPaginationResult<QuestionForList>
            {
                Data = quetsions.ToList(),
                Page = getQuestionsParams.Page,
                Take = getQuestionsParams.Take,
                TotalPage = count
            };

            return new ServiceResult<GetPaginationResult<QuestionForList>>(result);
        }
    }
}
