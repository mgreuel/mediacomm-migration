using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MediaCommMVC.UI.AccountModels
{
    /// <summary>The change password model.</summary>
    [PropertiesMustMatch("NewPassword", "ConfirmPassword",
        ErrorMessage = "The new password and confirmation password do not match.")]
    public class ChangePasswordModel
    {
        #region Properties

        /// <summary>Gets or sets ConfirmPassword.</summary>
        /// <value>The confirm password.</value>
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm new password")]
        public string ConfirmPassword { get; set; }

        /// <summary>Gets or sets NewPassword.</summary>
        /// <value>The new password.</value>
        [Required]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [DisplayName("New password")]
        public string NewPassword { get; set; }

        /// <summary>Gets or sets OldPassword.</summary>
        /// <value>The old password.</value>
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Current password")]
        public string OldPassword { get; set; }

        #endregion
    }
}