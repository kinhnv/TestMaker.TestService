﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.TestService.Domain.Models.Question
{
    public class CorrectAnswer
    {
        public Guid QuestionId { get; set; }

        public string AnswerAsJson { get; set; }
    }
}
