using Microservices.IdentityServer.Api.Dtos;
using Microservices.IdentityServer.Api.Models;
using Microservices.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace Microservices.IdentityServer.Api.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost] //Kullanıcı Oluştur
        public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        {
            var user = new ApplicationUser
            {
                UserName = signUpDto.UserName,
                Email = signUpDto.Email,
                City = signUpDto.City
            };
            var result = await _userManager.CreateAsync(user,signUpDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(Response<NoContent>
                    .Fail(result.Errors.Select(x=>x.Description).ToList(),404));
            }
            return NoContent();
        }

        [HttpGet] //Kullanıcı Getir
        public async Task<IActionResult> GetUser()
        {
            //Sub
            var userIdClaim = User.Claims.FirstOrDefault(x=>x.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null) return BadRequest();
            //Value
            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user == null) return BadRequest();

            //Get User Claims
            return Ok(new{Id = user.Id, UserName = user.UserName, Email = user.Email, City = user.City});

        }
    }
}
