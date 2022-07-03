using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.Common.Models
{
    public class GetPaginationParams
    {
        public int Page { get; set; } = 1;

        public int Take { get; set; } = 10;

        public int Skip
        {
            get
            {
                return Math.Max(Page - 1, 0) * Take;
            }
        }

        public bool IsDeleted { get; set; } = false;
    }
}
