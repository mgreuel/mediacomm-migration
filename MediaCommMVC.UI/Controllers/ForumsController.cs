#region Using Directives

using System;
using System.Collections.Generic;
using System.Web.Mvc;

using MediaCommMVC.Common.Logging;
using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Forums;
using MediaCommMVC.Core.Parameters;

#endregion

namespace MediaCommMVC.UI.Controllers
{
    /// <summary>The forums controller.</summary>
    [Authorize]
    public class ForumsController : Controller
    {
        #region Constants and Fields

        /// <summary>
        ///   The forum repository.
        /// </summary>
        private readonly IForumRepository forumRepository;

        /// <summary>
        ///   The user repository.
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        ///   The logger.
        /// </summary>
        private readonly ILogger logger;

#warning get from config!

        /// <summary>
        ///   The number of topics displayed per page.
        /// </summary>
        private int topicPageSize = 25;

#warning get from config!

        /// <summary>
        ///   The number of posts displayed per page.
        /// </summary>
        private int postPageSize = 10;

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

        /// <summary>The forums index.</summary>
        /// <returns>The forums index view.</returns>
        public ActionResult Index()
        {
            this.logger.Debug("Displaying forums index.");
            return this.View(this.forumRepository.GetAllForums());
        }

        /// <summary>Displays the forum with the provided Id.</summary>
        /// <param name="id">The forum id.</param>
        /// <param name="page">The current page.</param>
        /// <returns>The forum view, displaying topics.</returns>
        public ActionResult Forum(int id, int page)
        {
            this.logger.Debug("Displaying page {0} of the forum with id '{1}'", page, id);

            PagingParameters pagingParameters = new PagingParameters
                {
                   CurrentPage = page, PageSize = this.topicPageSize 
                };
            IEnumerable<Topic> topics = this.forumRepository.GetTopicsForForum(id, pagingParameters);
            return this.View(topics);
        }

        /// <summary>Displays the topic with the specified id.</summary>
        /// <param name="id">The topic id.</param>
        /// <param name="page">The current page.</param>
        /// <returns>The topic view, displaying posts.</returns>
        public ActionResult Topic(int id, int page)
        {
            this.logger.Debug("Displaying page {0} of the topic with id '{1}'", page, id);

            PagingParameters pagingParameters = new PagingParameters
                {
                   CurrentPage = page, PageSize = this.postPageSize 
                };
            return this.View(this.forumRepository.GetPostsForTopic(id, pagingParameters));
        }

        /// <summary>Adds a new reply to the topic.</summary>
        /// <param name="id">The topic id.</param>
        /// <param name="post">The post to add.</param>
        /// <returns>The last page of the topic.</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Topic(int id, Post post)
        {
            this.logger.Debug("Adding post '{0}' to the topic with id '{1}'", post, id);

            post.Topic = this.forumRepository.GetTopicById(id);
            post.Author = this.userRepository.GetUserByName(this.User.Identity.Name);
            post.Created = DateTime.Now;

            this.forumRepository.AddPost(post);

            int lastPage = this.forumRepository.GetLastPageNumberForTopic(id, this.postPageSize);

            this.logger.Debug("Redirecting to page {0} of the topic with the id '{0}'", lastPage, id);
            return this.RedirectToAction("Topic", new { page = lastPage });
        }

        /// <summary>Displays the create topic page.</summary>
        /// <returns>The create topic view.</returns>
        public ActionResult CreateTopic()
        {
            this.logger.Debug("Displaying create topic page.");
            return this.View();
        }

        /// <summary>Creates the topic.</summary>
        /// <param name="topic">The topic.</param>
        /// <param name="post">The first post.</param>
        /// <param name="id">The forum id.</param>
        /// <returns>The added topic view.</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateTopic(Topic topic, Post post, int id)
        {
            this.logger.Debug("Creating topic '{0}' with post '{1}' and forumId '{2}'", topic, post, id);

            post.Author = this.userRepository.GetUserByName(this.User.Identity.Name);

            topic.Forum = this.forumRepository.GetForumById(id);

            this.forumRepository.AddTopic(topic, post);

            return this.View();
        }

        #endregion
    }
}