using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMaker.Common.Models;
using TestMaker.TestService.Domain.Models.Section;
using TestMaker.TestService.Domain.Services;

namespace TestMaker.TestService.Api.Admin.Controllers.Admin
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

            if (!result.Successful)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetSection(Guid id)
        {
            var result = await _sectionsService.GetSectionAsync(id);

            if (result is ServiceNotFoundResult<SectionForDetails>)
            {
                return NotFound();
            }

            if (!result.Successful)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSection(Guid id, SectionForEditing section)
        {
            if (id != section.SectionId)
            {
                return BadRequest();
            }

            var result = await _sectionsService.EditSectionAsync(section);
            if (result is ServiceNotFoundResult<SectionForDetails>)
            {
                return NotFound();
            }

            if (!result.Successful)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<ActionResult> PostSection(SectionForCreating section)
        {
            var result = await _sectionsService.CreateSectionAsync(section);

            if (!result.Successful)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSection(Guid id)
        {
            var result = await _sectionsService.DeleteSectionAsync(id);
            if (result is ServiceNotFoundResult<SectionForDetails>)
            {
                return NotFound();
            }

            if (!result.Successful)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}
