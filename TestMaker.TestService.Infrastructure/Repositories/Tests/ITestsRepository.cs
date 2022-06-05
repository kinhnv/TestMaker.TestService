using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.TestService.Infrastructure.Entities;

namespace TestMaker.TestService.Infrastructure.Repositories.Tests
{
    public interface ITestsRepository: IRepository<Test>
    {
        Task<GetPrepareTestResult> GetPrepareTestAsync(Guid testId);

        Task<List<Question>> GetQuestionsByTestIdAsync(Guid testId);
    }
}
