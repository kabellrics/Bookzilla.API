using Bookzilla.API.Mapper;
using Bookzilla.API.Services.Interface;
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
    }
}
