using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.TestService.Domain.Models.Question.QuestionTypes
{
    public class QuestionBase
    {
        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        public int Type { get; set; }

        public string Media { get; set; }

        [Required]
        public string ContentAsJson { get; set; }

        [Required]
        public Guid SectionId { get; set; }
    }
}
