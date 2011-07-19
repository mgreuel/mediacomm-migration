namespace MediaCommMVC.Web.Core.Model.Forums
{
    public class PollAnswer
    {
        public virtual int Id { get; set; }

        public virtual Poll Poll { get; set; }

        public virtual string Text { get; set; }

        public override string ToString()
        {
            return string.Format("Id: '{0}', Text: '{1}', Poll: {2}", this.Id, this.Text, this.Poll);
        }
    }
}