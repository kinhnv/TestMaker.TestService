﻿using System;
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

        public int TotalPage { get; set; }
    }
}