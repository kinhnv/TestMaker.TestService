using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TestMaker.Common.Extensions;
using TestMaker.Common.Models;
using TestMaker.Common.Mongodb;
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
            var result = await _testsService.PrepareTestAsync(new PrepareTestParams
            {
                TestId = testId,
                UserId = User.GetUserId()
            });

            return Ok(new ApiResult<PreparedTest>(result));
        }

        [HttpGet]
        [Route("GetCorrectAnswers")]
        public async Task<IActionResult> GetCorrectAnswersAsync(Guid testId)
        {
            var result = await _testsService.GetCorrectAnswersAsync(testId);

            return Ok(new ApiResult<IEnumerable<TestService.Domain.Models.Question.CorrectAnswer>>(result));
        }

        [HttpPost]
        [Route("SaveAnswers")]
        public async Task<IActionResult> SaveAnswersAsync(List<UserAnswer> userAnswers)
        {
            var userId = User.GetUserId();
            if (userId != null)
            {
                var result = await _testsService.SaveUserAnswers((Guid)userId, userAnswers);
                return Ok(new ApiResult(result));
            }

            return Ok(new ApiResult());
        }
    }
}
