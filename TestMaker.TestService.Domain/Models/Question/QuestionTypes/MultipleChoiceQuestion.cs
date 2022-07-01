using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TestMaker.TestService.Domain.Extensions;

namespace TestMaker.TestService.Domain.Models.Question.QuestionTypes
{
    class MultipleChoiceQuestionContentResult
    {
        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("isSingleChoice")]
        public bool IsSingleChoice { get; set; }

        [JsonProperty("answers")]
        public List<string> Answers { get; set; }
    }

    public class MultipleChoiceQuestionContentAnswer
    {
        [JsonProperty("answer")]
        public string Answer { get; set; }

        [JsonProperty("isCorrect")]
        public bool IsCorrect { get; set; }
    }

    public class MultipleChoiceQuestionContent
    {
        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("answers")]
        public IEnumerable<MultipleChoiceQuestionContentAnswer> Answers { get; set; }
    }

    public class MultipleChoiceQuestion: QuestionBase
    {
        [JsonProperty("questionId")]
        public Guid QuestionId { get; set; }

        [JsonIgnore]
        public MultipleChoiceQuestionContent Content
        {
            get
            {
                if (string.IsNullOrEmpty(ContentAsJson))
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<MultipleChoiceQuestionContent>(ContentAsJson);
            }
            set
            {
                if (value != null)
                {
                    ContentAsJson = JsonConvert.SerializeObject(value);
                }
                else
                {
                    ContentAsJson = string.Empty;
                }
            }
        }

        [JsonProperty("answerAsJson")]
        public string AnswerAsJson
        {
            get
            {
                return JsonConvert.SerializeObject(Content.Answers.Where(a => a.IsCorrect).Select(a => a.Answer));
            }
        }

        [JsonProperty("questionAsJson")]
        public string QuestionAsJson
        {
            get
            {
                return JsonConvert.SerializeObject(new MultipleChoiceQuestionContentResult
                {
                    Question = Content.Question,
                    IsSingleChoice = Content.Answers.Count(x => x.IsCorrect) == 1,
                    Answers = Content.Answers.RandomPosition().Select(a => a.Answer).ToList()
                });
            }
        }
    }
}
