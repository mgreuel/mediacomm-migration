using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediaCommMVC.Core.Controllers
{
    using System.Collections;

    using AutoMapper;

    using MediaCommMVC.Core.Data;
    using MediaCommMVC.Core.Infrastructure;
    using MediaCommMVC.Core.Model;
    using MediaCommMVC.Core.ViewModel;

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
