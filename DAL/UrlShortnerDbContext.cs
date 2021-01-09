using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class UrlShortnerDbContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //add connection string here
            optionsBuilder.UseSqlServer(@"Add-Connection-String-Here");
        }
        public DbSet<UrlInfo> Urls { get; set; }
    }
}
