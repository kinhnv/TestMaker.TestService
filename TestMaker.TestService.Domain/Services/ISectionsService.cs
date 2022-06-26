using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.TestService.Domain.Models.Section;

namespace TestMaker.TestService.Domain.Services
{
    public interface ISectionsService
    {
        Task<IEnumerable<SectionForList>> GetSectionsAsync(GetQuestionsRequest request);

        Task<SectionForDetails> GetSectionAsync(Guid testId);

        Task<SectionForDetails> CreateSectionAsync(SectionForCreating test);

        Task<bool> EditSectionAsync(SectionForEditing test);

        Task<bool> DeleteSectionAsync(Guid testId);
    }
}
