namespace MediaCommMVC.Core.Model.Forums
{
    #region Using Directives

    using System;

    #endregion

    public class Post
    {
        #region Properties

        public virtual MediaCommUser Author { get; set; }

        public virtual DateTime Created { get; set; }

        public virtual int Id { get; protected set; }

        public virtual string Text { get; set; }

        public virtual Topic Topic { get; set; }

        #endregion
    }
}
