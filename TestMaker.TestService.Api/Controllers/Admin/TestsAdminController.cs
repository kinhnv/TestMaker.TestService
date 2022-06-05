using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMaker.TestService.Domain.Models.Test;
using TestMaker.TestService.Domain.Services;

namespace TestMaker.TestService.Api.Admin.Controllers.Admin
{
    [Route("api/Admin/Tests")]
    [ApiController]
    public class TestsAdminController : ControllerBase
    {
        private readonly ITestsService _testsService;

        public TestsAdminController(ITestsService testsService)
        {
            _testsService = testsService;
        }

        [HttpGet]
        public async Task<ActionResult> GetTests()
        {
            return Ok(await _testsService.GetTestsAsync());
        }

        [HttpGet]
        [Route("SelectOptions")]
        public async Task<ActionResult> GetSelectOptions()
        {
            return Ok(await _testsService.GetTestsAsSelectOptionsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTest(Guid id)
        {
            var test = await _testsService.GetTestAsync(id);

            if (test == null)
            {
                return NotFound();
            }

            return Ok(test);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTest(Guid id, TestForEditing test)
        {
            if (id != test.TestId)
            {
                return BadRequest();
            }

            try
            {
                await _testsService.EditTestAsync(test);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _testsService.GetTestAsync(id) == null)
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
        public async Task<ActionResult> PostTest(TestForCreating test)
        {
            var result = await _testsService.CreateTestAsync(test);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(Guid id)
        {
            if (await _testsService.GetTestAsync(id) == null)
            {
                return NotFound();
            }

            await _testsService.DeleteTestAsync(id);

            return NoContent();
        }
    }
}
