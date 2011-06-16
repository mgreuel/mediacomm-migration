using System.Collections.Generic;

using MediaCommMVC.Web.Core.Model.Forums;
using MediaCommMVC.Web.Core.Parameters;

namespace MediaCommMVC.Web.Core.ViewModel
{
    public class ForumPage
    {
        public Forum Forum { get; set; }

        public PagingParameters PagingParameters { get; set; }

        public int PostsPerTopicPage { get; set; }

        public IEnumerable<Topic> Topics { get; set; }
    }
}