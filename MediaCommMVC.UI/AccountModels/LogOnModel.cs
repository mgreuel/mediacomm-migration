using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MediaCommMVC.UI.AccountModels
{
    /// <summary>The log on model.</summary>
    public class LogOnModel
    {
        #region Properties

        /// <summary>Gets or sets Password.</summary>
        /// <value>The password.</value>
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        /// <summary>Gets or sets a value indicating whether RememberMe.</summary>
        /// <value>The remember me.</value>
        [DisplayName("Remember me?")]
        public bool RememberMe { get; set; }

        /// <summary>Gets or sets UserName.</summary>
        /// <value>The user name.</value>
        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }

        #endregion
    }
}