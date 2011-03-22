using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaCommMVC.UI.Core.Services
{
    using MediaCommMVC.UI.Core.ViewModel;

    public interface IAccountService
    {
        bool LoginDataIsValid(LogOnViewModel logOnViewModel);
    }
}
