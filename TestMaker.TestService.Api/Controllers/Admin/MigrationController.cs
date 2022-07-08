using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMaker.TestService.Infrastructure.Entities;

namespace TestMaker.TestService.Api.Controllers.Admin
{
    [Authorize]
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class MigrationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MigrationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post()
        {
            _context.Database.Migrate();

            return Ok();
        }
    }
}
