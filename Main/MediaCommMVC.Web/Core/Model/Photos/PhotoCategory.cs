using System.Collections.Generic;

namespace MediaCommMVC.Web.Core.Model.Photos
{
    public class PhotoCategory
    {
        private IEnumerable<PhotoAlbum> albums = new List<PhotoAlbum>();

        public virtual int AlbumCount { get; protected set; }

        public virtual IEnumerable<PhotoAlbum> Albums
        {
            get
            {
                return this.albums;
            }

            protected set
            {
                this.albums = value;
            }
        }

        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual int PhotoCount { get; protected set; }

        public override string ToString()
        {
            return string.Format("Id: '{0}', Name: '{1}'", this.Id, this.Name);
        }
    }
}