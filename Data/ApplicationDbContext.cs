using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace UrlShortener.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<UrlEntry> UrlEntries { get; set; }
        public DbSet<Report> Reports { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
