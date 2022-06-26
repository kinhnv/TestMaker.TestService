using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.TestService.Domain.Models.Quersion;
using TestMaker.TestService.Domain.Models.Question;

namespace TestMaker.TestService.Domain.Services
{
    public interface IQuestionsService
    {
        Task<IEnumerable<QuestionForList>> GetQuestionsAsync(GetQuestionsRequest request);

        Task<QuestionForDetails> GetQuestionAsync(Guid questionId);

        Task<QuestionForDetails> CreateQuestionAsync(QuestionForCreating question);

        Task EditQuestionAsync(QuestionForEditing question);

        Task DeleteQuestionAsync(Guid questionId);

        Task<bool> QuestionExistsAsync(Guid questionId);
    }
}
