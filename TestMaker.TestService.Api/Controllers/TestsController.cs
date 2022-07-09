using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestMaker.Common.Models;
using TestMaker.TestService.Domain.Models.Test;
using TestMaker.TestService.Domain.Services;

namespace TestMaker.TestService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ITestsService _testsService;

        public TestsController(ITestsService testsService)
        {
            _testsService = testsService;
        }

        [HttpGet]
        public async Task<ActionResult> GetTests()
        {
            var result = await _testsService.GetTestsAsync(new GetTestParams());

            return Ok(new ApiResult<GetPaginationResult<TestForList>>(result));
        }

        [HttpGet]
        [Route("PrepareTest")]
        public async Task<ActionResult> PrepareTestAsync(Guid testId)
        {
            var result = await _testsService.PrepareTestAsync(testId);

            return Ok(new ApiResult<PreparedTest>(result));
        }

        [HttpGet]
        [Route("GetCorrectAnswers")]
        public async Task<IActionResult> GetCorrectAnswersAsync(Guid testId)
        {
            var result = await _testsService.GetCorrectAnswersAsync(testId);

            return Ok(new ApiResult<IEnumerable<TestService.Domain.Models.Question.CorrectAnswer>>(result));
        }
    }
}
