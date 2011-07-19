using System.Collections.Generic;

using MediaCommMVC.Web.Core.Model.Forums;
using MediaCommMVC.Web.Core.Parameters;

namespace MediaCommMVC.Web.Core.ViewModel
{
    public class TopicPage
    {
        public PagingParameters PagingParameters { get; set; }

        public IEnumerable<Post> Posts { get; set; }

        public Topic Topic { get; set; }
    }
}