using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using MediaCommMVC.Web.Core.Common.Logging;
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
    public class ForumsController : Controller
    {
        private const int PostsPerTopicPage = 15;

        private const int TopicsPerForumPage = 25;

        private readonly IForumRepository forumRepository;

        private readonly ILogger logger;

        private readonly IUserRepository userRepository;

        private MediaCommUser currentUser;

        public ForumsController(IForumRepository forumRepository, IUserRepository userRepository, ILogger logger)
        {
            this.forumRepository = forumRepository;
            this.userRepository = userRepository;
            this.logger = logger;
        }

        [HttpPost]
        [NHibernateActionFilter]
        public RedirectResult AnswerPoll(int pollId, int[] answerIds)
        {
            this.logger.Debug("User '{0}' answered poll '{1}' with '{2}'", this.GetCurrentUser().UserName, pollId, string.Join(",", answerIds));

            foreach (int id in answerIds)
            {
                PollAnswer answer = this.forumRepository.GetPollAnswerById(id);
                Poll poll = answer.Poll;
                PollUserAnswer userAnswer = new PollUserAnswer { User = this.GetCurrentUser(), Answer = answer, Poll = poll };

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
            this.logger.Debug("Creating topic '{0}' with post '{1}' and forumId '{2}'", topic, post, id);

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

            Topic createdTopic = this.forumRepository.AddTopic(topic, post);

            return this.RedirectToAction("Topic", new { id = createdTopic.Id, name = this.Url.ToFriendlyUrl(createdTopic.Title) });
        }

        [HttpPost]
        [NHibernateActionFilter]
        public ActionResult DeletePost(int id)
        {
#warning check if allowed
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
#warning check if allowed
            Post post = this.forumRepository.GetPostById(id);

            return this.View(post);
        }

        [HttpPost]
        [ValidateInput(false)]
        [NHibernateActionFilter]
        public ActionResult EditPost(int id, Post post)
        {
#warning check if allowed
            this.logger.Debug("Updating post '{0}' with topicId '{1}'", post, id);

            Post postToUpdate = this.forumRepository.GetPostById(id);
            postToUpdate.Text = post.Text;

            this.forumRepository.UpdatePost(postToUpdate);

            string url = this.GetPostUrl(postToUpdate.Topic.Id, postToUpdate);
            return this.Redirect(url);
        }

        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult FirstNewPostInTopic(int id)
        {
            Post post = this.forumRepository.GetFirstUnreadPostForTopic(id, this.GetCurrentUser());

            string url = this.GetPostUrl(id, post);
            return this.Redirect(url);
        }

        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult Forum(int id, int page)
        {
            this.logger.Debug("Displaying page {0} of the forum with id '{1}'", page, id);

            PagingParameters pagingParameters = new PagingParameters { CurrentPage = page, PageSize = TopicsPerForumPage };

            Forum forum = this.forumRepository.GetForumById(id);
            pagingParameters.TotalCount = forum.TopicCount;

            IEnumerable<Topic> topics = this.forumRepository.GetTopicsForForum(id, pagingParameters, this.GetCurrentUser());

            return
                this.View(
                    new ForumPage { Forum = forum, Topics = topics, PagingParameters = pagingParameters, PostsPerTopicPage = PostsPerTopicPage });
        }

        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult Index()
        {
            return this.View(this.forumRepository.GetAllForums(this.GetCurrentUser()));
        }

        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult Post(int id)
        {
            Post post = this.forumRepository.GetPostById(id);

            string url = this.GetPostUrl(post.Topic.Id, post);

            return this.Redirect(url);
        }

        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult Topic(int id, int page)
        {
            PagingParameters pagingParameters = new PagingParameters { CurrentPage = page, PageSize = PostsPerTopicPage };

            Topic topic = this.forumRepository.GetTopicById(id);
            pagingParameters.TotalCount = topic.PostCount;

            IEnumerable<Post> posts = this.forumRepository.GetPostsForTopic(
                id, pagingParameters, this.userRepository.GetUserByName(this.User.Identity.Name));

            return this.View(new TopicPage { Topic = topic, Posts = posts, PagingParameters = pagingParameters });
        }

        [HttpPost]
        [ValidateInput(false)]
        [NHibernateActionFilter]
        public ActionResult Topic(int id, Post post)
        {
            this.logger.Debug("Adding post '{0}' to the topic with id '{1}'", post, id);

            post.Topic = this.forumRepository.GetTopicById(id);
            post.Author = this.GetCurrentUser();
            post.Created = DateTime.Now;

            this.forumRepository.AddPost(post);

            int lastPage = this.forumRepository.GetLastPageNumberForTopic(id, PostsPerTopicPage);

            this.logger.Debug("Redirecting to page {0} of the topic with the id '{0}'", lastPage, id);

#warning redirect to latest post
            return this.RedirectToAction("Topic", new { page = lastPage });
        }

        private MediaCommUser GetCurrentUser()
        {
            return this.currentUser ?? (this.currentUser = this.userRepository.GetUserByName(this.User.Identity.Name));
        }

        private string GetPostUrl(int topicId, Post post)
        {
            int page = this.forumRepository.GetPageNumberForPost(postId: post.Id, topicId: topicId, pageSize: PostsPerTopicPage);
            string postAnker = string.Format("#{0}", post.Id);

            this.logger.Debug("Redirecting to page {0} of the topic with the id '{0}'", page, topicId);

            return this.Url.RouteUrl("ViewTopic", new { id = post.Topic.Id, page, name = this.Url.ToFriendlyUrl(post.Topic.Title) }) + postAnker;
        }
    }
}