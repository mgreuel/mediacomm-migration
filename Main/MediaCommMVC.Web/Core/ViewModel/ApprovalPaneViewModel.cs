using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaCommMVC.Web.Core.ViewModel
{
    public class ApprovalPaneViewModel
    {
        public ApprovalPaneViewModel()
        {
            this.ShowButton = true;
        }

        public string Url { get; set; }

        public bool ShowButton { get; set; }
    }
}