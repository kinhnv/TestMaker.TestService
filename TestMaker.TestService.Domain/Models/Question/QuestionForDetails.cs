using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.TestService.Domain.Models.Question
{
    public class QuestionForDetails
    {
        public Guid QuestionId { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public string Media { get; set; }

        public string ContentAsJson { get; set; }

        public Guid SectionId { get; set; }
    }
}
