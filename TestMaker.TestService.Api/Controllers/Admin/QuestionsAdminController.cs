using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMaker.Common.Models;
using TestMaker.TestService.Domain.Models.Quersion;
using TestMaker.TestService.Domain.Models.Question;
using TestMaker.TestService.Domain.Services;

namespace TestMaker.TestService.Api.Admin.Controllers.Admin
{
    [Route("api/Admin/Questions")]
    [ApiController]
    public class QuestionsAdminController : ControllerBase
    {
        private readonly IQuestionsService _questionsService;

        public QuestionsAdminController(IQuestionsService questionsService)
        {
            _questionsService = questionsService;
        }

        [HttpGet]
        public async Task<ActionResult> GetQuestions([FromQuery]GetQuestionsParams request)
        {
            var result = await _questionsService.GetQuestionsAsync(request);

            if (!result.Successful)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetQuestion(Guid id)
        {
            var result = await _questionsService.GetQuestionAsync(id);

            if (result is ServiceNotFoundResult<QuestionForDetails>)
            {
                return NotFound();
            }

            if (!result.Successful)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(Guid id, QuestionForEditing question)
        {
            if (id != question.QuestionId)
            {
                return BadRequest();
            }

            var result = await _questionsService.EditQuestionAsync(question);
            if (result is ServiceNotFoundResult<QuestionForDetails>)
            {
                return NotFound();
            }

            if (!result.Successful)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<ActionResult> PostQuestion(QuestionForCreating question)
        {
            var result = await _questionsService.CreateQuestionAsync(question);

            if (!result.Successful)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            var result = await _questionsService.DeleteQuestionAsync(id);
            if (result is ServiceNotFoundResult<QuestionForDetails>)
            {
                return NotFound();
            }

            if (!result.Successful)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}
