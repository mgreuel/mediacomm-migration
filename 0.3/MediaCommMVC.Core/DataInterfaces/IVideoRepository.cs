using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MediaCommMVC.Core.Model.Videos;

namespace MediaCommMVC.Core.DataInterfaces
{
    public interface IVideoRepository
    {
        VideoCategory GetCategoryById(int id);

        IEnumerable<VideoCategory> GetAllCategories();
    }
}
