namespace MediaCommMVC.Core.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class LogOnViewModel
    {
        [Required(ErrorMessage =  RE]
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
