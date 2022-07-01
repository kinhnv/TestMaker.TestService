using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Models;

namespace TestMaker.TestService.Domain.Models.Test
{
    public class GetTestParams : GetPaginationParams
    {
        public GetTestParams()
        {
            Page = 1;
            Take = 10;
            IsDeleted = false;
        }

        public bool IsDeleted { get; set; }
    }
}
