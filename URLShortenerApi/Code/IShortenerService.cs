using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLShortner.Dtos;

namespace URLShortner.Code
{
    public interface IShortenerService
    {
        Task<ShortenUrlResponse> ShortenURL(UrlInfo input);
        Task<OrignalUrlResponse> OrignalURL(string url);
        Task<Urls> GetUrlListById(String UserId);
        Task<DeleteUrlResponse> DeleteUrlById(DeleteUrlInput input);
    }
}
