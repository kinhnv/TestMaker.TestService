using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.TestService.Infrastructure.Entities;

namespace TM.TestService.Infrastructure.Repositories.Tests
{
    public class TestsRepository : Repository<Test>, ITestsRepository
    {
        public TestsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<GetPrepareTestResult> GetPrepareTestAsync(Guid testId)
        {
            var data =
                await (from test in _dbContext.Tests.Where(t => t.TestId == testId)
                       join section in _dbContext.Sections on test.TestId equals section.TestId
                       join question in _dbContext.Questions on section.SectionId equals question.SectionId
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
            var questions = await (from section in _dbContext.Sections.Where(s => s.TestId == testId)
                                   join question in _dbContext.Questions on section.SectionId equals question.SectionId
                                   select question).ToListAsync();

            return questions;
        }
    }
}
