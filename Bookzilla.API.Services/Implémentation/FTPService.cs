using Bookzilla.API.Models;
using Bookzilla.API.Services.Interface;
using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookzilla.API.Services.Implémentation
{
    public class FTPService : IFTPService
    {
        private FTPConfig fTPsettings;

        public FTPService(FTPConfig fTPsettings)
        {
            this.fTPsettings = fTPsettings;
        }

        public async Task UploadCollectionArt(Stream filestream,string Filename)
        {
            using (var client = new AsyncFtpClient(fTPsettings.Adresse,fTPsettings.User,fTPsettings.Password))
            {
                await client.AutoConnect();
                var remotePath = Path.Combine(fTPsettings.Path, fTPsettings.CollectionArtPath, Filename);
                await client.UploadStream(filestream, remotePath,FtpRemoteExists.Overwrite,true);
            }
        }
        public async Task UploadSerieArt(Stream filestream,string Filename)
        {
            using (var client = new AsyncFtpClient(fTPsettings.Adresse,fTPsettings.User,fTPsettings.Password))
            {
                await client.AutoConnect();
                var remotePath = Path.Combine(fTPsettings.Path, fTPsettings.SerieCoverPath, Filename);
                await client.UploadStream(filestream, remotePath,FtpRemoteExists.Overwrite,true);
            }
        }
        public async Task UploadFileArt(Stream filestream,string Filename)
        {
            using (var client = new AsyncFtpClient(fTPsettings.Adresse,fTPsettings.User,fTPsettings.Password))
            {
                await client.AutoConnect();
                var remotePath = Path.Combine(fTPsettings.Path, fTPsettings.AlbumPath, Filename);
                await client.UploadStream(filestream, remotePath,FtpRemoteExists.Overwrite,true);
            }
        }
    }
}
