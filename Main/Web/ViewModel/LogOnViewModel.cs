namespace MediaCommMVC.UI.ViewModels
{
    #region Using Directives

    using System.ComponentModel.DataAnnotations;

    using Resources;

    #endregion

    public class LogOnViewModel
    {
        #region Properties

        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 20, MinimumLength = 5)]
        [Display(Name = "Password", ResourceType = typeof(AccountResources))]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        [Display(Name = "Username", ResourceType = typeof(AccountResources))]
        public string UserName { get; set; }

        #endregion
    }
}