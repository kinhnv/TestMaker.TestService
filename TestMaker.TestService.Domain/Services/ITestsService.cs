using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestMaker.Common.Models;
using TestMaker.TestService.Domain.Models;
using TestMaker.TestService.Domain.Models.Question;
using TestMaker.TestService.Domain.Models.Test;

namespace TestMaker.TestService.Domain.Services
{
    public interface ITestsService
    {
        Task<ServiceResult<PreparedTest>> PrepareTestAsync(PrepareTestParams @params);

        Task<ServiceResult<IEnumerable<CorrectAnswer>>> GetCorrectAnswersAsync(Guid testId);

        Task<ServiceResult<CorrectAnswer>> GetCorrectAnswerAsync(Guid questionId);

        Task<ServiceResult> SaveUserAnswers(Guid userId, IEnumerable<UserAnswer> userAnswers);

        Task<ServiceResult<GetPaginationResult<TestForList>>> GetTestsAsync(GetTestParams getTestParams);

        Task<ServiceResult<TestForDetails>> GetTestAsync(Guid testId);

        Task<ServiceResult<TestForDetails>> CreateTestAsync(TestForCreating test);

        Task<ServiceResult<TestForDetails>> EditTestAsync(TestForEditing test);

        Task<ServiceResult> DeleteTestAsync(Guid testId);

        Task<ServiceResult<IEnumerable<SelectOption>>> GetTestsAsSelectOptionsAsync();
    }
}
