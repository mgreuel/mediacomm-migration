using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

using WatiN.Core;

namespace MediaCommMVC.WebTests.Pages
{
    public class LoginPage : Page
    {
        [FindBy(Name = "UserName")]
        public TextField UserName;

        [FindBy(Name = "Password")]
        public TextField Password;

        [FindBy(Name = "RememberMe")]
        public CheckBox RememberMe;

        [FindBy(Id = "loginButton")]
        public Button SubmitButton;

        public void LoginUsingDefaultUser()
        {
            this.Login(
                ConfigurationManager.AppSettings["defaultUser"],
                ConfigurationManager.AppSettings["defaultPassword"]);
        }

        public void Login(string username, string password)
        {
            this.UserName.TypeText(username);
            this.Password.TypeText(password);
            this.RememberMe.Checked = true;
            this.SubmitButton.Click();
        }
    }
}
