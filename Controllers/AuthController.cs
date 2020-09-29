namespace Authorization.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Authorization.Models;
    using Authorization.Services;
    using System.Linq;
    using Authorization.ViewModels;
    using System.Net;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly ITokenBuilder _tokenBuilder;
        private readonly IPasswordService _passwordService;
        public AuthController(ICosmosDbService cosmosDbService, IPasswordService passwordService, ITokenBuilder tokenBuilder)
        {

            _cosmosDbService = cosmosDbService;
            _passwordService = passwordService;
            _tokenBuilder = tokenBuilder;
        }


        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUp([Bind("Email,Password")] UserSignUpVM userVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = userVM.Email,
                        Password = _passwordService.HashPassword(userVM.Password)
                    };
                    await _cosmosDbService.AddUserAsync(user);
                    var token = _tokenBuilder.BuildToken(user);
                    return Ok(token);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return BadRequest("Invalid user model");
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult> SignIn([Bind("Email,Password")] ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string email = user.Email;
                    var appUser = await _cosmosDbService.GetUsersAsync($"SELECT * FROM Users u WHERE u.email = \"{user.Email}\"");
                    if (appUser.FirstOrDefault() != null && _passwordService.VerifyPassword(appUser.FirstOrDefault().Password, user.Password))
                    {
                        var token = _tokenBuilder.BuildToken(appUser.FirstOrDefault());
                        return Ok(token);
                    }
                    else
                    {
                        return Unauthorized();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    return Ok(e.Message);
                }
            }
            return BadRequest("Invalid user model");
        }

        [HttpPost("verify")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> verify([Bind("Email,Password")] ApplicationUser user)
        {
            var appUserEmail = User
                .Claims
                .First(c => c.Type == "Email").Value;

            if (appUserEmail == null)
            {
                return Unauthorized();
            }
            var appUserExists = await _cosmosDbService.GetUsersAsync($"SELECT * FROM Users u WHERE u.email = \"{appUserEmail}\"");

            if (appUserExists == null)
            {
                return Unauthorized();
            }
            return NoContent();
        }



    }
}