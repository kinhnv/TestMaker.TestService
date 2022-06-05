using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.TestService.Domain.Models.Test
{
    public class TestForList
    {
        public Guid TestId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
