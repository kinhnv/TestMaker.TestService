using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMaker.Common.Models;
using TestMaker.TestService.Domain.Models;
using TestMaker.TestService.Domain.Models.Test;
using TestMaker.TestService.Domain.Services;

namespace TestMaker.TestService.Api.Admin.Controllers.Admin
{
    [Authorize]
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? null;
            var role = User.FindFirst(ClaimTypes.Role)?.Value ?? null;
            var result = await _testsService.GetTestsAsync(new GetTestParams());

            return Ok(new ApiResult<GetPaginationResult<TestForList>>(result));
        }

        [HttpGet]
        [Route("SelectOptions")]
        public async Task<ActionResult> GetSelectOptions()
        {
            var result = await _testsService.GetTestsAsSelectOptionsAsync();
            return Ok(new ApiResult<IEnumerable<SelectOption>>(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTest(Guid id)
        {
            var result = await _testsService.GetTestAsync(id);

            return Ok(new ApiResult<TestForDetails>(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTest(Guid id, TestForEditing test)
        {
            if (id != test.TestId)
            {
                return Ok(new ApiResult());
            }

            var result = await _testsService.EditTestAsync(test);

            return Ok(new ApiResult<TestForDetails>(result));
        }

        [HttpPost]
        public async Task<ActionResult> PostTest(TestForCreating test)
        {
            var result = await _testsService.CreateTestAsync(test);
            return Ok(new ApiResult<TestForDetails>(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(Guid id)
        {
            var result = await _testsService.DeleteTestAsync(id);
            return Ok(new ApiResult(result));
        }
    }
}
