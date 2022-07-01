using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Models;
using TestMaker.TestService.Domain.Models;
using TestMaker.TestService.Domain.Models.Quersion;
using TestMaker.TestService.Domain.Models.Question;

namespace TestMaker.TestService.Domain.Services
{
    public interface IQuestionsService
    {
        Task<ServiceResult<GetPaginationResult<QuestionForList>>> GetQuestionsAsync(GetQuestionsParams request);

        Task<ServiceResult<QuestionForDetails>> GetQuestionAsync(Guid questionId);

        Task<ServiceResult<QuestionForDetails>> CreateQuestionAsync(QuestionForCreating question);

        Task<ServiceResult<QuestionForDetails>> EditQuestionAsync(QuestionForEditing question);

        Task<ServiceResult> DeleteQuestionAsync(Guid questionId);
    }
}
