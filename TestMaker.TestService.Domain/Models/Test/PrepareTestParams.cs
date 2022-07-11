using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.TestService.Domain.Models.Test
{
    public class PrepareTestParams
    {
        public Guid TestId { get; set; }

        public Guid? UserId { get; set; } = null;
    }
}
