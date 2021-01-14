using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public interface IUrlShortnerDbContext 
    {
        DbSet<UrlInfo> Urls { get; set; }
    }
}