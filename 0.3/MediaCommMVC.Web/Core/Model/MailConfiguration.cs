using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaCommMVC.Web.Core.Model
{
    public class MailConfiguration
    {
        public string SmtpHost { get; set; }

        public string MailFrom { get; set; }
    }
}