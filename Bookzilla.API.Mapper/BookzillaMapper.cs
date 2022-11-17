using AutoMapper;
using Bookzilla.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookzilla.API.Mapper
{
    public class BookzillaMapper : Profile
    {
        public BookzillaMapper()
        {
            CreateMap<AlbumDTO, Album>().ReverseMap();
            CreateMap<SerieDTO, Serie>().ReverseMap();
            CreateMap<CollectionDTO, Collection>().ReverseMap();
        }
    }
}
