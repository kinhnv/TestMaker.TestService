﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.TestService.Domain.Models.Section
{
    public class SectionForList
    {
        public Guid SectionId { get; set; }

        public string Name { get; set; }

        public Guid TestId { get; set; }
    }
}
