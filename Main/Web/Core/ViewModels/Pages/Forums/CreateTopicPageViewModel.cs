using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaCommMVC.Core.ViewModel
{
    public class CreateTopicViewModel
    {
        public IEnumerable<string> UserNames { get; set; }

        public string TopicSubject { get; set; }

        public string PostText { get; set; }
    }
}