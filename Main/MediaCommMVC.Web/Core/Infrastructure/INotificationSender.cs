using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MediaCommMVC.Web.Core.Model.Forums;
using MediaCommMVC.Web.Core.Model.Photos;
using MediaCommMVC.Web.Core.Model.Videos;

namespace MediaCommMVC.Web.Core.Infrastructure
{
    public interface INotificationSender
    {
        void SendForumsNotification(Post newPost);

        void SendPhotosNotification(PhotoAlbum albumContainingNewPhotos, string uploaderName);

        void SendVideosNotification(Video newVideo);

        void SendForumsNotification(Topic newTopic);
    }
}