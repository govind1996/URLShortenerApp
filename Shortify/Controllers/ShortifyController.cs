using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using shortify.Code;
using shortify.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace shortify.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ShortifyController : ControllerBase
    {
        
        private readonly IShortifyService _service;
        public ShortifyController(IShortifyService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        [HttpGet("{Url}")]
        public async Task<IActionResult> Get(string Url)
        {
            
            var response = await _service.GetOrignalUrl(Url);
            if (response.Message!=null)
            {
                return Redirect("https://localhost:44322/app/notfound");
            }
            return Redirect(response.Url.OrignalUrl);
        }
        [HttpGet("Urls")]
        public async Task<UrlListResponse> GetAllUrls()
        {
            return await _service.ListOfUrlsByUserId();
        }
        [HttpPost("ShortenUrl")]
        public async Task<ShortenUrlResponse> ShortenUrl(ShortenUrlInput input)
        {
            //TODO model validation and remove IActionresult
            return await _service.ShortenUrl(input);
        }
        [HttpDelete("DeleteUrl")]
        public async Task<DeleteUrlResponse> DeleteUrlById(DeleteUrlInput input)
        {
            //TODO model validation and remove IActionresult
            return await _service.DeleteUrlById(input.UrlId);
        }

    }
}
