#region Using Directives

using System;

#endregion

namespace MediaCommMVC.Core.Model.Users
{
    /// <summary>Represent an user.</summary>
    [Serializable]
    public class MediaCommUser
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="MediaCommUser"/> class.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="password">The password.</param>
        public MediaCommUser(string userName, string emailAddress, string password)
        {
            this.UserName = userName;
            this.EMailAddress = emailAddress;
            this.Password = password;
        }

        /// <summary>Initializes a new instance of the <see cref="MediaCommUser"/> class.
        /// The empty constructor is needed by the ORM.</summary>
        protected MediaCommUser()
        {
        }

        #endregion

        #region Properties

        /// <summary>Gets or sets the city.</summary>
        /// <value>The user's city.</value>
        public virtual string City { get; set; }

        /// <summary>Gets or sets the user's date of birth.</summary>
        /// <value>The user's date of birth.</value>
        public virtual DateTime? DateOfBirth { get; set; }

        /// <summary>Gets or sets the user's E-Mail address.
        ///   The E-Mail address should only be set within the constructor or by the ORM.</summary>
        /// <value>The user's E-Mail address.</value>
        public virtual string EMailAddress { get; protected set; }

        /// <summary>Gets or sets the user's first name.</summary>
        /// <value>The user's first name.</value>
        public virtual string FirstName { get; set; }

        /// <summary>Gets or sets the user's icq uin.</summary>
        /// <value>The user's icq uin.</value>
        public virtual string IcqUin { get; set; }

        /// <summary>Gets or sets the ID.</summary>
        /// <value>The user's ID.</value>
        public virtual int Id { get; protected set; }

        /// <summary>Gets or sets a value indicating whether this user is admin.</summary>
        /// <value><c>true</c> if this user is admin; otherwise, <c>false</c>.</value>
        public virtual bool IsAdmin { get; set; }

        /// <summary>Gets or sets the user's last name.</summary>
        /// <value>The user's last name.</value>
        public virtual string LastName { get; set; }

        /// <summary>Gets or sets the user's last visit.</summary>
        /// <value>The user's last visit.</value>
        public virtual DateTime? LastVisit { get; set; }

        /// <summary>Gets or sets the user's mobile phone number.</summary>
        /// <value>The user's mobile phone number.</value>
        public virtual string MobilePhoneNumber { get; set; }

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        public virtual string Password { get; set; }

        /// <summary>Gets or sets the user's phone number.</summary>
        /// <value>The user's phone number.</value>
        public virtual string PhoneNumber { get; set; }

        /// <summary>Gets or sets the user's skype nick.</summary>
        /// <value>The user's skype nick.</value>
        public virtual string SkypeNick { get; set; }

        /// <summary>Gets or sets the street.</summary>
        /// <value>The user's street.</value>
        public virtual string Street { get; set; }

        /// <summary>Gets or sets the name of the user.
        ///   The name should only be set within the constructor or by the ORM.</summary>
        /// <value>The name of the user.</value>
        public virtual string UserName { get; protected set; }

        /// <summary>Gets or sets the zip code.</summary>
        /// <value>The user's zip code.</value>
        public virtual string ZipCode { get; set; }

        #endregion

        #region Public Methods

        /// <summary>Determines whether the specified <see cref="System.Object"/> is equal to this instance.</summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            MediaCommUser user = obj as MediaCommUser;

            return user != null && user.UserName.Equals(this.UserName, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>Returns a <see cref="System.String"/> that represents this instance.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("Username: '{0}', Id: '{1}", this.UserName, this.Id);
        }

        #endregion
    }
}
