using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MediaCommMVC.Web.Core.Model.Users;

namespace MediaCommMVC.Web.Core.Model
{
    public class Approval
    {
        public virtual int Id { get; set; }

        public virtual MediaCommUser ApprovedBy { get; set; }

        public virtual string ApprovedUrl { get; set; }
    }
}