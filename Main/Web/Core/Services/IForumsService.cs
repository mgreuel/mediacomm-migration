using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaCommMVC.Core.Services
{
    using MediaCommMVC.Core.ViewModels.Pages.Forums;

    public interface IForumsService
    {
        ForumPageViewModel GetForumPage(int id, int page);
    }
}