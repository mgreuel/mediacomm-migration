using System.Collections.Generic;
using System.Drawing;

using MediaCommMVC.Web.Core.Model.Videos;

namespace MediaCommMVC.Web.Core.DataInterfaces
{
    public interface IVideoRepository
    {
        VideoCategory GetCategoryById(int id);

        IEnumerable<VideoCategory> GetAllCategories();

        IEnumerable<string> GetUnmappedThumbnailFiles();

        IEnumerable<string> GetUnmappedVideoFiles();

        void AddCategory(VideoCategory videoCategory);

        void AddVideo(Video video);

        Image GetThumbnailImage(int videoId);

        Video GetVideoById(int id);

        IEnumerable<string> GetUnmappedPosterFiles();
    }
}
