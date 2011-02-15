using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Core.Model.Videos;

namespace MediaCommMVC.Data.NHInfrastructure.Mapping
{
    public class VideoCategoryMapper : IAutoMappingOverride<VideoCategory>
    {
        public void Override(AutoMapping<VideoCategory> mapping)
        {
            mapping.Table("VideoCategories");
            mapping.Map(a => a.VideoCount).Formula("(SELECT COUNT(*) FROM videos WHERE videos.VideoCategoryId = Id)");
            mapping.HasMany(v => v.Videos).Inverse();
        }
    }
}
