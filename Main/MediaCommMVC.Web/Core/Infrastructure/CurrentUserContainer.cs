﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Model.Users;

namespace MediaCommMVC.Web.Core.Infrastructure
{
    public class CurrentUserContainer
    {
        private readonly IUserRepository userRepository;

        private readonly IIdentity mediaCommIdentity;

        private MediaCommUser user;

        public CurrentUserContainer(IUserRepository userRepository, IIdentity mediaCommIdentity)
        {
            this.userRepository = userRepository;
            this.mediaCommIdentity = mediaCommIdentity;
        }

        public MediaCommUser User
        {
            get
            {
                return this.user ?? (this.user = this.userRepository.GetUserByName(this.UserName));
            }
        }

        public string UserName
        {
            get
            {
                return this.mediaCommIdentity.Name;
            }
        }
    }
}