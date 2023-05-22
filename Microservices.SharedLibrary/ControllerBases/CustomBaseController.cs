using Microservices.SharedLibrary.ControllerBases;
using Microservices.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Microservices.SharedLibrary.ControllerBases
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreatedAtActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
