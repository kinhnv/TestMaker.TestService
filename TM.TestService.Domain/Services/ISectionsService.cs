using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.TestService.Domain.Models.Section;

namespace TM.TestService.Domain.Services
{
    public interface ISectionsService
    {
        Task<IEnumerable<SectionForList>> GetSectionsAsync();

        Task<SectionForDetails> GetSectionAsync(Guid testId);

        Task<SectionForDetails> CreateSectionAsync(SectionForCreating test);

        Task<bool> EditSectionAsync(SectionForEditing test);

        Task<bool> DeleteSectionAsync(Guid testId);
    }
}
