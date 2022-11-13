using Bookzilla.API.Mapper;
using Bookzilla.API.Services.Interface;
using ICSharpCode.SharpZipLib.Zip;
using SharpCompress.Archives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookzilla.API.Services.Implémentation
{
    public class CoverExtractorService: ICoverExtractorService
    {
        private readonly IFTPService _ftpservice;
        public CoverExtractorService(IFTPService ftpservice)
        {
            _ftpservice = ftpservice;
        }
        public async Task<String> ExtractCoverForFile(String path)
        {
            if (Path.GetExtension(path).Contains("cbz"))
            {
                return ExtractCoverForCBZ(path);
            }
            else if (Path.GetExtension(path).Contains("cbr")) 
            {
                return ExtractCoverForCBR(path);
            }
            else
                return string.Empty;
        }

        private String ExtractCoverForCBR(string path)
        {
            var archive = ArchiveFactory.Open(path);
            var firstpage = archive.Entries.FirstOrDefault(x => x.IsDirectory == false);
            if(firstpage != null)
            {
                return firstpage.Key;
            }
            else
                return string.Empty;
        }

        private String ExtractCoverForCBZ(string FilePath)
        {

            using (ZipInputStream s = new ZipInputStream(File.OpenRead(FilePath)))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {

                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    // create directory if the archive has a folder at root
                    if (directoryName.Length > 0)
                    {
                        string fDirectory = Path.GetTempPath() + directoryName;

                        //We need to delete the directory is it's already 
                        //there so we get the first entry
                        if (Directory.Exists(fDirectory))
                        {
                            Directory.Delete(fDirectory, true);
                        }
                        Directory.CreateDirectory(fDirectory);
                    }

                    string fullPath = Path.GetTempPath() + theEntry.Name;

                    if (fileName != String.Empty && !File.Exists(fullPath))
                    {
                        using (FileStream streamWriter = File.Create(fullPath))
                        {
                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            return fullPath;
                        }
                    }
                    return string.Empty;
                }
            }
            return string.Empty;
        }
    }
}
