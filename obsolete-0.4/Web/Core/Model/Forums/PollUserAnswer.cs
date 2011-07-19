namespace MediaCommMVC.Core.Model.Forums
{
    public class PollUserAnswer
    {
        #region Properties

        public virtual PollAnswer Answer { get; set; }

        public virtual int Id { get; set; }

        public virtual Poll Poll { get; set; }

        public virtual MediaCommUser User { get; set; }

        #endregion
    }
}