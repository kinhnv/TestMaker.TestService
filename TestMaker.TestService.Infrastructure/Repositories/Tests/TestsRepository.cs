using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;
using TestMaker.TestService.Infrastructure.Entities;

namespace TestMaker.TestService.Infrastructure.Repositories.Tests
{
    public class TestsRepository : Repository<Test>, ITestsRepository
    {
        public TestsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<GetPrepareTestResult> GetPrepareTestAsync(Guid testId, Guid? userId)
        {
            var questionUserAsQueryable = _dbContext.Set<UserQuestion>().AsQueryable();


            var data =
                await (from test in _dbContext.Set<Test>().Where(t => t.TestId == testId)
                       join section in _dbContext.Set<Section>() on test.TestId equals section.TestId
                       join question in _dbContext.Set<Question>() on section.SectionId equals question.SectionId
                       select new GetPrepareTestResultItem
                       {
                           Test = test,
                           Section = section,
                           Question = question,
                           Rank = 0
                       }).ToListAsync();

            var questionIds = data.Select(x => x.Question.QuestionId).ToList();

            if (userId != null)
            {
                var questionUsers = questionUserAsQueryable.Where(x => x.UserId == userId && questionIds.Contains(x.QuestionId)).ToList();

                data.ForEach(x =>
                {
                    var questionUser = questionUsers.SingleOrDefault(q => q.QuestionId == x.Question.QuestionId);
                    if (questionUser != null)
                    {
                        x.Rank = questionUser.Rank;
                    }
                });
            }

            return new GetPrepareTestResult(data);
        }

        public async Task<List<Question>> GetQuestionsByTestIdAsync(Guid testId)
        {
            var questions = await (from section in _dbContext.Set<Section>().Where(s => s.TestId == testId)
                                   join question in _dbContext.Set<Question>() on section.SectionId equals question.SectionId
                                   select question).ToListAsync();

            return questions;
        }
    }
}
