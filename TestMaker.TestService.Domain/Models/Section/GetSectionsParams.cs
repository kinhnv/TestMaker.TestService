using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Models;

namespace TestMaker.TestService.Domain.Models.Section
{
    public class GetSectionsParams : GetPaginationParams
    {
        public GetSectionsParams()
        {
            Page = 1;
            Take = 10;
            TestId = null;
            IsDeleted = false;
        }
        public Guid? TestId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
