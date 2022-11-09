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
            CreateMap<AlbumDTO, Album>();
            CreateMap<Album, AlbumDTO>();
            CreateMap<SerieDTO, Serie>();
            CreateMap<Serie, SerieDTO>();
            CreateMap<CollectionDTO, Collection>();
            CreateMap<Collection, CollectionDTO>();
        }
    }
}
