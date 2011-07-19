namespace MediaCommMVC.Core.Model
{
    #region Using Directives

    using System;

    #endregion

    public class MediaCommUser
    {
        #region Properties

        public virtual string City { get; set; }

        public virtual DateTime? DateOfBirth { get; set; }

        public virtual string EMailAddress { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string IcqUin { get; set; }

        public virtual int Id { get; protected set; }

        public virtual bool IsAdmin { get; set; }

        public virtual string LastName { get; set; }

        public virtual DateTime? LastVisit { get; set; }

        public virtual string MobilePhoneNumber { get; set; }

        public virtual string Password { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual string SkypeNick { get; set; }

        public virtual string Street { get; set; }

        public virtual string UserName { get; protected set; }

        public virtual string ZipCode { get; set; }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return string.Format("Username: '{0}', Id: '{1}", this.UserName, this.Id);
        }

        #endregion
    }
}