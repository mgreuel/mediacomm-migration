﻿#region Using Directives

using System.Collections.Generic;
using System.Linq;

#endregion

namespace MediaCommMVC.Core.Model.Forums
{
    /// <summary>Represents a poll/survey.</summary>
    public class Poll
    {
        #region Constants and Fields

        /// <summary>The users answers with their count.</summary>
        private Dictionary<PollAnswer, int> answerCount;

        #endregion

        #region Properties

        /// <summary>Gets or sets the id.</summary>
        /// <value>The poll id.</value>
        public virtual int Id { get; set; }

        /// <summary>Gets or sets the possible answers.</summary>
        /// <value>The possible answers.</value>
        public virtual IEnumerable<PollAnswer> PossibleAnswers { get; set; }

        /// <summary>Gets or sets the title of the poll.</summary>
        /// <value>The title of the poll.</value>
        public virtual string Question { get; set; }

        /// <summary>Gets or sets the type of the poll.
        /// <example>
        ///     Single Answer or multiple answers.</example>
        /// </summary>
        /// <value>The poll type.</value>
        public virtual PollType Type { get; set; }

        /// <summary>Gets or sets the user answers.</summary>
        /// <value>The user answers.</value>
        public virtual IEnumerable<PollUserAnswer> UserAnswers { get; set; }

        /// <summary>Gets the user ansers and their count.</summary>
        /// <value>The count of the user answers.</value>
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

        #region Public Methods

        /// <summary>Returns a <see cref="System.String"/> that represents this instance.</summary>
        /// <returns>A <see cref="System.String"/> that contains the Id and the Question.</returns>
        public override string ToString()
        {
            return string.Format("Id: '{0}', Question: '{1}'", this.Id, this.Question);
        }

        #endregion
    }
}