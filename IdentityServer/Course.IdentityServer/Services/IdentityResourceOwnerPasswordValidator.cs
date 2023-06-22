using Course.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(context.UserName);
            if (user != null)
            {
                bool chechpassword = await _userManager.CheckPasswordAsync(user, context.Password);
                if (chechpassword)
                {
                    context.Result = new GrantValidationResult(user.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
                    return;
                }
            }
            context.Result.CustomResponse = new Dictionary<string, object>() { { "errors", new List<string>() { "Email veya şifre hatalı" } } };
        }
    }
}
