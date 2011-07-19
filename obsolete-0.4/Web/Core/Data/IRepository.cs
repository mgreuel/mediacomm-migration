namespace MediaCommMVC.Core.Data
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    public interface IRepository<T>
    {
        #region Public Methods

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAllMatching(Func<T, bool> filter);

        T GetById(int id);

        #endregion
    }
}