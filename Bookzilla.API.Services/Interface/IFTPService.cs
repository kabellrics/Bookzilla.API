using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookzilla.API.Services.Interface
{
    public interface IFTPService
    {
        Task UploadCollectionArt(Stream filestream, string Filename);
        Task UploadSerieArt(Stream filestream, string Filename);
        Task UploadFileArt(Stream filestream, string Filename);
    }
}
