using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TestMaker.TestService.Domain.Extensions;

namespace TestMaker.TestService.Domain.Models.Question.QuestionTypes
{
    class MatchingQuestionContentResult
    {
        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("answers")]
        public List<MatchingQuestionContentAnswer> Answers { get; set; }
    }

    public class MatchingQuestionContentAnswer
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("target")]
        public string Target { get; set; }
    }

    public class MatchingQuestionContent
    {
        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("answers")]
        public IEnumerable<MatchingQuestionContentAnswer> Answers { get; set; }
    }

    public class MatchingQuestion : QuestionBase
    {
        [JsonProperty("questionId")]
        public Guid QuestionId { get; set; }

        [JsonIgnore]
        public MatchingQuestionContent Content
        {
            get
            {
                if (string.IsNullOrEmpty(ContentAsJson))
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<MatchingQuestionContent>(ContentAsJson);
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
                return JsonConvert.SerializeObject(Content.Answers.OrderBy(x => x.From));
            }
        }

        [JsonProperty("questionAsJson")]
        public string QuestionAsJson
        {
            get
            {
                var content = Content;

                var froms = content.Answers.Select(x => x.From).RandomPosition();
                var targets = content.Answers.Select(x => x.Target).RandomPosition().ToList();

                return JsonConvert.SerializeObject(new MatchingQuestionContentResult
                {
                    Question = content.Question,
                    Answers = froms.Select((from, index) => new MatchingQuestionContentAnswer
                    {
                        From = from,
                        Target = targets[index]
                    }).ToList()
                });
            }
        }
    }
}
