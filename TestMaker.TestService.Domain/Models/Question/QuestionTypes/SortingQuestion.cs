using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TestMaker.Common.Extensions;

namespace TestMaker.TestService.Domain.Models.Question.QuestionTypes
{
    class SortingQuestionContentResult
    {
        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("answers")]
        public List<string> Answers { get; set; }
    }

    public class SortingQuestionContentAnswer
    {
        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("answer")]
        public string Answer { get; set; }
    }

    public class SortingQuestionContent
    {
        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("answers")]
        public IEnumerable<SortingQuestionContentAnswer> Answers { get; set; }
    }

    public class SortingQuestion : QuestionBase
    {
        [JsonProperty("questionId")]
        public Guid QuestionId { get; set; }

        [JsonIgnore]
        public SortingQuestionContent Content
        {
            get
            {
                if (string.IsNullOrEmpty(ContentAsJson))
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<SortingQuestionContent>(ContentAsJson);
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
                return JsonConvert.SerializeObject(Content.Answers.OrderBy(x => x.Position).Select(x => x.Answer).OrderBy(x => x));
            }
        }

        [JsonProperty("questionAsJson")]
        public string QuestionAsJson
        {
            get
            {
                return JsonConvert.SerializeObject(new SortingQuestionContentResult
                {
                    Question = Content.Question,
                    Answers = Content.Answers.OrderRandom().Select(x => x.Answer).ToList()
                }); ;
            }
        }
    }
}
