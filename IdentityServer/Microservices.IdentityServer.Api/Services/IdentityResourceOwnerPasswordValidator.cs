using IdentityModel;
using IdentityServer4.Validation;
using Microservices.IdentityServer.Api.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.IdentityServer.Api.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            //Kulannıcı Email kontrolü
            var existUser = await _userManager.FindByEmailAsync(context.UserName);
            if (existUser == null)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors",new List<string> { "Email veya şifre hatalı" });
                context.Result.CustomResponse = errors;
                return;
            }
            //Kulannıcı Password kontrolü
            var passwordCheck = await _userManager.CheckPasswordAsync(existUser,context.Password);
            if (passwordCheck == false)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Email veya şifre hatalı" });
                context.Result.CustomResponse = errors;
                return;
            }
            //Email-Passord Ok
            context.Result = new GrantValidationResult(existUser.Id.ToString(),
                OidcConstants.AuthenticationMethods.Password);
        }
    }
}
