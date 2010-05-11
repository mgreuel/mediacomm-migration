using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;

namespace MediaCommMVC.WebTests.Pages.Admin
{
    public class CreateUserPage : LoginPage
    {
        [FindBy(Id = "userName")]
        public TextField UserName;

        [FindBy(Id = "password")]
        public TextField Password;

        [FindBy(Id = "mailAddress")]
        public TextField MailAddress;

        [FindBy(Id = "submitUser")]
        public Button SubmitUserButton;
    }
}
