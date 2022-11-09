using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookzilla.API.Models
{
    public class Album
    {
        public int Id { get; set; }
        public int SerieId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int Order { get; set; }
        public int CurrentPage { get; set; }
        public ReadingStatus ReadingStatus{ get; set; }
    }
}
