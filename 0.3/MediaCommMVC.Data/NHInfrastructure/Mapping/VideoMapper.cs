using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

using MediaCommMVC.Core.Model.Videos;

namespace MediaCommMVC.Data.NHInfrastructure.Mapping
{
    public class VideoMapper : IAutoMappingOverride<Video>
    {
        public void Override(AutoMapping<Video> mapping)
        {
            mapping.Table("Videos");
            mapping.References(v => v.Uploader).Not.Nullable().Cascade.SaveUpdate();
            mapping.References(v => v.VideoCategory).Not.Nullable();
        }
    }
}
