using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestMaker.TestService.Infrastructure.Entities
{
    public class QuestionUser
    {
        public Guid UserId { get; set; }
        
        public Guid QuestionId { get; set; }
        
        [Required]
        public int Rank { get; set; }
    }
}