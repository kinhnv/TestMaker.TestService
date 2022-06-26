using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.TestService.Infrastructure.Repositories.Sections
{
    public class SectionsFilter
    {
        public SectionsFilter()
        {
            TestId = null;
        }
        public Guid? TestId { get; set; }
    }
}
