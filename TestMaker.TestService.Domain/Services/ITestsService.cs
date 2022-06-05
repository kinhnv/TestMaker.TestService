using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestMaker.TestService.Domain.Models;
using TestMaker.TestService.Domain.Models.Question;
using TestMaker.TestService.Domain.Models.Test;

namespace TestMaker.TestService.Domain.Services
{
    public interface ITestsService
    {
        Task<PreparedTest> PrepareTestAsync(Guid testId);

        Task<IEnumerable<CorrectAnswer>> GetCorrectAnswersAsync(Guid testId);

        Task<IEnumerable<TestForList>> GetTestsAsync();

        Task<TestForDetails> GetTestAsync(Guid testId);

        Task<TestForDetails> CreateTestAsync(TestForCreating test);

        Task<bool> EditTestAsync(TestForEditing test);

        Task<bool> DeleteTestAsync(Guid testId);

        Task<IEnumerable<SelectOption>> GetTestsAsSelectOptionsAsync();
    }
}
