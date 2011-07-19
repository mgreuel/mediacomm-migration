namespace MediaCommMVC.Core.Model.Forums
{
    public class PollAnswer
    {
        #region Properties

        public virtual int Id { get; set; }

        public virtual Poll Poll { get; set; }

        public virtual string Text { get; set; }

        #endregion
    }
}