using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaCommMVC.Core.ViewModel
{
    public class ForumsIndexViewModel
    {
        public IEnumerable<ForumViewModel> Forums { get; set; }
    }
}