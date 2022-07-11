using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestMaker.Common.Models;
using TestMaker.Common.Mongodb;
using TestMaker.TestService.Domain.Models.Test;
using TestMaker.TestService.Domain.Services;

namespace TestMaker.TestService.Api.Controllers
{
    public class MongoTest : MongoEntity
    {
        public override string Type => "MongoTest";
    }

    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ITestsService _testsService;
        private readonly MongoRepository<MongoTest> _mongoRepository;

        public TestsController(ITestsService testsService, Common.Mongodb.MongoRepository<MongoTest> mongoRepository)
        {
            _testsService = testsService;
            _mongoRepository = mongoRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetTests()
        {
            var result = await _testsService.GetTestsAsync(new GetTestParams());

            if (!result.Successful)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpGet]
        [Route("PrepareTest")]
        public async Task<ActionResult> PrepareTestAsync(Guid testId)
        {
            var result = await _testsService.PrepareTestAsync(testId);

            if (result is ServiceNotFoundResult<PreparedTest>)
            {
                return NotFound();
            }

            if (!result.Successful)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpGet]
        [Route("GetCorrectAnswers")]
        public async Task<IActionResult> GetCorrectAnswersAsync(Guid testId)
        {
            var result = await _testsService.GetCorrectAnswersAsync(testId);

            if (!result.Successful)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }
    }
}
