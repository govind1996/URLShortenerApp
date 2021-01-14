using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortenerTests.Utils
{
    class TestDbSeeder
    {
        public static UrlShortnerDbContext SeedData(UrlShortnerDbContext context)
        {
            context.Urls.Add(new UrlInfo { Id = 2, Key = 1000012, Url = "https://youtube.com", UrlHash = "hfRa", UserId = "9b495ebe-045f-494f-b813-17b363f4a859", Clicks = 0, Title = "Youtube" });
            context.Urls.Add(new UrlInfo { Id = 4, Key = 1000014, Url = "https://google.com", UrlHash = "hfRc", UserId = "9b495ebe-045f-494f-b813-17b363f4a859", Clicks = 1, Title = "Gmail" });
            context.SaveChanges();
            return context;
        }
    }
}
