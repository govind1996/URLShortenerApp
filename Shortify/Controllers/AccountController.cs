using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using shortify.Dtos;
using shortify.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace shortify.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccountController : ControllerBase
    {
        SignInManager<IdentityUser> _signInManager;
        UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> register(RegisterViewModel input)
        {
            var response = new RegisterResponse();
            //TODO validation
            //List<string> errors= new List<string>();
            //if (input.Email == "" || input.Email == null)
            //    errors.Add("Email cannot be empty");
            //if (input.Username == "" || input.Username == null)
            //    errors.Add("Email cannot be empty");
            //if (input.Email == "" || input.Email == null)
            //    errors.Add("Email cannot be empty");
            //if (input.Email == "" || input.Email == null)
            //    errors.Add("Email cannot be empty");
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = input.Username,
                    Email = input.Email
                };
                var UserResult = await _userManager.CreateAsync(user, input.Password);
                if(UserResult.Succeeded)
                {
                    var RoleResult = await _userManager.AddToRoleAsync(user, "User");
                    if (RoleResult.Succeeded)
                    {
                        response.Email = user.Email;
                        response.Username = user.UserName;
                        return Ok(response);
                    }
                }
                else
                {
                    response.exception = new ApiException();
                    response.exception.Errors = new List<string>();
                    foreach (var error in UserResult.Errors)
                    {
                        response.exception.Errors.Add(error.Description);
                    }
                    return Ok(response);
                }
            }
            response.exception = new ApiException();
            response.exception.Errors = new List<string>();
            foreach(var state in ModelState.Values)
            {
                foreach(var error in state.Errors)
                    response.exception.Errors.Add(error.ErrorMessage);
            }
            return Ok(response);
        }
        [HttpPost("signin")]
        public async Task<IActionResult> signin(SignInViewModel input)
        {
            var response = new LoginResponse();
            if (ModelState.IsValid)
            {
                var SignInResult = await _signInManager.PasswordSignInAsync(input.Username, input.Password, false, false);
                if (SignInResult.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(input.Username);
                    var roles = await _userManager.GetRolesAsync(user);
                    var UserId = user.Id;
         
                    //JWT authentication

                    //Read from config
                    var SecretKey = _configuration["SecretKey"];
                    //create SignIn key using the secret key which will be used to create signature
                    var SignInkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
                    //use SignIn key to generate JWT signature
                    var Credentials = new SigningCredentials(SignInkey, SecurityAlgorithms.HmacSha256);
                    //create claims
                    IdentityOptions identityOptions = new IdentityOptions();
                    var Claims = new Claim[]
                    {
                        new Claim(identityOptions.ClaimsIdentity.UserIdClaimType,user.Id),
                        new Claim(identityOptions.ClaimsIdentity.UserNameClaimType, user.UserName),
                        new Claim(identityOptions.ClaimsIdentity.RoleClaimType, roles[0])
                    };
                    //TODO change expires and user refresh tokens
                    var jwt = new JwtSecurityToken(signingCredentials: Credentials, expires: DateTime.Now.AddSeconds(3600), claims: Claims);
                    var ExpiresAt = 3600;
                    var JwtToken = new JwtSecurityTokenHandler().WriteToken(jwt);
                    response.user = new User()
                    {
                        UserId = UserId,
                        ExpiresAt = ExpiresAt,
                        JwtToken = JwtToken
                    };
                    return Ok(response);
                }
            }
            //TODO set exception using throw
            response.Exception = new LoginException()
            {
                Message = "Invalid Username or Password"
            };
            return Ok(response);
        }
        [HttpPost("signout")]
        public async Task<IActionResult> signout()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }

    }
}
