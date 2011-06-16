using System.ComponentModel.DataAnnotations;

namespace MediaCommMVC.Web.Core.Model.Movies
{
    public class MovieLanguage
    {
        public virtual int Id { get; protected set; }

        [StringLength(50)]
        public virtual string Name { get; set; }

        public override string ToString()
        {
            return this.Name ?? string.Empty;
        }
    }
}