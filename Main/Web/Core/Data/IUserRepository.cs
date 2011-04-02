using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaCommMVC.Core.Data
{
    using MediaCommMVC.Core.Model;

    public interface IUserRepository : IRepository<MediaCommUser>
    {
        MediaCommUser GetByName(string name);
    }
}