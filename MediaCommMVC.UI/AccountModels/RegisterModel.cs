#region Using Directives

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion

namespace MediaCommMVC.UI.AccountModels
{
    /// <summary>
    ///   The register model.
    /// </summary>
    [PropertiesMustMatch("Password", "ConfirmPassword", 
        ErrorMessage = "The password and confirmation password do not match.")]
    public class RegisterModel
    {
        #region Properties

        /// <summary>
        ///   Gets or sets ConfirmPassword.
        /// </summary>
        /// <value>The confirm password.</value>
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        ///   Gets or sets Email.
        /// </summary>
        /// <value>The email.</value>
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email address")]
        public string Email { get; set; }

        /// <summary>
        ///   Gets or sets Password.
        /// </summary>
        /// <value>The password.</value>
        [Required]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        /// <summary>
        ///   Gets or sets UserName.
        /// </summary>
        /// <value>The user name.</value>
        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }

        #endregion
    }
}