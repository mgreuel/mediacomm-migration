using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaCommMVC.Core.Infrastructure
{
    using AutoMapper;

    using MediaCommMVC.Core.Model;
    using MediaCommMVC.Core.ViewModel;

    public static class AutomapperSetup
    {
        public static void Initialize()
        {
            Mapper.CreateMap<Forum, ForumViewModel>();
            Mapper.CreateMap<Forum[], ForumViewModel[]>();

            Mapper.AssertConfigurationIsValid();
        }
    }
}