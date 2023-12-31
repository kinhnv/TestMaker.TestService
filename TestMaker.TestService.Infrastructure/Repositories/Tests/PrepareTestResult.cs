﻿using System.Collections.Generic;
using TestMaker.TestService.Infrastructure.Entities;

namespace TestMaker.TestService.Infrastructure.Repositories.Tests
{
    public class GetPrepareTestResultItem
    {
        public Test Test { get; set; }

        public Section Section { get; set; }

        public Question Question { get; set; }

        public double Rank { get; set; }
    }

    public class GetPrepareTestResult: List<GetPrepareTestResultItem>
    {
        public GetPrepareTestResult(List<GetPrepareTestResultItem> items)
        {
            items.ForEach(item =>
            {
                Add(item);
            });
        }
    }
}
