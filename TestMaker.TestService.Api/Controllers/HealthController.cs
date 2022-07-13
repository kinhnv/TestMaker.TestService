using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestMaker.Common.Mongodb;

namespace TestMaker.TestService.Api.Controllers
{
    public class TestTemp: MongoEntity
    {
        public string TestName { get; set; } = string.Empty;
    }

    public interface ITestTempRepository : IMongoRepository<TestTemp>
    {

    }

    public class TestTempRepository : MongoRepository<TestTemp>, ITestTempRepository
    {
        public TestTempRepository(IMongoContext context) : base(context)
        {
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly ITestTempRepository _testTempRepository;

        public HealthController(ITestTempRepository testTempRepository)
        {
            _testTempRepository = testTempRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _testTempRepository.CreateAsync(new TestTemp
            {
                TestName = "TestName1"
            });
            return Ok();
        }
    }
}
