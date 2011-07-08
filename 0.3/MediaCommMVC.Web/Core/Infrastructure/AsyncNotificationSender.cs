using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.Data.Repositories;
using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Model;
using MediaCommMVC.Web.Core.Model.Forums;
using MediaCommMVC.Web.Core.Model.Photos;
using MediaCommMVC.Web.Core.Model.Users;
using MediaCommMVC.Web.Core.Model.Videos;

using NHibernate;

namespace MediaCommMVC.Web.Core.Infrastructure
{
    public class AsyncNotificationSender : INotificationSender
    {
        private readonly ILogger logger;

        private readonly MailConfiguration mailConfiguration;

        private readonly IUserRepository userRepository;

        private delegate void NewPostNotificationDelegate(Post newPost);

        private readonly NewPostNotificationDelegate newPostNotificationDelegate;

        private delegate void NewTopicNotificationDelegate(Topic newTopic);

        private readonly NewTopicNotificationDelegate newTopicNotificationDelegate;

        private delegate void PhotosNotificationDelegate(PhotoAlbum albumWithNewPhotos);

        private readonly PhotosNotificationDelegate photosNotificationDelegate;

        private delegate void VideosNotificationDelegate(Video newVideo);

        private readonly VideosNotificationDelegate videosNotificationDelegate;

        private readonly MemorySessionContainer sessionContainer;

        public AsyncNotificationSender(ILogger logger, MailConfiguration mailConfiguration)
        {
            this.logger = logger;
            this.mailConfiguration = mailConfiguration;
            this.sessionContainer = new MemorySessionContainer();
            this.userRepository = new UserRepository(this.sessionContainer);

            this.newPostNotificationDelegate = new NewPostNotificationDelegate(this.SendNewPostNotificationAsync);
            this.photosNotificationDelegate = new PhotosNotificationDelegate(this.SendPhotosNotificationAsync);
            this.videosNotificationDelegate = new VideosNotificationDelegate(this.SendVideosNotificationAsync);
            this.newTopicNotificationDelegate = new NewTopicNotificationDelegate(this.SendNewTopicNotificationAsync);
        }

        public void SendForumsNotification(Post newPost)
        {
            this.newPostNotificationDelegate.BeginInvoke(newPost, null, null);
        }

        public void SendForumsNotification(Topic newTopic)
        {
            this.newTopicNotificationDelegate.BeginInvoke(newTopic, null, null);
        }

        public void SendPhotosNotification(PhotoAlbum albumContainingNewPhotos)
        {
            this.photosNotificationDelegate.BeginInvoke(albumContainingNewPhotos, null, null);
        }

        public void SendVideosNotification(Video newVideo)
        {
            this.videosNotificationDelegate.BeginInvoke(newVideo, null, null);
        }

        private void SendNewPostNotificationAsync(Post newPost)
        {
            this.ExecuteNotification(delegate()
                {
                    IEnumerable<string> usersMailAddressesToNotify = this.userRepository.GetMailAddressesToNotifyAboutNewPost();

                    string subject = Resources.Mail.NewPostTitle + Resources.General.Title;
                    string body = string.Format(Resources.Mail.NewPostBody, newPost.Author.UserName, newPost.Created);
                    this.SendNotificationMail(subject, body, usersMailAddressesToNotify);

                    this.sessionContainer.EndSessionAndCommitTransaftion();
                });
        }

        private void ExecuteNotification(Action action)
        {
            this.sessionContainer.BeginSessionAndTransaction();

            try
            {
                action();
            }
            catch (Exception exception)
            {
                this.logger.Error("Unable to send notification", exception);

                this.sessionContainer.EndSessionAndRollbackTransaftion();
            }
        }

        private void SendNewTopicNotificationAsync(Topic newTopic)
        {
            IEnumerable<string> usersMailAddressesToNotify = this.userRepository.GetMailAddressesToNotifyAboutNewPost();

            string bcc = string.Join(";", usersMailAddressesToNotify);
        }

        private void SendPhotosNotificationAsync(PhotoAlbum albumContainingNewPhotos)
        {
            throw new NotImplementedException();
        }

        private void SendVideosNotificationAsync(Video newVideo)
        {
            throw new NotImplementedException();
        }

        private void SendNotificationMail(string subject, string body, IEnumerable<string> recipients)
        {
            this.logger.Info("Sending mail with subject '{0}' to '{1}'", subject, string.Join(";", recipients));
            
            var smtp = new SmtpClient
            {
                Host = this.mailConfiguration.SmtpHost,
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };

            using (MailMessage message = new MailMessage(this.mailConfiguration.MailFrom, string.Empty) { Subject = subject, Body = body })
            {
                recipients.ToList().ForEach(r => message.Bcc.Add(r));
                smtp.Send(message);
            }
        }
    }
}