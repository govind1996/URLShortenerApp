using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using shortify.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

namespace shortify.Code
{
    public class ShortifyService : IShortifyService
    {
        private readonly IHttpClientFactory _clientFactory;
        UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ShortifyService(IHttpClientFactory clientFactory, UserManager<IdentityUser> userManager,IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ShortenUrlResponse> ShortenUrl(ShortenUrlInput input)
        {
            var UserId = GetCurrentUserId();
            ShortenUrlPayload payload = new ShortenUrlPayload()
            {
                UserId = UserId,
                Url = input.Url,
            };
            var json = JsonConvert.SerializeObject(payload);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var uri = "https://localhost:44364/api/Links/Shorten";
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = httpContent;
            //adding auth token in header
            var authToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            request.Headers.Add("Authorization", authToken);
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            //TODO add customised error responses
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ShortenUrlResponse()
                {
                    Url = new Url(),
                    Message = "Server Error"
                };
            }
            string content = await response.Content.ReadAsStringAsync();
            var ShortenUrl = JsonConvert.DeserializeObject<ShortenUrlResponse>(content);
            return ShortenUrl;
        }

        public async Task<OrignalUrlResponse> GetOrignalUrl(string Url)
        {
            OrignalUrlPayload payload = new OrignalUrlPayload()
            {
                Url = Url
            };
            var json = JsonConvert.SerializeObject(payload);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:44364/api/Links/Orignal");
            request.Content = httpContent;
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            var responseStrem = await response.Content.ReadAsStringAsync();
            var link = JsonConvert.DeserializeObject<OrignalUrlResponse>(responseStrem);
            return link;
        }
        public async Task<UrlListResponse> ListOfUrlsByUserId()
        {
            var UserId = GetCurrentUserId(); 
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44364/api/Links/UrlList/"+UserId);
            //adding auth token in header
            var authToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            request.Headers.Add("Authorization",authToken);
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            
            //TODO add customised error responses

            if (response.StatusCode!=HttpStatusCode.OK)
            {
                return new UrlListResponse()
                {
                    UrlList = new List<Url>(),
                    Error = "Server Error"
                };
            }
            var responseStrem = await response.Content.ReadAsStringAsync();
            var UrlListResponse = JsonConvert.DeserializeObject<UrlListResponse>(responseStrem);
            return UrlListResponse;
        }
        public async Task<DeleteUrlResponse> DeleteUrlById(int UrlId)
        {
            var UserId = GetCurrentUserId();
            var payload = new DeleteUrlPayload()
            {
                UserId = UserId,
                UrlId = UrlId
            };
            var json = JsonConvert.SerializeObject(payload);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Delete, "https://localhost:44364/api/Links/DeleteUrl/");
            request.Content = httpContent;

            //adding auth token in header
            var authToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            request.Headers.Add("Authorization", authToken);

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            //TODO add customised error responses
            if (response.StatusCode!=HttpStatusCode.OK)
            {
                return new DeleteUrlResponse
                {
                    Message = "server error"
                };
            }
            var responseStrem = await response.Content.ReadAsStringAsync();
            var DeleteResponse = JsonConvert.DeserializeObject<DeleteUrlResponse>(responseStrem);
            return DeleteResponse;
        }
        private string GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }
    }
}

