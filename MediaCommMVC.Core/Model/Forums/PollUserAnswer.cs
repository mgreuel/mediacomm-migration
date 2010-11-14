#region Using Directives

using MediaCommMVC.Core.Model.Users;

#endregion

namespace MediaCommMVC.Core.Model.Forums
{
    /// <summary>
    ///   The answer a user gave to a poll question.
    /// </summary>
    public class PollUserAnswer
    {
        #region Properties

        /// <summary>
        ///   Gets or sets the type.
        /// </summary>
        /// <value>The answer the user gave.</value>
        public virtual PollAnswer Answer { get; set; }

        /// <summary>
        ///   Gets or sets the id.
        /// </summary>
        /// <value>The poll user answer id.</value>
        public virtual int Id { get; set; }

        /// <summary>
        ///   Gets or sets the user.
        /// </summary>
        /// <value>The user who gave the answer.</value>
        public virtual MediaCommUser User { get; set; }

        /// <summary>
        /// Gets or sets the poll.
        /// </summary>
        /// <value>The poll the user answer belongs to.</value>
        public virtual Poll Poll { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///   Returns a <see cref = "System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///   A <see cref = "System.String" /> that contains the id, the answer and the user.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Id: '{0}', Answer: {1}, User: {2}", this.Id, this.Answer, this.User);
        }

        #endregion
    }
}