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

        [HttpGet]
        [NHibernateActionFilter]
        public ActionResult CreateTopic(int id)
        {
            IEnumerable<string> userNames = this.userRepository.GetAll().Select(u => u.UserName).ToList();
            CreateTopicViewModel createTopicViewModel = new CreateTopicViewModel { UserNames = userNames };

            return this.View(createTopicViewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult CreateTopic(CreateTopicViewModel createTopic, int id)
        {
            createTopic.PostText = UrlResolver.ResolveLinks(createTopic.PostText);

            return this.RedirectToAction("Index");

            // this.RedirectToAction("Topic", new { id = createdTopic.Id, name = this.Url.ToFriendlyUrl(createdTopic.Title) });
        }

        [NHibernateActionFilter]
        public ActionResult Forum(int id, int page)
        {
            // ForumPageViewModel forumPageViewModel = new ForumPageViewModel
            // {
            // ForumTitle = "SomeForum",
            // ForumId = id.ToString(),
            // PagingParameters = new PagingParameters { CurrentPage = 1, PageSize = 10, TotalCount = 34 },
            // Topics =
            // new List<TopicViewModel> {
            // new TopicViewModel
            // {
            // CreatedBy = "Autor1", 
            // Id = "1", 
            // LastPostAuthor = "Autor2", 
            // LastPostTime = DateTime.Now.ToString(), 
            // ExcludedUsers = "Schlaefisch, test",
            // PostCount = 60, 
            // Title = "Title 1"
            // }, 
            // new TopicViewModel
            // {
            // CreatedBy = "Autor3", 
            // Id = "2", 
            // LastPostAuthor = "Autor4", 
            // LastPostTime = DateTime.Now.ToString(), 
            // PostCount = 20, 
            // Title = "Title abc"
            // }
            // }
            // };
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
        public ActionResult Topic(int id)
        {
            TopicPageViewModel topicPageViewModel = new TopicPageViewModel
                {
                    Posts =
                        new List<PostViewModel> {
                                new PostViewModel
                                    {
                                        AuthorUserName = "User1", 
                                        CreatedDate = "10.1", 
                                        CurrentUserIsAllowedToEdit = true, 
                                        Id = "1", 
                                        Text = "FirstPost"
                                    }, 
                                new PostViewModel
                                    {
                                        AuthorUserName = "User3", 
                                        CreatedDate = "5.2", 
                                        CurrentUserIsAllowedToEdit = false, 
                                        Id = "2", 
                                        Text = "SecondPost"
                                    }
                            }
                };

            return this.View(topicPageViewModel);
        }

        #endregion
    }
}