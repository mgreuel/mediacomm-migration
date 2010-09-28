namespace MediaCommMVC.Core.Model.Forums
{
    /// <summary>
    /// A possible answer to a poll question.
    /// </summary>
    public class PollAnswer
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The poll answer id.</value>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the poll.
        /// </summary>
        /// <value>The poll the answer belongs to.</value>
        public virtual Poll Poll { get; set; }

        /// <summary>
        /// Gets or sets the answer text.
        /// </summary>
        /// <value>The answer.</value>
        public virtual string Text { get; set; }
    }
}