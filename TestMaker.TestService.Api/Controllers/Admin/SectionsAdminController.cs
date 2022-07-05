using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMaker.Common.Models;
using TestMaker.TestService.Domain.Models.Section;
using TestMaker.TestService.Domain.Services;

namespace TestMaker.TestService.Api.Controllers.Admin
{
    [Route("api/Admin/Sections")]
    [ApiController]
    public class SectionsAdminController : ControllerBase
    {
        private readonly ISectionsService _sectionsService;

        public SectionsAdminController(ISectionsService sectionsService)
        {
            _sectionsService = sectionsService;
        }

        [HttpGet]
        public async Task<ActionResult> GetSections([FromQuery]GetSectionsParams request)
        {
            var result = await _sectionsService.GetSectionsAsync(request);
            return Ok(new ApiResult<GetPaginationResult<SectionForList>>(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetSection(Guid id)
        {
            var result = await _sectionsService.GetSectionAsync(id);
            return Ok(new ApiResult<SectionForDetails>(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSection(Guid id, SectionForEditing section)
        {
            if (id != section.SectionId)
            {
                return Ok(new ApiResult());
            }

            var result = await _sectionsService.EditSectionAsync(section);
            return Ok(new ApiResult<SectionForDetails>(result));
        }

        [HttpPost]
        public async Task<ActionResult> PostSection(SectionForCreating section)
        {
            var result = await _sectionsService.CreateSectionAsync(section);
            return Ok(new ApiResult<SectionForDetails>(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSection(Guid id)
        {
            var result = await _sectionsService.DeleteSectionAsync(id);
            return Ok(new ApiResult(result));
        }
    }
}
