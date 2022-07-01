using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;

namespace TestMaker.TestService.Infrastructure.Entities
{
    public class Section : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SectionId { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        public Guid TestId { get; set; }
    }
}
