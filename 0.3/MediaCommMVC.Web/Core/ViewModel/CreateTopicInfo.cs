using System.Collections.Generic;

using MediaCommMVC.Web.Core.Model.Forums;

namespace MediaCommMVC.Web.Core.ViewModel
{
    public class CreateTopicInfo
    {
        public Forum Forum { get; set; }

        public IEnumerable<string> UserNames { get; set; }
    }
}