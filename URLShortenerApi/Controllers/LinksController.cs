using AutoMapper;
using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using System.Threading.Tasks;
using URLShortner.Code;
using URLShortner.Dtos;
using URLShortner.Exceptions;

namespace URLShortner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LinksController : ControllerBase
    {
        private readonly IShortenerService _service;
        private readonly IMapper _mapper;
        public LinksController(IShortenerService service,IMapper mapper, UrlShortnerDbContext dbContext)
        {
            _service = service;
            _mapper = mapper;
        }
        /// <summary>
        /// Converts An Url to Shorten Url and returns Shorten Url 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>ShortenUrl</returns>
        [HttpPost("Shorten")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ShortenUrlResponse))]
        public async Task<IActionResult> GetShortenUrl(ShortenUrlInput input)
        {
            //add fluent validation
            if(!ModelState.IsValid)
            {
                return BadRequest(new ApiExceptionResponse("Invalid Input"));
            }
            var response = await _service.ShortenURL(_mapper.Map<ShortenUrlInput, UrlInfo>(input));
            return Ok(response);
        }
        /// <summary>
        /// gets orignal url
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        [HttpGet("{Url}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(OrignalUrlResponse))]
        public async Task<IActionResult> GetOrignalUrl(string Url)
        {
            if (string.IsNullOrWhiteSpace(Url))
                return BadRequest(new ApiExceptionResponse("Invalid Url"));
            var response = await _service.OrignalURL(Url);
            return Ok(response);
        }
        
        /// <summary>
        /// converts the shortened url to orignal url and return orignal url
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Orignal Ur</returns>
        [HttpPut("Orignal")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(OrignalUrlResponse))]
        [AllowAnonymous]
        public async Task<IActionResult> GetOrignalUrl(OrignalUrlInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Url))
                return BadRequest(new ApiExceptionResponse("Invalid Url"));
            var response = await _service.OrignalURL(input.Url);
            return Ok(response);
        }
        /// <summary>
        /// gets list of all links of a particular user
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>Orignal Ur</returns>
        [HttpGet("UrlList/{UserId}")]
        [SwaggerResponse((int)HttpStatusCode.OK,Type= typeof(Urls))]
        public async Task<IActionResult> GetAllUrl(string UserId)
        {
            var response = await _service.GetUrlListById(UserId);
            return Ok(response);
        }
        /// <summary>
        /// delete a url of particular urlId and userId
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Orignal Ur</returns>
        [HttpDelete("DeleteUrl")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DeleteUrlResponse))]
        public async Task<IActionResult> DeleteUrl(DeleteUrlInput input)
        {
            var response = await _service.DeleteUrlById(input);
            return Ok(response);
        }
    }
}
