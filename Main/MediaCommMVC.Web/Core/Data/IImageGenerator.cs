using System.Collections.Generic;
using System.IO;

namespace MediaCommMVC.Web.Core.Data
{
    public interface IImageGenerator
    {
        void GenerateImages(string pathToPhotos, IEnumerable<FileInfo> sourceImages);
    }
}