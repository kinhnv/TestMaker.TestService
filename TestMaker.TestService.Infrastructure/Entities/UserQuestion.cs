using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Common.Repository;

namespace TestMaker.TestService.Infrastructure.Entities
{
    public class UserQuestion : Entity
    {
        public Guid UserId { get; set; }
        
        public Guid QuestionId { get; set; }

        [Required]
        public double Rank { get; set; } = 0;
    }
}