using Bookzilla.API.Models;
using Bookzilla.API.Services.Interface;
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
    }
}
