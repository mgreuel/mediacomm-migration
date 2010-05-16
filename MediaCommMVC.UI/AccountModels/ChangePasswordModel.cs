#region Using Directives

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion

namespace MediaCommMVC.UI.AccountModels
{
    /// <summary>The change password model.</summary>
    [PropertiesMustMatch(
        "NewPassword", 
        "ConfirmPassword", 
        ErrorMessageResourceType = typeof(Resources.Users), 
        ErrorMessageResourceName = "PasswordsMustMatchError")]
    public class ChangePasswordModel
    {
        #region Properties

        /// <summary>
        ///   Gets or sets ConfirmPassword.
        /// </summary>
        /// <value>The confirm password.</value>
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Current Password")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        ///   Gets or sets NewPassword.
        /// </summary>
        /// <value>The new password.</value>
        [Required]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [DisplayName("New Password")]
        public string NewPassword { get; set; }

        /// <summary>
        ///   Gets or sets OldPassword.
        /// </summary>
        /// <value>The old password.</value>
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Old Password")]
        public string OldPassword { get; set; }

        #endregion
    }
}