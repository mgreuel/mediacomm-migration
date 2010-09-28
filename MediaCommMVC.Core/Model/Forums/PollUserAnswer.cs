using MediaCommMVC.Core.Model.Users;

namespace MediaCommMVC.Core.Model.Forums
{
    /// <summary>
    /// The answer a user gave to a poll question.
    /// </summary>
    public class PollUserAnswer
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The poll user answer id.</value>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user who gave the answer.</value>
        public virtual MediaCommUser User { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The answer the user gave.</value>
        public virtual PollAnswer Answer { get; set; }
    }
}