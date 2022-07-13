using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.TestService.Domain.Models.Test
{
    public class UserAnswer
    {
        public Guid QuestionId { get; set; }

        public string AnswerAsJson { get; set; }
    }
}
