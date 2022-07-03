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
        public Guid? TestId { get; set; } = null;
    }
}
