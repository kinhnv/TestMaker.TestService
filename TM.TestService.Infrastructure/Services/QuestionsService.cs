using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TM.TestService.Domain.Models.Question;
using TM.TestService.Domain.Services;
using TM.TestService.Infrastructure.Entities;

namespace TM.TestService.Infrastructure.Services
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

        public async Task<QuestionForDetails> CreateQuestionAsync(QuestionForCreating question)
        {
            var entity = _mapper.Map<Question>(question);
            _dbContext.Questions.Add(entity);
            await _dbContext.SaveChangesAsync();

            return await GetQuestionAsync(entity.QuestionId);
        }

        public async Task DeleteQuestionAsync(Guid questionId)
        {
            var question = await _dbContext.Questions.FindAsync(questionId);
            if (question != null)
            {
                _dbContext.Questions.Remove(question);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task EditQuestionAsync(QuestionForEditing question)
        {
            var entity = _mapper.Map<Question>(question);

            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<QuestionForDetails> GetQuestionAsync(Guid questionId)
        {
            var question = _dbContext.Questions.SingleOrDefault(x => x.QuestionId == questionId);

            if (question == null)
                return null;

            return await Task.FromResult(_mapper.Map<QuestionForDetails>(question));
        }

        public async Task<IEnumerable<QuestionForList>> GetQuestionsAsync()
        {
            var result = _dbContext.Questions.ToList().Select(question => _mapper.Map<QuestionForList>(question));
            return await Task.FromResult(result);
        }

        public async Task<bool> QuestionExistsAsync(Guid questionId)
        {
            return await _dbContext.Questions.AnyAsync(e => e.QuestionId == questionId);
        }
    }
}
