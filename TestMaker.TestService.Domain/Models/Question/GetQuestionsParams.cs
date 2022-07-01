using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Models;

namespace TestMaker.TestService.Domain.Models.Quersion
{
    public class GetQuestionsParams : GetPaginationParams
    {
        public GetQuestionsParams()
        {
            //Page = 1;
            //Take = 10;
        }

        public Guid? SectionId { get; set; }
    }
}
