using Bookzilla.API.Mapper;
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

        public async Task<String> UploadCollectionArt(Stream filestream,string Filename)
        {
            using (var client = new AsyncFtpClient(fTPsettings.Adresse,fTPsettings.User,fTPsettings.Password))
            {
                try
                {
                    await client.AutoConnect();
                    var remotePath = Path.Combine(fTPsettings.Path, fTPsettings.CollectionArtPath, Filename);
                    await client.UploadStream(filestream, remotePath, FtpRemoteExists.Overwrite, true);
                    return remotePath;
                }
                catch (Exception ex)
                {
                    //throw;
                    return String.Empty;
                }
            }
        }
        public async Task<String> UploadSerieArt(Stream filestream,string Filename)
        {
            using (var client = new AsyncFtpClient(fTPsettings.Adresse,fTPsettings.User,fTPsettings.Password))
            {
                await client.AutoConnect();
                var remotePath = Path.Combine(fTPsettings.Path, fTPsettings.SerieCoverPath, Filename);
                await client.UploadStream(filestream, remotePath,FtpRemoteExists.Overwrite,true);
                return remotePath;
            }
        }
        public async Task<String> UploadAlbumCover(String filesource,String filename)
        {
            using (var client = new AsyncFtpClient(fTPsettings.Adresse,fTPsettings.User,fTPsettings.Password))
            {
                await client.AutoConnect();
                var remotePath = Path.Combine(fTPsettings.Path, fTPsettings.AlbumCoverPath, filename);
                await client.UploadFile(filesource, remotePath,FtpRemoteExists.Overwrite,true);
                return remotePath;
            }
        }
        public async Task<String> UploadFileArt(Stream filestream,string Filename)
        {
            using (var client = new AsyncFtpClient(fTPsettings.Adresse,fTPsettings.User,fTPsettings.Password))
            {
                await client.AutoConnect();
                var remotePath = Path.Combine(fTPsettings.Path, fTPsettings.AlbumPath, Filename);
                await client.UploadStream(filestream, remotePath,FtpRemoteExists.Overwrite,true);
                return remotePath;
            }
        }
        public async Task<byte[]> GetStreamAsync(String target)
        {
            using (var client = new AsyncFtpClient(fTPsettings.Adresse, fTPsettings.User, fTPsettings.Password))
            {
                await client.AutoConnect();         
                var stream = await client.DownloadBytes(target,0);
                return stream;
                

            }
        }
        public async Task DownloadOnLocalFile(String target,string source)
        {
            using (var client = new AsyncFtpClient(fTPsettings.Adresse,fTPsettings.User,fTPsettings.Password))
            {
                await client.AutoConnect();
                await client.DownloadFile(target, source,FtpLocalExists.Overwrite);
            }
        }
    }
}
