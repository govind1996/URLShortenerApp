using Microsoft.AspNetCore.Mvc;
using shortify.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shortify.Code
{
    public interface IShortifyService
    {
        //TODO accept by list of url by user id
        Task<UrlListResponse> ListOfUrlsByUserId();
        Task<ShortenUrlResponse> ShortenUrl(ShortenUrlInput input);
        Task<OrignalUrlResponse> GetOrignalUrl(string Url);
        Task<DeleteUrlResponse> DeleteUrlById(int UrlId);

    }
}
