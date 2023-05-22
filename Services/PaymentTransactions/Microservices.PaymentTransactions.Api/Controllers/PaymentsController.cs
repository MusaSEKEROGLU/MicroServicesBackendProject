using Microservices.SharedLibrary.ControllerBases;
using Microservices.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.PaymentTransactions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            return CreatedAtActionResultInstance(Response<NoContent>.Success(200));
        }
    }
}
