using System.ComponentModel.DataAnnotations;

using MediaCommMVC.Web.Core.Model.Users;

namespace MediaCommMVC.Web.Core.Model.Movies
{
    public class Movie
    {
        public virtual int Id { get; protected set; }

        public virtual string InfoLink { get; set; }

        [Required]
        public virtual MovieLanguage Language { get; set; }

        public virtual MediaCommUser Owner { get; set; }

        [Required]
        public virtual MovieQuality Quality { get; set; }

        [Required]
        [StringLength(255)]
        public virtual string Title { get; set; }

        public override string ToString()
        {
            return string.Format("Id: '{0}', Title: '{1}, Owner: '{2}", this.Id, this.Title, this.Owner);
        }
    }
}