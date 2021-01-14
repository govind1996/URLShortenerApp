using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DAL
{
    public class UrlShortnerDbContext : IdentityDbContext, IUrlShortnerDbContext
    {
        public UrlShortnerDbContext()
        {

        }
      
        public UrlShortnerDbContext(DbContextOptions<UrlShortnerDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            if(!optionsBuilder.IsConfigured)
            {
                //add connection string here
                optionsBuilder.UseSqlServer(@"Add-connection-string-here");
            }
        }
        public DbSet<UrlInfo> Urls { get; set; }
    }
}
