using System.Collections.Generic;
using System.Linq;

namespace MediaCommMVC.Web.Core.Model.Forums
{
    public class Poll
    {
        private Dictionary<PollAnswer, int> answerCount;

        public virtual int Id { get; set; }

        public virtual IEnumerable<PollAnswer> PossibleAnswers { get; set; }

        public virtual string Question { get; set; }

        public virtual PollType Type { get; set; }

        public virtual IEnumerable<PollUserAnswer> UserAnswers { get; set; }

        public virtual IDictionary<PollAnswer, int> UserAnswersWithCount
        {
            get
            {
                if (this.answerCount == null)
                {
                    this.answerCount = this.UserAnswers.GroupBy(ua => ua.Answer).ToDictionary(g => g.Key, g => g.Count());

                    foreach (PollAnswer possibleAnswer in this.PossibleAnswers)
                    {
                        if (!this.answerCount.ContainsKey(possibleAnswer))
                        {
                            this.answerCount.Add(possibleAnswer, 0);
                        }
                    }

                    this.answerCount = this.answerCount.OrderByDescending(a => a.Value).ToDictionary(a => a.Key, a => a.Value);
                }

                return this.answerCount;
            }
        }

        public override string ToString()
        {
            return string.Format("Id: '{0}', Question: '{1}'", this.Id, this.Question);
        }
    }
}