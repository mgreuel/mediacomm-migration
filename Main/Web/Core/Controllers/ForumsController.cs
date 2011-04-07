namespace MediaCommMVC.Core.Controllers
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Data;
    using Infrastructure;
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

        [HttpGet]
        public ActionResult CreateTopic(int id)
        {
            IEnumerable<string> userNames = this.userRepository.GetAll().Select(u => u.UserName).ToList();
            CreateTopicViewModel createTopicViewModel = new CreateTopicViewModel { UserNames = userNames };

            return this.View(createTopicViewModel);
        }
    }
}
