using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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
            return Ok(await _testsService.GetTestsAsync());
        }

        [HttpGet]
        [Route("PrepareTest")]
        public async Task<ActionResult> PrepareTestAsync(Guid testId)
        {
            return Ok(await _testsService.PrepareTestAsync(testId));
        }

        [HttpGet]
        [Route("GetCorrectAnswers")]
        public async Task<IActionResult> GetCorrectAnswersAsync(Guid testId)
        {
            return Ok(await _testsService.GetCorrectAnswersAsync(testId));
        }
    }
}
