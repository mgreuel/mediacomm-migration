using System.Collections.Generic;

using MediaCommMVC.Web.Core.Model.Forums;
using MediaCommMVC.Web.Core.Model.Photos;

namespace MediaCommMVC.Web.Core.ViewModel
{
    public class WhatsNewInfo
    {
        public IEnumerable<PhotoAlbum> Albums { get; set; }

        public int PostsPerTopicPage { get; set; }

        public IEnumerable<Topic> Topics { get; set; }
    }
}