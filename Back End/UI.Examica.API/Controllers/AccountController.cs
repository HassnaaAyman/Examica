using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UI.Examica.API.Dtos;
using UI.Examica.Model.Core.Domains;

namespace UI.Examica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthDto>> Login([FromBody] LoginDto model)
        {
            var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
            if (appUser == null) return BadRequest();
            var result = await _signInManager.PasswordSignInAsync(appUser.UserName, model.Password, false, false);
            if (result.Succeeded)
            {
                AuthDto auth = new AuthDto
                {
                    Token = GenerateJwtToken(model.Email, appUser),
                    UserId = appUser.Id
                };
                return Ok(auth);
            }
            else return BadRequest();
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<AuthDto>> Register([FromBody] RegisterDto model)
        {
            var user = new AppUser
            {
                UserName = model.Name,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                AuthDto auth = new AuthDto
                {
                    Token = GenerateJwtToken(model.Email, user),
                    UserId = user.Id
                };
                return Ok(auth);
            }
            else return BadRequest();
        }
        private string GenerateJwtToken(string email, AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}