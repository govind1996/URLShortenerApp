using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using URLShortenerApi.Helpers;
using URLShortner.Dtos;
using URLShortner.Mapper;

namespace URLShortner.Code
{
    public class ShortenerService : IShortenerService
    {
        
        private readonly UrlShortnerDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IURLHelper _helper;
        public ShortenerService(UrlShortnerDbContext dbContext, IConfiguration config, IHttpClientFactory clientFactory, IURLHelper helper)
        {
            _dbContext = dbContext;
            _config = config;
            _clientFactory = clientFactory;
            _helper = helper;
        }
        public async Task<OrignalUrlResponse> OrignalURL(string Url)
        {
            var Key = await _helper.Decode(Url);
            UrlInfo urlInfo = new UrlInfo();
            try
            {
                urlInfo = await _dbContext.Urls.Where(x => x.Key == Key).FirstOrDefaultAsync();
                if (urlInfo == null)
                {
                    //TODO Add to error list
                    throw new ApiException("Invalid Input");
                }
                urlInfo.Clicks += 1;
                urlInfo.LastClicked = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new OrignalUrlResponse
                {
                    Message = ex.Message
                };
            }
            
            var response = UrlMapper.MapUrlInfoToOrignalUrlResponse(urlInfo);
            return response;
        }

        public async Task<ShortenUrlResponse> ShortenURL(UrlInfo input)
        {
            int Id = await SaveToDbAndGetKey(input);
            input.Key = Id + 1000000;
            input.UrlHash = await _helper.Encode(input.Key);
            input.Title = await _helper.GetTitle(input.Url);
            //TODO research about date time
            input.CreatedAt = DateTime.Now;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new ShortenUrlResponse
                {
                    Message = "Unable to save to Db"
                };
            }
            var response = UrlMapper.MapUrlInfoToShortenUrlResponse(input);
            return response;
        }
        public async Task<Urls> GetUrlListById(string UserId)
        {
            Urls response = new Urls();
            try
            {
                List<UrlInfo> Urls = await _dbContext.Urls.Where(x => x.UserId == UserId).ToListAsync();
                List<Url> UrlList = new List<Url>();
                string BaseUrl = _config["BaseUrl"];
                foreach (UrlInfo url in Urls)
                {
                    Url UrlListItem = new Url();
                    UrlListItem.Id = url.Id;
                    UrlListItem.OrignalUrl = url.Url;
                    //TODO add base url to URL hash
                    UrlListItem.ShortUrl = BaseUrl+url.UrlHash;
                    UrlListItem.CreatedAt = url.CreatedAt;
                    UrlListItem.Clicks = url.Clicks;
                    UrlListItem.LastClicked = url.LastClicked;
                    UrlListItem.Title = url.Title;
                    UrlList.Add(UrlListItem);
                }
                response.UrlList = UrlList;
            }
            catch (Exception)
            {

                response.Error = "Unable to fetch";
            }
            return response;
        }
        public async Task<DeleteUrlResponse> DeleteUrlById(DeleteUrlInput input)
        {
            UrlInfo urlInfo = new UrlInfo();
            DeleteUrlResponse response = new DeleteUrlResponse();
            try
            {
                urlInfo = await _dbContext.Urls.Where(x => x.Id == input.UrlId && x.UserId == input.UserId).FirstOrDefaultAsync();
                if (urlInfo == null)
                {
                    //TODO Add to error list
                    //throw new ApiException("Invallid Input");
                    response.Message = "No url found to delete";
                    return response;
                }
                else
                {
                    _dbContext.Remove(urlInfo);
                    await _dbContext.SaveChangesAsync();
                }
                
            }
            catch (Exception)
            {
                response.Message = "Failed to fetch from DB";
                return response;
            }

            response.Message = "Delete Success";
            return response;
        }
        private async Task<int> SaveToDbAndGetKey(UrlInfo input)
        {
            try
            {
                _dbContext.Urls.Add(input);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                
            }
            return input.Id;

        }
        
    }
}
