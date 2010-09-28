using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaCommMVC.Core.Model.Forums
{
    /// <summary>
    /// Represents a poll/survey.
    /// </summary>
    public class Poll
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The poll id.</value>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the poll.
        /// </summary>
        /// <value>The title of the poll.</value>
        public virtual string Question { get; set; }

        /// <summary>
        /// Gets or sets the type of the poll.
        /// <example>Single Answer or multiple answers.</example>
        /// </summary>
        /// <value>The poll type.</value>
        public virtual PollType Type { get; set; }

        /// <summary>
        /// Gets or sets the possible answers.
        /// </summary>
        /// <value>The possible answers.</value>
        public virtual IEnumerable<PollAnswer> PossibleAnswers { get; set; }

        /// <summary>
        /// Gets or sets the user answers.
        /// </summary>
        /// <value>The user answers.</value>
        public virtual IEnumerable<PollUserAnswer> UserAnswers { get; set; }

        /// <summary>
        /// Gets or sets the topic the poll belongs to.
        /// </summary>
        /// <value>The forum topic.</value>
        public virtual Topic Topic { get; set; }
    }
}
