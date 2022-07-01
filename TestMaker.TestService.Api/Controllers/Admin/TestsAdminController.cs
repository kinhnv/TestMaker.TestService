using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMaker.Common.Models;
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
            var result = await _testsService.GetTestsAsync(new GetTestParams());
            if (!result.Successful)
            {
                return BadRequest(result.Errors);
            }
            else
            {
                return Ok(result.Data);
            }
        }

        [HttpGet]
        [Route("SelectOptions")]
        public async Task<ActionResult> GetSelectOptions()
        {
            var result = await _testsService.GetTestsAsSelectOptionsAsync();
            if (!result.Successful)
            {
                return BadRequest(result.Errors);
            }
            else
            {
                return Ok(result.Data);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTest(Guid id)
        {
            var result = await _testsService.GetTestAsync(id);

            if (result is ServiceNotFoundResult<TestForDetails>)
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
        public async Task<IActionResult> PutTest(Guid id, TestForEditing test)
        {
            if (id != test.TestId)
            {
                return BadRequest();
            }

            var result = await _testsService.EditTestAsync(test);
            if (result is ServiceNotFoundResult<TestForDetails>)
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
        public async Task<ActionResult> PostTest(TestForCreating test)
        {
            var result = await _testsService.CreateTestAsync(test);

            if (!result.Successful)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(Guid id)
        {
            var result = await _testsService.DeleteTestAsync(id);
            if (result is ServiceNotFoundResult<TestForDetails>)
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
