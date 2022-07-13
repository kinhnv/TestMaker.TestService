using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TestMaker.Common.Extensions;

namespace TestMaker.TestService.Domain.Models.Question.QuestionTypes
{
    class BlankFillingQuestionContentBlankResult
    {
        [JsonProperty("answers")]
        public IEnumerable<string> Answers { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }
    }

    class BlankFillingQuestionContentResult
    {
        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("blanks")]
        public List<BlankFillingQuestionContentBlankResult> Blanks { get; set; }
    }

    public class BlankFillingQuestionContentBlank
    {
        [JsonProperty("answer")]
        public string Answer { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }
    }

    public class BlankFillingQuestionContent
    {
        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("isFromAPrivateCollection")]
        public bool IsFromAPrivateCollection { get; set; }

        [JsonProperty("blanks")]
        public IEnumerable<BlankFillingQuestionContentBlank> Blanks { get; set; }
    }

    public class BlankFillingQuestion : QuestionBase
    {
        [JsonProperty("questionId")]
        public Guid QuestionId { get; set; }

        [JsonIgnore]
        public BlankFillingQuestionContent Content
        {
            get
            {
                if (string.IsNullOrEmpty(ContentAsJson))
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<BlankFillingQuestionContent>(ContentAsJson);
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
                var content = Content;

                var contentBlanks = new List<BlankFillingQuestionContentBlank>();
                var blanks = new Regex("blank_[0->9]*\\(").Matches(content.Question);

                foreach (Capture blank in blanks)
                {
                    var endIndex = blank.Index + blank.Length - 1;
                    var correctAnswer = string.Empty;
                    while (content.Question[endIndex] != ')')
                    {
                        if (content.Question[endIndex] != '(' && content.Question[endIndex] != ')')
                        {
                            correctAnswer += content.Question[endIndex];
                        }
                        endIndex++;
                    }

                    contentBlanks.Add(new BlankFillingQuestionContentBlank
                    {
                        Position = blank.Value.Substring(0, blank.Length - 1),
                        Answer = correctAnswer
                    });
                }

                return JsonConvert.SerializeObject(contentBlanks.OrderBy(x => x.Position));
            }
        }

        [JsonProperty("questionAsJson")]
        public string QuestionAsJson
        {
            get
            {
                var contentBlanks = new List<BlankFillingQuestionContentBlankResult>();
                var correctAnswers = new List<string>();
                var startIndexs = new List<int>();
                var endIndexs = new List<int>();

                var content = Content;

                var blanks = new Regex("blank_[0->9]*\\(").Matches(content.Question);

                foreach (Capture blank in blanks)
                {
                    var endIndex = blank.Index + blank.Length - 1;
                    var correctAnswer = string.Empty;
                    while (content.Question[endIndex] != ')')
                    {
                        if (content.Question[endIndex] != '(' && content.Question[endIndex] != ')')
                        {
                            correctAnswer += content.Question[endIndex];
                        }
                        endIndex++;
                    }

                    startIndexs.Add(blank.Index + blank.Length - 1);
                    endIndexs.Add(endIndex);
                    correctAnswers.Add(correctAnswer);
                }
                startIndexs.Reverse();
                endIndexs.Reverse();

                for (int i = 0; i < startIndexs.Count; i++)
                {
                    content.Question = content.Question.Remove(startIndex: startIndexs[i], endIndexs[i] - startIndexs[i] + 1);
                }

                if (content.IsFromAPrivateCollection)
                {
                    contentBlanks = Content.Blanks.Select(a => new BlankFillingQuestionContentBlankResult
                    {
                        Position = a.Position,
                        Answers = a.Answer.Split(",").OrderRandom()
                    }).ToList();
                }
                else
                {
                    foreach (Capture blank in blanks)
                    {
                        contentBlanks.Add(new BlankFillingQuestionContentBlankResult
                        {
                            Position = blank.Value.Substring(0, blank.Length - 1),
                            Answers = correctAnswers.OrderRandom()
                        });
                    }
                }

                return JsonConvert.SerializeObject(new BlankFillingQuestionContentResult
                {
                    Question = content.Question,
                    Blanks = contentBlanks
                });
            }
        }
    }
}
