namespace MediaCommMVC.Core.Model.Forums
{
    /// <summary>A possible answer to a poll question.</summary>
    public class PollAnswer
    {
        #region Properties

        /// <summary>Gets or sets the id.</summary>
        /// <value>The poll answer id.</value>
        public virtual int Id { get; set; }

        /// <summary>Gets or sets the poll.</summary>
        /// <value>The poll the answer belongs to.</value>
        public virtual Poll Poll { get; set; }

        /// <summary>Gets or sets the answer text.</summary>
        /// <value>The answer.</value>
        public virtual string Text { get; set; }

        #endregion

        #region Public Methods

        /// <summary>Returns a <see cref="System.String"/> that represents this instance.</summary>
        /// <returns>A <see cref="System.String"/> that contains the id, the poll and the answer text.</returns>
        public override string ToString()
        {
            return string.Format("Id: '{0}', Text: '{1}', Poll: {2}", this.Id, this.Text, this.Poll);
        }

        #endregion
    }
}