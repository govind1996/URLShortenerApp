using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortenerTests.Utils
{
    class TestDbContext<T> where T: DbContext
    {
        public static T GetContext(T context)
        {
            DbContextOptions<T> options;
            var builder = new DbContextOptionsBuilder<T>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            context = (T) Activator.CreateInstance(typeof(T),options);
            context.SaveChanges();
            return context;
        }
    }
}
