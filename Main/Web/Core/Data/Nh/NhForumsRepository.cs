using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaCommMVC.Core.Data.Nh
{
    using MediaCommMVC.Core.Model;

    using NHibernate;

    public class NhForumsRepository : NhRepository<Forum>, IForumsRepository
    {
        public NhForumsRepository(ISession session)
            : base(session)
        {
        }
    }
}