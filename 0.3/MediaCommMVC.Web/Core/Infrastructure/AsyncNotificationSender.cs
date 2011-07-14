using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.Data.Repositories;
using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Model.Forums;
using MediaCommMVC.Web.Core.Model.Photos;
using MediaCommMVC.Web.Core.Model.Videos;

using Resources;

namespace MediaCommMVC.Web.Core.Infrastructure
{
    public class AsyncNotificationSender : INotificationSender
    {
        private readonly ILogger logger;

        private readonly MailConfiguration mailConfiguration;

        private readonly NewPostNotificationDelegate newPostNotificationDelegate;

        private readonly NewTopicNotificationDelegate newTopicNotificationDelegate;

        private readonly PhotosNotificationDelegate photosNotificationDelegate;

        private readonly MemorySessionContainer sessionContainer;

        private readonly IUserRepository userRepository;

        private readonly VideosNotificationDelegate videosNotificationDelegate;

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

        private delegate void NewPostNotificationDelegate(Post newPost);

        private delegate void NewTopicNotificationDelegate(Topic newTopic);

        private delegate void PhotosNotificationDelegate(PhotoAlbum albumWithNewPhotos, string uploaderName);

        private delegate void VideosNotificationDelegate(Video newVideo);

        public void SendForumsNotification(Post newPost)
        {
            this.newPostNotificationDelegate.BeginInvoke(newPost, null, null);
        }

        public void SendForumsNotification(Topic newTopic)
        {
            this.newTopicNotificationDelegate.BeginInvoke(newTopic, null, null);
        }

        public void SendPhotosNotification(PhotoAlbum albumContainingNewPhotos, string uploaderName)
        {
            this.photosNotificationDelegate.BeginInvoke(albumContainingNewPhotos, uploaderName, null, null);
        }

        public void SendVideosNotification(Video newVideo)
        {
            this.videosNotificationDelegate.BeginInvoke(newVideo, null, null);
        }

        private void ExecuteNotification(Action action)
        {
            try
            {
                this.sessionContainer.BeginSessionAndTransaction();

                action();

                this.sessionContainer.EndSessionAndCommitTransaftion();
            }
            catch (Exception exception)
            {
                this.logger.Error("Unable to send notification", exception);

                this.sessionContainer.EndSessionAndRollbackTransaftion();
            }
        }

        private void SendNewPostNotificationAsync(Post newPost)
        {
            this.ExecuteNotification(
                () =>
                {
                    Topic notifyTopic = this.sessionContainer.CurrentSession.Get<Topic>(newPost.Topic.Id);

                    DateTime notificationTime = DateTime.Now;
                    IEnumerable<string> usersMailAddressesToNotify =
                        this.userRepository.GetMailAddressesToNotifyAboutNewPost().Where(
                            m => !notifyTopic.ExcludedUsers.Select(u => u.EMailAddress).Contains(m)).ToList();

                    if (usersMailAddressesToNotify.Count() == 0)
                    {
                        return;
                    }

                    string subject = Mail.NewPostTitle + General.Title;
                    string body = string.Format(Mail.NewPostBody, newPost.Author.UserName, newPost.Topic.Title, newPost.Created) + "<br /><br />" +
                                  General.WebSite;
                    this.SendNotificationMail(subject, body, usersMailAddressesToNotify);

                    this.userRepository.UpdateLastForumsNotification(usersMailAddressesToNotify, notificationTime);
                });
        }

        private void SendNewTopicNotificationAsync(Topic newTopic)
        {
            this.ExecuteNotification(
                () =>
                {
                    Topic notifyTopic = this.sessionContainer.CurrentSession.Get<Topic>(newTopic.Id);

                    DateTime notificationTime = DateTime.Now;
                    IEnumerable<string> usersMailAddressesToNotify = this.userRepository.GetMailAddressesToNotifyAboutNewPost().Where(
                            m => !notifyTopic.ExcludedUsers.Select(u => u.EMailAddress).Contains(m)).ToList();

                    if (usersMailAddressesToNotify.Count() == 0)
                    {
                        return;
                    }

                    string subject = Mail.NewTopicTitle + General.Title;
                    string body = string.Format(Mail.NewTopicBody, newTopic.CreatedBy, newTopic.Title, newTopic.Created) + "<br /><br />" +
                                  General.WebSite;
                    this.SendNotificationMail(subject, body, usersMailAddressesToNotify);

                    this.userRepository.UpdateLastForumsNotification(usersMailAddressesToNotify, notificationTime);
                });
        }

        private void SendNotificationMail(string subject, string body, IEnumerable<string> recipients)
        {
            this.logger.Info("Sending mail with subject '{0}' to '{1}'", subject, string.Join(";", recipients));

            var smtp = new SmtpClient { Host = this.mailConfiguration.SmtpHost, DeliveryMethod = SmtpDeliveryMethod.Network, };

            using (MailMessage message = new MailMessage(this.mailConfiguration.MailFrom, recipients.First()) { Subject = subject, Body = body })
            {
                message.IsBodyHtml = true;
                recipients.ToList().ForEach(r => message.Bcc.Add(r));
                smtp.Send(message);
            }
        }

        private void SendPhotosNotificationAsync(PhotoAlbum albumContainingNewPhoto, string uploaderName)
        {
            this.ExecuteNotification(
                () =>
                {
                    DateTime notificationTime = DateTime.Now;
                    IEnumerable<string> usersMailAddressesToNotify = this.userRepository.GetMailAddressesToNotifyAboutNewPhotos();

                    if (usersMailAddressesToNotify.Count() == 0)
                    {
                        return;
                    }

                    string subject = Mail.NewPhotosTitle + General.Title;
                    string body = string.Format(Mail.NewPhotosBody, uploaderName, albumContainingNewPhoto.Name) + "<br /><br />" +
                                  General.WebSite;
                    this.SendNotificationMail(subject, body, usersMailAddressesToNotify);

                    this.userRepository.UpdateLastPhotosNotification(usersMailAddressesToNotify, notificationTime);
                });
        }

        private void SendVideosNotificationAsync(Video newVideo)
        {
            this.ExecuteNotification(
                () =>
                {
                    DateTime notificationTime = DateTime.Now;
                    IEnumerable<string> usersMailAddressesToNotify = this.userRepository.GetMailAddressesToNotifyAboutNewVideos();

                    if (usersMailAddressesToNotify.Count() == 0)
                    {
                        return;
                    }

                    string subject = Mail.NewVideoTitle + General.Title;
                    string body = string.Format(Mail.NewVideoBody, newVideo.Uploader.UserName, newVideo.Title) + "<br /><br />" +
                                  General.WebSite;
                    this.SendNotificationMail(subject, body, usersMailAddressesToNotify);

                    this.userRepository.UpdateLastVideosNotification(usersMailAddressesToNotify, notificationTime);
                });
        }
    }
}