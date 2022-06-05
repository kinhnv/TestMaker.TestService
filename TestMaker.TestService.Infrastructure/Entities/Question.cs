using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestMaker.TestService.Infrastructure.Entities
{
    public class Question : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid QuestionId { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        public string Media { get; set; }

        public int Type { get; set; }

        [JsonIgnore]
        [Required]
        public string ContentAsJson { get; set; }

        [Required]
        public Guid SectionId { get; set; }
    }
}
