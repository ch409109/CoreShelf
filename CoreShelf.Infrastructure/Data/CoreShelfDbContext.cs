using CoreShelf.Core.Entities;
using CoreShelf.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreShelf.Infrastructure.Data
{
    public class CoreShelfDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookConfiguration).Assembly);
        }
    }
}
