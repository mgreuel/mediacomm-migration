namespace MediaCommMVC.Core.Controllers
{
    #region Using Directives

    using System.Collections.Generic;
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

        public ForumsController(IForumsRepository forumsRepository)
        {
            this.forumsRepository = forumsRepository;
        }

        [TransactionFilter]
        public ActionResult Index()
        {
            IEnumerable<Forum> forums = this.forumsRepository.GetAll();
            IEnumerable<ForumViewModel> forumViewModels = Mapper.Map<IEnumerable<Forum>, IEnumerable<ForumViewModel>>(forums);
            ForumsIndexViewModel forumsIndexViewModel = new ForumsIndexViewModel { Forums = forumViewModels };

            return View(forumsIndexViewModel);
        }
    }
}
