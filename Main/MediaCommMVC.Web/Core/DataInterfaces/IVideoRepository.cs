using System.Collections.Generic;
using System.Drawing;

using MediaCommMVC.Web.Core.Model.Videos;

namespace MediaCommMVC.Web.Core.DataInterfaces
{
    public interface IVideoRepository
    {
        void AddCategory(VideoCategory videoCategory);

        void AddVideo(Video video);

        IEnumerable<VideoCategory> GetAllCategories();

        VideoCategory GetCategoryById(int id);

        Image GetThumbnailImage(int videoId);

        IEnumerable<string> GetUnmappedPosterFiles();

        IEnumerable<string> GetUnmappedThumbnailFiles();

        IEnumerable<string> GetUnmappedVideoFiles();

        Video GetVideoById(int id);
    }
}