using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MarkdownSharp;

using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Helpers;
using MediaCommMVC.Web.Core.Infrastructure;
using MediaCommMVC.Web.Core.Model.Forums;
using MediaCommMVC.Web.Core.Model.Users;
using MediaCommMVC.Web.Core.Parameters;
using MediaCommMVC.Web.Core.ViewModel;

namespace MediaCommMVC.Web.Core.Controllers
{
    [Authorize]
    public sealed class ForumsController : Controller
    {
        private const int PostsPerTopicPage = 25;

        private const int TopicsPerForumPage = 25;

        private readonly IForumRepository forumRepository;

        private readonly IUserRepository userRepository;

        private readonly CurrentUserContainer currentUserContainer;

        private readonly INotificationSender notificationSender;

        private readonly Markdown markdownConverter;

        public ForumsController(IForumRepository forumRepository, IUserRepository userRepository, CurrentUserContainer currentUserContainer, INotificationSender notificationSender, Markdown markdownConverter)
        {
            this.forumRepository = forumRepository;
            this.userRepository = userRepository;
            this.currentUserContainer = currentUserContainer;
            this.notificationSender = notificationSender;
            this.markdownConverter = markdownConverter;
        }

        [HttpPost]
        [NHibernateActionFilter]
        public RedirectResult AnswerPoll(IEnumerable<int> answerIds)
        {
            foreach (int id in answerIds)
            {
                PollAnswer answer = this.forumRepository.GetPollAnswerById(id);
                Poll poll = answer.Poll;
                PollUserAnswer userAnswer = new PollUserAnswer { User = this.currentUserContainer.User, Answer = answer, Poll = poll };

                this.forumRepository.SavePollUserAnswer(userAnswer);
            }

            return this.Redirect(this.Request.UrlReferrer.ToString());
        }

        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult CreateTopic(int id)
        {
            Forum forum = this.forumRepository.GetForumById(id);
            IEnumerable<string> userNames = this.userRepository.GetAllUsers().Select(u => u.UserName);
            this.ViewData["PollTypes"] = PollType.SingleAnswer.ToSelectList();

            return this.View(new CreateTopicInfo { Forum = forum, UserNames = userNames });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        [NHibernateActionFilter]
        public ActionResult CreateTopic(Topic topic, Post post, int id, bool sticky, string excludedUsers, Poll poll)
        {
            post.Author = this.userRepository.GetUserByName(this.User.Identity.Name);
            topic.Forum = this.forumRepository.GetForumById(id);
            List<MediaCommUser> usersToExclude = new List<MediaCommUser>();

            if (sticky)
            {
                topic.DisplayPriority = TopicDisplayPriority.Sticky;
            }

            List<string> userNamesToExclude = excludedUsers.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();

            foreach (string userName in userNamesToExclude)
            {
                MediaCommUser user = this.userRepository.GetUserByName(userName);
                usersToExclude.Add(user);
            }

            topic.ExcludedUsers = usersToExclude;

            if (poll != null && !string.IsNullOrEmpty(poll.Question))
            {
                topic.Poll = poll;
            }

            post.Text = HtmlSanitizer.Sanitize(this.markdownConverter.Transform(post.Text));

            Topic createdTopic = this.forumRepository.AddTopic(topic, post);

            this.notificationSender.SendForumsNotification(createdTopic);

            return this.RedirectToAction("Topic", new { id = createdTopic.Id, name = this.Url.ToFriendlyUrl(createdTopic.Title) });
        }

        [HttpPost]
        [NHibernateActionFilter]
        public ActionResult DeletePost(int id)
        {
            Post postToDelete = this.forumRepository.GetPostById(id);
            this.forumRepository.DeletePost(postToDelete);

            if (this.forumRepository.GetTopicById(postToDelete.Topic.Id) != null)
            {
                return this.RedirectToAction("Topic", new { id = postToDelete.Topic.Id, name = this.Url.ToFriendlyUrl(postToDelete.Topic.Title) });
            }
            else
            {
                return this.RedirectToAction(
                    "Forum", new { id = postToDelete.Topic.Forum.Id, name = this.Url.ToFriendlyUrl(postToDelete.Topic.Forum.Title) });
            }
        }

        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult EditPost(int id)
        {
            Post post = this.forumRepository.GetPostById(id);

            if (post.Author != this.currentUserContainer.User && !this.currentUserContainer.User.IsAdmin)
            {
                throw new UnauthorizedAccessException("Only Administrator can edit posts made by other users");
            }

            return this.View(post);
        }

        [HttpPost]
        [ValidateInput(false)]
        [NHibernateActionFilter]
        public ActionResult EditPost(int id, Post post)
        {
            Post postToUpdate = this.forumRepository.GetPostById(id);
            postToUpdate.Text = post.Text;

            if (postToUpdate.Author != this.currentUserContainer.User && !this.currentUserContainer.User.IsAdmin)
            {
                throw new UnauthorizedAccessException("Only Administrator can edit posts made by other users");
            }

            postToUpdate.Text = HtmlSanitizer.Sanitize(this.markdownConverter.Transform(postToUpdate.Text));

            this.forumRepository.UpdatePost(postToUpdate);

            string url = this.GetPostUrl(postToUpdate.Topic.Id, postToUpdate);
            return this.Redirect(url);
        }

        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult FirstNewPostInTopic(int id)
        {
            Post post = this.forumRepository.GetFirstUnreadPostForTopic(id);

            string url = this.GetPostUrl(id, post);
            return this.Redirect(url);
        }

        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult Forum(int id, int page)
        {
            PagingParameters pagingParameters = new PagingParameters { CurrentPage = page, PageSize = TopicsPerForumPage };

            Forum forum = this.forumRepository.GetForumById(id);
            pagingParameters.TotalCount = forum.TopicCount;

            IEnumerable<Topic> topics = this.forumRepository.GetTopicsForForum(id, pagingParameters);

            return
                this.View(
                    new ForumPage { Forum = forum, Topics = topics, PagingParameters = pagingParameters, PostsPerTopicPage = PostsPerTopicPage });
        }

        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult Index()
        {
            MediaCommUser currentUser = this.currentUserContainer.User;
            currentUser.LastVisit = DateTime.Now;

            this.userRepository.UpdateUser(currentUser);

            return this.View(this.forumRepository.GetAllForums());
        }

        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult Topic(int id, int page)
        {
            PagingParameters pagingParameters = new PagingParameters { CurrentPage = page, PageSize = PostsPerTopicPage };

            Topic topic = this.forumRepository.GetTopicById(id);

            if (topic == null)
            {
                throw new HttpException(404, "HTTP/1.1 404 Not Found");
            }

            pagingParameters.TotalCount = topic.PostCount;

            IEnumerable<Post> posts = this.forumRepository.GetPostsForTopic(id, pagingParameters);

            return this.View(new TopicPage { Topic = topic, Posts = posts, PagingParameters = pagingParameters });
        }

        [HttpPost]
        [ValidateInput(false)]
        [NHibernateActionFilter]
        public ActionResult Topic(int id, Post post)
        {
            post.Topic = this.forumRepository.GetTopicById(id);
            post.Author = this.currentUserContainer.User;
            post.Created = DateTime.Now;

            post.Text = HtmlSanitizer.Sanitize(this.markdownConverter.Transform(post.Text));

            this.forumRepository.AddPost(post);

            this.notificationSender.SendForumsNotification(post);

            return this.Redirect(this.GetPostUrl(id, post));
        }

        private string GetPostUrl(int topicId, Post post)
        {
            int page = this.forumRepository.GetPageNumberForPost(postId: post.Id, topicId: topicId, pageSize: PostsPerTopicPage);
            return this.Url.GetPostUrl(post, page);
        }
    }
}