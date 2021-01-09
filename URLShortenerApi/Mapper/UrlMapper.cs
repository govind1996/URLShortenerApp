using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLShortner.Dtos;

namespace URLShortner.Mapper
{
    internal static class UrlMapper
    {
        internal static OrignalUrlResponse MapUrlInfoToOrignalUrlResponse(UrlInfo input)
        {
            if (input == null)
                return new OrignalUrlResponse();

            return new OrignalUrlResponse
            {
                Url = new Url
                {
                    Id = input.Id,
                    ShortUrl = input.UrlHash,
                    OrignalUrl = input.Url,
                    CreatedAt = input.CreatedAt,
                    Clicks = input.Clicks,
                    LastClicked = input.LastClicked,
                    Title = input.Title
                }
            };
        }
        internal static ShortenUrlResponse MapUrlInfoToShortenUrlResponse(UrlInfo input)
        {
            if (input == null)
                return new ShortenUrlResponse();
            return new ShortenUrlResponse
            {
                Url = new Url
                {
                    Id = input.Id,
                    ShortUrl = input.UrlHash,
                    OrignalUrl = input.Url,
                    CreatedAt = input.CreatedAt,
                    Clicks = input.Clicks,
                    LastClicked = input.LastClicked,
                    Title = input.Title
                }
            };
        }
    }
}
