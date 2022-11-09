using Bookzilla.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Bookzilla.API.DataAccessLayer
{
    public class BookzillaDbContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Collection> Collections { get; set; }

        public string DbPath { get; }

        public BookzillaDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "bookzilla.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }

}
