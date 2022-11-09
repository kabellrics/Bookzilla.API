using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookzilla.API.Mapper
{
    public class SerieDTO
    {
        public int Id { get; set; }
        public int CollectionId { get; set; }
        public string Name { get; set; }
        public string CoverArtPath { get; set; }
    }
}
