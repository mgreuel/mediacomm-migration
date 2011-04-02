﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaCommMVC.Core.Data
{
    public interface IRepository<T>
    {
        T GetById(int id);
    }
}