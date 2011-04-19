using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaCommMVC.Core.ViewModels.Pages
{
    public class ForumPageViewModel
    {
        public IEnumerable<TopicViewModel> Topics { get; set; }
    }
}