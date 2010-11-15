#region Using Directives

using System;

using MediaCommMVC.Core.Model.Users;

#endregion

namespace MediaCommMVC.Core.Model.Forums
{
    /// <summary>Saves the date an user has last visited a topic.</summary>
    public class TopicRead
    {
        #region Properties

        /// <summary>Gets or sets the id.
        ///   Only the ORM should set the id.</summary>
        /// <value>The automatic id.</value>
        public virtual int Id { get; protected set; }

        /// <summary>Gets or sets the last visit date.</summary>
        /// <value>The last visit date.</value>
        public virtual DateTime LastVisit { get; set; }

        /// <summary>Gets or sets the user the topic was read by.</summary>
        /// <value>The user the topic was read by.</value>
        public virtual MediaCommUser ReadByUser { get; set; }

        /// <summary>Gets or sets the read topic.</summary>
        /// <value>The read topic.</value>
        public virtual Topic ReadTopic { get; set; }

        #endregion
    }
}
