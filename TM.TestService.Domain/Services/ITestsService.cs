﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TM.TestService.Domain.Models;
using TM.TestService.Domain.Models.Question;
using TM.TestService.Domain.Models.Test;

namespace TM.TestService.Domain.Services
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
