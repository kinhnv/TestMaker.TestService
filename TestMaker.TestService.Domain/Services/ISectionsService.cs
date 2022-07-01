using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Models;
using TestMaker.TestService.Domain.Models.Section;

namespace TestMaker.TestService.Domain.Services
{
    public interface ISectionsService
    {
        Task<ServiceResult<GetPaginationResult<SectionForList>>> GetSectionsAsync(GetSectionsParams request);

        Task<ServiceResult<SectionForDetails>> GetSectionAsync(Guid testId);

        Task<ServiceResult<SectionForDetails>> CreateSectionAsync(SectionForCreating test);

        Task<ServiceResult<SectionForDetails>> EditSectionAsync(SectionForEditing test);

        Task<ServiceResult> DeleteSectionAsync(Guid testId);
    }
}
