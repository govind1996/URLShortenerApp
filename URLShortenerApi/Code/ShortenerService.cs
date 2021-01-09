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
using URLShortner.Dtos;
using URLShortner.Mapper;

namespace URLShortner.Code
{
    public class ShortenerService : IShortenerService
    {
        
        private readonly UrlShortnerDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;
        public ShortenerService(UrlShortnerDbContext dbContext, IConfiguration config, IHttpClientFactory clientFactory)
        {
            _dbContext = dbContext;
            _config = config;
            _clientFactory = clientFactory;
        }
        public async Task<OrignalUrlResponse> OrignalURL(string Url)
        {
            var Key = await Decode(Url);
            UrlInfo urlInfo = new UrlInfo();
            try
            {
                urlInfo = await _dbContext.Urls.Where(x => x.Key == Key).FirstOrDefaultAsync();
                if (urlInfo == null)
                {
                    //TODO Add to error list
                    throw new ApiException("Invallid Input");
                }
                urlInfo.Clicks += 1;
                urlInfo.LastClicked = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            
            var response = UrlMapper.MapUrlInfoToOrignalUrlResponse(urlInfo);
            return response;
        }

        public async Task<ShortenUrlResponse> ShortenURL(UrlInfo input)
        {
            int Id = await SaveToDbAndGetKey(input);
            input.Key = Id + 1000000;
            input.UrlHash = await Encode(input.Key);
            input.Title = await GetTitle(input.Url);
            //TODO research about date time
            input.CreatedAt = DateTime.Now;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
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
            catch (Exception ex)
            {

                throw ex; 
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
            catch (Exception ex)
            {
                response.Message = "failed to fetch from DB";
                return response;
            }

            response.Message = "Delete Success";
            return response;
        }
        /// <summary>
        /// Encodes an integer to unique string
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        private async Task<string> Encode(int Key)
        {
            string IntToChar = _config["IntToChar"];
            StringBuilder UrlHash = new StringBuilder();
            while(Key!=0)
            {
                UrlHash.Insert(0,IntToChar[Key % 52]);
                Key = Key / 52;
            }
            return UrlHash.ToString();
        }
        private async Task<int> Decode(string Url)
        {
            int Key = 0;
            for(int i=0;i<Url.Length;i++)
            {
                if (Url[i] >= 'a' && Url[i] <= 'z')
                    Key = Key * 52 + (Url[i] - 'a');
                else
                    Key = Key * 52 + (26 + Url[i] - 'A');
            }
            return Key;
        }
        private async Task<int> SaveToDbAndGetKey(UrlInfo input)
        {
            try
            {
                _dbContext.Urls.Add(input);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw(ex);
            }
            return input.Id;

        }
        private async Task<string> GetTitle(string url)
        {
            try
            {
                var uri = new Uri(url);
                var baseUri = uri.GetLeftPart(System.UriPartial.Authority).ToString();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://textance.herokuapp.com/title/" + baseUri);
                var client = _clientFactory.CreateClient();
                var response = await client.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseStream = await response.Content.ReadAsStringAsync();
                    return responseStream;
                }
            }
            catch (Exception ex)
            {

                return "No Title";
            }
            return "No Title";
        }
    }
}
