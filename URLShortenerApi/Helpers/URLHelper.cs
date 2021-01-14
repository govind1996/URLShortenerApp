using DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace URLShortenerApi.Helpers
{

    public class URLHelper : IURLHelper
    {
        private readonly UrlShortnerDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;
        public URLHelper(UrlShortnerDbContext dbContext, IConfiguration config, IHttpClientFactory clientFactory)
        {
            _dbContext = dbContext;
            _config = config;
            _clientFactory = clientFactory;
        }
        /// <summary>
        /// Encodes an integer to unique string
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public async Task<string> Encode(int Key)
        {
            
            string IntToChar = _config.GetValue<string>("IntToChar");
            StringBuilder UrlHash = new StringBuilder();
            while (Key != 0)
            {
                UrlHash.Insert(0, IntToChar[Key % 52]);
                Key = Key / 52;
            }
            return UrlHash.ToString();
        }
        /// <summary>
        /// Decodes the given string
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public async Task<int> Decode(string Url)
        {
            int Key = 0;
            for (int i = 0; i < Url.Length; i++)
            {
                if (Url[i] >= 'a' && Url[i] <= 'z')
                    Key = Key * 52 + (Url[i] - 'a');
                else
                    Key = Key * 52 + (26 + Url[i] - 'A');
            }
            return Key;
        }
        /// <summary>
        /// Gets title of an Url 
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public async Task<string> GetTitle(string Url)
        {
            if (string.IsNullOrWhiteSpace(Url))
                return "No Title";
            try
            {
                var uri = new Uri(Url);
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
            catch (Exception)
            {
                return "No Title";
            }
            return "No Title";
        }
    }
}
