using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.SharedLibrary.Services
{
    public interface ISharedIdentityService
    {
        public string GetUserId { get;}
    }
}
