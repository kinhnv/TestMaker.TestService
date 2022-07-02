using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.Common.Models
{
    public class GetPaginationParams
    {
        public GetPaginationParams()
        {
            Page = 1;
            Take = 10;
        }
        public int Page { get; set; }

        public int Take { get; set; }

        public int Skip
        {
            get
            {
                return Math.Max(Page - 1, 0) * Take;
            }
        }
    }
}
