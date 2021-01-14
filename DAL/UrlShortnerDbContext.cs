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
            //add connection string here
            //optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-T9571MCS\SQLEXPRESS;Initial Catalog=UrlShortenerDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        public DbSet<UrlInfo> Urls { get; set; }
    }
}
