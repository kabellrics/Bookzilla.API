using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookzilla.API.Services.Interface
{
    public interface IFTPService
    {
        Task<String> UploadCollectionArt(Stream filestream, string Filename);
        Task<String> UploadSerieArt(Stream filestream, string Filename);
        Task<String> UploadAlbumCover(String filesource, String filename);
        Task<String> UploadFileArt(Stream filestream, string Filename);
        Task<byte[]> GetStreamAsync(String target);
        Task DownloadOnLocalFile(String target, string source);
    }
}
