#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Mvc;

using MediaCommMVC.Common.Logging;
using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Forums;
using MediaCommMVC.Core.Model.Users;
using MediaCommMVC.Core.Parameters;
using MediaCommMVC.UI.Helpers;
using MediaCommMVC.UI.ViewModel;

#endregion

namespace MediaCommMVC.UI.Controllers
{
    /// <summary>The forums controller.</summary>
    [Authorize]
    public class ForumsController : Controller
    {
        #region Constants and Fields

#warning get from config
        /// <summary>
        ///   The number of posts displayed per page.
        /// </summary>
        private const int PostsPerTopicPage = 10;

#warning get from config
        /// <summary>
        ///   The number of topics displayed per page.
        /// </summary>
        private const int TopicsPerForumPage = 25;

        /// <summary>
        ///   The forum repository.
        /// </summary>
        private readonly IForumRepository forumRepository;

        /// <summary>
        ///   The logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        ///   The user repository.
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        ///   The current user.
        /// </summary>
        private MediaCommUser currentUser;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="ForumsController"/> class.</summary>
        /// <param name="forumRepository">The forum repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="logger">The logger.</param>
        public ForumsController(IForumRepository forumRepository, IUserRepository userRepository, ILogger logger)
        {
            this.forumRepository = forumRepository;
            this.userRepository = userRepository;
            this.logger = logger;
        }

        #endregion

        #region Public Methods

        /// <summary>Displays the create topic page.</summary>
        /// <returns>The create topic view.</returns>
        public ActionResult CreateTopic()
        {
            return this.View();
        }

        /// <summary>Creates the topic.</summary>
        /// <param name="topic">The topic.</param>
        /// <param name="post">The first post.</param>
        /// <param name="id">The forum id.</param>
        /// <returns>The added topic view.</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult CreateTopic(Topic topic, Post post, int id)
        {
            this.logger.Debug("Creating topic '{0}' with post '{1}' and forumId '{2}'", topic, post, id);

            post.Author = this.userRepository.GetUserByName(this.User.Identity.Name);
            topic.Forum = this.forumRepository.GetForumById(id);
            Topic createdTopic = this.forumRepository.AddTopic(topic, post);

            return this.RedirectToAction("Topic", new { id = createdTopic.Id, name = this.Url.ToFriendlyUrl(createdTopic.Title) });
        }

        /// <summary>
        /// Firsts the new post in topic.
        /// </summary>
        /// <param name="id">The topic id.</param>
        /// <returns>Reirect to the first unread post of the topic.</returns>
        public ActionResult FirstNewPostInTopic(int id)
        {
            Contract.Requires(id > 0);

            Post post = this.forumRepository.GetFirstUnreadPostForTopic(id, this.GetCurrentUser());

            string url = this.GetPostUrl(id, post);
            return this.Redirect(url);
        }

        /// <summary>Displays the forum with the provided Id.</summary>
        /// <param name="id">The forum id.</param>
        /// <param name="page">The current page.</param>
        /// <returns>The forum view, displaying topics.</returns>
        public ActionResult Forum(int id, int page)
        {
            this.logger.Debug("Displaying page {0} of the forum with id '{1}'", page, id);

            PagingParameters pagingParameters = new PagingParameters { CurrentPage = page, PageSize = TopicsPerForumPage };

            Forum forum = this.forumRepository.GetForumById(id);
            pagingParameters.TotalCount = forum.TopicCount;

            IEnumerable<Topic> topics = this.forumRepository.GetTopicsForForum(id, pagingParameters, this.GetCurrentUser());

            return this.View(new ForumPage { Forum = forum, Topics = topics, PagingParameters = pagingParameters, PostsPerTopicPage = PostsPerTopicPage });
        }

        /// <summary>The forums index.</summary>
        /// <returns>The forums index view.</returns>
        public ActionResult Index()
        {
            return this.View(this.forumRepository.GetAllForums(this.GetCurrentUser()));
        }

        /// <summary>Displays the topic with the specified id.</summary>
        /// <param name="id">The topic id.</param>
        /// <param name="page">The current page.</param>
        /// <returns>The topic view, displaying posts.</returns>
        public ActionResult Topic(int id, int page)
        {
            this.logger.Debug("Displaying page {0} of the topic with id '{1}'", page, id);

            PagingParameters pagingParameters = new PagingParameters { CurrentPage = page, PageSize = PostsPerTopicPage };

            Topic topic = this.forumRepository.GetTopicById(id);
            pagingParameters.TotalCount = topic.PostCount;

            IEnumerable<Post> posts = this.forumRepository.GetPostsForTopic(id, pagingParameters, this.userRepository.GetUserByName(this.User.Identity.Name));

            return this.View(new TopicPage { Topic = topic, Posts = posts, PagingParameters = pagingParameters });
        }

        /// <summary>Adds a new reply to the topic.</summary>
        /// <param name="id">The topic id.</param>
        /// <param name="post">The post to add.</param>
        /// <returns>The last page of the topic.</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Topic(int id, Post post)
        {
            this.logger.Debug("Adding post '{0}' to the topic with id '{1}'", post, id);

            post.Topic = this.forumRepository.GetTopicById(id);
            post.Author = this.GetCurrentUser();
            post.Created = DateTime.Now;

            this.forumRepository.AddPost(post);

            int lastPage = this.forumRepository.GetLastPageNumberForTopic(id, PostsPerTopicPage);

            this.logger.Debug("Redirecting to page {0} of the topic with the id '{0}'", lastPage, id);
            return this.RedirectToAction("Topic", new { page = lastPage });
        }

        /// <summary>
        /// Shows the edit post page.
        /// </summary>
        /// <param name="id">The post id.</param>
        /// <returns>The edit post view.</returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditPost(int id)
        {
#warning check if allowed
            Post post = this.forumRepository.GetPostById(id);

            return this.View(post);
        }

        /// <summary>
        /// Saves the changed made to the post.
        /// </summary>
        /// <param name="id">The post id.</param>
        /// <param name="post">The edited post.</param>
        /// <returns>Redirect to the topic the post belongs to.</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult EditPost(int id, Post post)
        {
#warning check if allowed
            this.logger.Debug("Updating post '{0}' with topicId '{1}'", post, id);

            Post postToUpdate = this.forumRepository.GetPostById(id);
            postToUpdate.Text = post.Text;

            this.forumRepository.UpdatePost(postToUpdate);

            string url = this.GetPostUrl(id, postToUpdate);
            return this.Redirect(url);
        }

        /// <summary>
        /// Gets the post URL.
        /// </summary>
        /// <param name="topicId">The topic id.</param>
        /// <param name="post">The  post.</param>
        /// <returns>The url to the post.</returns>
        private string GetPostUrl(int topicId, Post post)
        {
            int page = this.forumRepository.GetPageNumberForPost(postId: post.Id, topicId: topicId, pageSize: PostsPerTopicPage);
            string postAnker = string.Format("#{0}", post.Id);

            this.logger.Debug("Redirecting to page {0} of the topic with the id '{0}'", page, topicId);

            return this.Url.RouteUrl("ViewTopic", new { id = post.Topic.Id, page = page, name = this.Url.ToFriendlyUrl(post.Topic.Title) }) + postAnker;
        }

        #endregion

        #region Methods

        /// <summary>Gets the current user.</summary>
        /// <returns>The current user.</returns>
        private MediaCommUser GetCurrentUser()
        {
            return this.currentUser ?? (this.currentUser = this.userRepository.GetUserByName(this.User.Identity.Name));
        }

        #endregion
    }
}