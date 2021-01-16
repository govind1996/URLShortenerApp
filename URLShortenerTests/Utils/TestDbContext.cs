using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortenerTests.Utils
{
    class TestDbContext
    {
        public static DbContextOptions<UrlShortnerDbContext> GetOptions()
        {
            DbContextOptions<UrlShortnerDbContext> options;
            var builder = new DbContextOptionsBuilder<UrlShortnerDbContext>().UseInMemoryDatabase("TestDb");
            options = builder.Options;
            return options;
        }
    }
}
