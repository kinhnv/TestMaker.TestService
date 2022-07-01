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

            if (result.Successful)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetQuestion(Guid id)
        {
            var result = await _questionsService.GetQuestionAsync(id);

            if (!result.Successful)
            {
                if (result is ServiceNotFoundResult<QuestionForDetails>)
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }

            return Ok(result.Data);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutQuestion(Guid id, QuestionForEditing question)
        //{
        //    if (id != question.QuestionId)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        var result = await _questionsService.EditQuestionAsync(question);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!await _questionsService.QuestionExistsAsync(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        [HttpPost]
        public async Task<ActionResult> PostQuestion(QuestionForCreating question)
        {
            return Ok(await _questionsService.CreateQuestionAsync(question));
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteQuestion(Guid id)
        //{
        //    if (!await _questionsService.QuestionExistsAsync(id))
        //    {
        //        return NotFound();
        //    }

        //    await _questionsService.DeleteQuestionAsync(id);

        //    return NoContent();
        //}
    }
}
