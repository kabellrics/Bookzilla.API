using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookzilla.API.Mapper
{
    public class FTPConfig
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Adresse { get; set; }
        public string Path { get; set; }
        public string CollectionArtPath { get; set; }
        public string SerieCoverPath { get; set; }
        public string AlbumPath { get; set; }
    }
}
