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
            Mapper.CreateMap<Forum, ForumViewModel>().ForMember(
                v => v.LastPostTime,
                opt =>
                opt.MapFrom(
                    f => string.IsNullOrEmpty(f.LastPostAuthor) ? string.Empty : string.Format("{0:g}", f.LastPostTime)));
            Mapper.CreateMap<Forum[], ForumViewModel[]>();

            Mapper.AssertConfigurationIsValid();
        }
    }
}