using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microservices.SharedLibrary.Services
{
    public class SharedIdentityService : ISharedIdentityService
    {
        //Token(5li) içerisinden kullanıcını Id'sini alacak : sub

        private IHttpContextAccessor _httpContextAccessor;
        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;
            
    }
}
