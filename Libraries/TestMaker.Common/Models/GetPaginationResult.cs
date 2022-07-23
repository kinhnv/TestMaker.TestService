using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.Common.Models
{
    public class GetPaginationResult<T>
    {
        public List<T> Data { get; set; } = new List<T>();

        public int Page { get; set; }

        public int Take { get; set; }

        public int TotalRecord { get; set; }

        public int TotalPage
        {
            get
            {
                return (TotalRecord / Take) + ((TotalRecord % Take) == 0 ? 0 : 1);
            }
        }

        public int FirstPage
        {
            get
            {
                return Math.Max(Page - 3, 1);
            }
        }

        public int LastPage
        {
            get
            {
                return Math.Min(Page + 3, TotalPage);
            }
        }
    }
}
