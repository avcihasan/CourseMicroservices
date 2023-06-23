using Course.Shared.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Course.Shared.Services.Concretes
{
    public class SharedIdentityService : ISharedIdentityService
    {
        readonly IHttpContextAccessor _httpContextAccessor;

        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;
    }
}
