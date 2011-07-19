namespace MediaCommMVC.Core.Controllers
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using MediaCommMVC.Core.Data;
    using MediaCommMVC.Core.Helpers;
    using MediaCommMVC.Core.Infrastructure;
    using MediaCommMVC.Core.Model.Forums;
    using MediaCommMVC.Core.Services;
    using MediaCommMVC.Core.ViewModel;
    using MediaCommMVC.Core.ViewModels;
    using MediaCommMVC.Core.ViewModels.Pages.Forums;

    #endregion

    [Authorize]
    public class ForumsController : Controller
    {
        #region Constants and Fields

        private readonly IForumsRepository forumsRepository;

        private readonly IForumsService forumsService;

        private readonly IUserRepository userRepository;

        #endregion

        #region Constructors and Destructors

        public ForumsController(IForumsRepository forumsRepository, IUserRepository userRepository, IForumsService forumsService)
        {
            this.forumsRepository = forumsRepository;
            this.userRepository = userRepository;
            this.forumsService = forumsService;
        }

        #endregion

        #region Public Methods


        /// <param name="id">The forum id.</param>
        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult CreateTopic(int id)
        {
            IEnumerable<string> userNames = this.userRepository.GetAll().Select(u => u.UserName).ToList();
            ForumViewModel forum = Mapper.Map<Forum, ForumViewModel>(this.forumsRepository.GetById(id));

            CreateTopicViewModel createTopicViewModel = new CreateTopicViewModel { UserNames = userNames, Forum = forum };

            return this.View(createTopicViewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        [NHibernateActionFilter]
        public ActionResult CreateTopic(CreateTopicViewModel createTopic, int id)
        {
            TopicViewModel createdTopic = this.forumsService.AddNewTopic(createTopic, id);

            return this.RedirectToAction("Topic", new { id = createdTopic.Id, name = createdTopic.UrlFriendlyTitle });
        }

        [NHibernateActionFilter]
        public ActionResult Forum(int id, int page)
        {
            ForumPageViewModel forumPageViewModel = this.forumsService.GetForumPage(id, page);

            return this.View(forumPageViewModel);
        }

        [NHibernateActionFilter]
        public ActionResult Index()
        {
            IEnumerable<Forum> forums = this.forumsRepository.GetAll();
            IEnumerable<ForumViewModel> forumViewModels = Mapper.Map<IEnumerable<Forum>, IEnumerable<ForumViewModel>>(forums);
            ForumsIndexViewModel forumsIndexViewModel = new ForumsIndexViewModel { Forums = forumViewModels };

            return this.View(forumsIndexViewModel);
        }

        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult Topic(int id, int page)
        {
            TopicPageViewModel topicPageViewModel = this.forumsService.GetTopicPage(id, page);

            return this.View(topicPageViewModel);
        }

        #endregion
    }
}