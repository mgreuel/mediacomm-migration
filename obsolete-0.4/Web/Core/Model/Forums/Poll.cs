namespace MediaCommMVC.Core.Model.Forums
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public class Poll
    {
        #region Constants and Fields

        private Dictionary<PollAnswer, int> answerCount;

        #endregion

        #region Properties

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
                    this.answerCount = this.UserAnswers.GroupBy(ua => ua.Answer).ToDictionary(
                        g => g.Key, g => g.Count());

                    foreach (PollAnswer possibleAnswer in this.PossibleAnswers)
                    {
                        if (!this.answerCount.ContainsKey(possibleAnswer))
                        {
                            this.answerCount.Add(possibleAnswer, 0);
                        }
                    }
                }

                return this.answerCount;
            }
        }

        #endregion
    }
}