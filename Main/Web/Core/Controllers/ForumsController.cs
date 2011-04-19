namespace MediaCommMVC.Core.Controllers
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using Data;
    using Infrastructure;

    using MediaCommMVC.Core.Helpers;
    using MediaCommMVC.Core.ViewModels;
    using MediaCommMVC.Core.ViewModels.Pages;

    using Model;
    using ViewModel;
    using AutoMapper;

    #endregion

    [Authorize]
    public class ForumsController : Controller
    {
        private readonly IForumsRepository forumsRepository;

        private readonly IUserRepository userRepository;

        public ForumsController(IForumsRepository forumsRepository, IUserRepository userRepository)
        {
            this.forumsRepository = forumsRepository;
            this.userRepository = userRepository;
        }

        [TransactionFilter]
        public ActionResult Index()
        {
            IEnumerable<Forum> forums = this.forumsRepository.GetAll();
            IEnumerable<ForumViewModel> forumViewModels = Mapper.Map<IEnumerable<Forum>, IEnumerable<ForumViewModel>>(forums);
            ForumsIndexViewModel forumsIndexViewModel = new ForumsIndexViewModel { Forums = forumViewModels };

            return this.View(forumsIndexViewModel);
        }

        public ActionResult Forum(int id)
        {
            ForumPageViewModel forumPageViewModel = new ForumPageViewModel
                {
                    Topics =
                        new List<TopicViewModel>
                            {
                                new TopicViewModel
                                    {
                                        CreatedBy = "Autor1",
                                        Id = "1",
                                        LastPostAuthor = "Autor2",
                                        PostCount = "1",
                                        Title = "Title 1"
                                    },
                                new TopicViewModel
                                    {
                                        CreatedBy = "Autor3",
                                        Id = "2",
                                        LastPostAuthor = "Autor4",
                                        PostCount = "5",
                                        Title = "Title abc"
                                    }
                            }
                };

            return this.View(forumPageViewModel);
        }

        [HttpGet]
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
            //this.RedirectToAction("Topic", new { id = createdTopic.Id, name = this.Url.ToFriendlyUrl(createdTopic.Title) });
        }
    }
}
