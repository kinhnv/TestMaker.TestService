using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Common.Models;
using TestMaker.TestService.Domain.Models;
using TestMaker.TestService.Domain.Models.Quersion;
using TestMaker.TestService.Domain.Models.Question;
using TestMaker.TestService.Domain.Services;
using TestMaker.TestService.Infrastructure.Entities;

namespace TestMaker.TestService.Infrastructure.Services
{
    public class QuestionsService : IQuestionsService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public QuestionsService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ServiceResult<QuestionForDetails>> CreateQuestionAsync(QuestionForCreating question)
        {
            var entity = _mapper.Map<Question>(question);
            _dbContext.Questions.Add(entity);
            await _dbContext.SaveChangesAsync();

            return await GetQuestionAsync(entity.QuestionId);
        }

        public async Task<ServiceResult> DeleteQuestionAsync(Guid questionId)
        {
            var question = await _dbContext.Questions.FindAsync(questionId);
            if (question == null)
            {
                return new ServiceNotFoundResult<Question>(questionId.ToString());
            }
            _dbContext.Questions.Remove(question);
            await _dbContext.SaveChangesAsync();
            return new ServiceResult();
        }

        public async Task<ServiceResult> EditQuestionAsync(QuestionForEditing question)
        {
            var entity = _mapper.Map<Question>(question);

            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
            return new ServiceResult();
        }

        public async Task<ServiceResult<QuestionForDetails>> GetQuestionAsync(Guid questionId)
        {
            var question = _dbContext.Questions.SingleOrDefault(x => x.QuestionId == questionId);

            if (question == null)
                return new ServiceNotFoundResult<QuestionForDetails>(questionId.ToString());

            return await Task.FromResult(new ServiceResult<QuestionForDetails>(_mapper.Map<QuestionForDetails>(question)));
        }

        public async Task<ServiceResult<GetPaginationResult<QuestionForList>>> GetQuestionsAsync(GetQuestionsParams request)
        {
            var query = _dbContext.Questions.AsQueryable();
            if (request?.SectionId != null)
            {
                query = query.Where(x => x.SectionId == request.SectionId);
            }
            var result = query.Skip(request.Skip).Take(request.Take).ToList().Select(question => _mapper.Map<QuestionForList>(question));
            return await Task.FromResult(new ServiceResult<GetPaginationResult<QuestionForList>>(new GetPaginationResult<QuestionForList>
            {
                Data = result.ToList(),
                Page = request.Page,
                Take = request.Take,
                TotalPage = query.Count()
            }));
        }
    }
}
