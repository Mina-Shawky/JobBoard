using JobBoard.DTOs;
using JobBoard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace JobBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountsController(UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAccount(RegisterDTO register)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if(register.Role != "Recruiter" && register.Role != "User")
                return BadRequest("Invalid role. Role must be either 'Admin' or 'User'.");
            ApplicationUser user = new ApplicationUser
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                UserName = register.UserName,
                
            };
            IdentityResult result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            var roleResult = await _userManager.AddToRoleAsync(user, register.Role);
            if (!roleResult.Succeeded)
                return BadRequest(result.Errors);
            return Ok("User created successfully");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAccount([FromBody] LoginDTO login)
        {
            if (!ModelState.IsValid)
                return Unauthorized();
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null)
                return Unauthorized();
            var found = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!found)
                return Unauthorized();
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:scretKey"])), SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwt = new JwtSecurityToken
            (
             issuer: _configuration["JWT:issuer"],
             audience: _configuration["JWT:audience"],
             claims: claims,
             expires:DateTime.Now.AddHours(1),
             signingCredentials: credentials
            );
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(jwt),
                expiration = jwt.ValidTo
            });

        }
        [HttpPost("CreateAdmin")]
        public async Task<IActionResult> CreateAdmin(CreateAdminDTO register)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            ApplicationUser user = new ApplicationUser
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                UserName = register.UserName,

            };
            IdentityResult result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            var roleResult = await _userManager.AddToRoleAsync(user,"Admin");
            if (!roleResult.Succeeded)
                return BadRequest(result.Errors);
            return Ok("User created successfully");
        }
    }
}
