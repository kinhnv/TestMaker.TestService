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

        public async Task<GetPrepareTestResult> GetPrepareTestAsync(Guid testId)
        {
            var data =
                await (from test in _dbContext.Set<Test>().Where(t => t.TestId == testId)
                       join section in _dbContext.Set<Section>() on test.TestId equals section.TestId
                       join question in _dbContext.Set<Question>() on section.SectionId equals question.SectionId
                       select new GetPrepareTestResultItem
                       {
                           Test = test,
                           Section = section,
                           Question = question
                       }).ToListAsync();

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
