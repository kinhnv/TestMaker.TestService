using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult> GetSections()
        {
            return Ok(await _sectionsService.GetSectionsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetSection(Guid id)
        {
            var section = await _sectionsService.GetSectionAsync(id);

            if (section == null)
            {
                return NotFound();
            }

            return Ok(section);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSection(Guid id, SectionForEditing section)
        {
            if (id != section.SectionId)
            {
                return BadRequest();
            }

            try
            {
                await _sectionsService.EditSectionAsync(section);
            }
            catch (DbUpdateConcurrencyException)
            {
                if ((await _sectionsService.GetSectionAsync(id)) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> PostSection(SectionForCreating section)
        {
            return Ok(await _sectionsService.CreateSectionAsync(section));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSection(Guid id)
        {
            if ((await _sectionsService.GetSectionAsync(id)) == null)
            {
                return NotFound();
            }

            await _sectionsService.DeleteSectionAsync(id);

            return NoContent();
        }
    }
}
