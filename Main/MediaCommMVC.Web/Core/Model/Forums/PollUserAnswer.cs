using MediaCommMVC.Web.Core.Model.Users;

namespace MediaCommMVC.Web.Core.Model.Forums
{
    public class PollUserAnswer
    {
        public virtual PollAnswer Answer { get; set; }

        public virtual int Id { get; set; }

        public virtual Poll Poll { get; set; }

        public virtual MediaCommUser User { get; set; }

        public override string ToString()
        {
            return string.Format("Id: '{0}', Answer: {1}, User: {2}", this.Id, this.Answer, this.User);
        }
    }
}