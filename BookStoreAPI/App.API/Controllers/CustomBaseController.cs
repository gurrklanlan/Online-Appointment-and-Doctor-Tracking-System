using App.Application;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(ServiceResult<T> serviceResult)
        {
            if (serviceResult.StatusCode == HttpStatusCode.NoContent)
            {
                return NoContent();
            }
            if (serviceResult.StatusCode == HttpStatusCode.NoContent)
            {
                return Created(serviceResult.UrlAsCreated, serviceResult);
            }
            return new ObjectResult(serviceResult) { StatusCode = serviceResult.StatusCode.GetHashCode() };
        }
        [NonAction]
        public IActionResult CreateActionResult(ServiceResult serviceResult)
        {
            if (serviceResult.StatusCode == HttpStatusCode.NoContent)
            {
                return new ObjectResult(null) { StatusCode = serviceResult.StatusCode.GetHashCode() };
            }
            return new ObjectResult(serviceResult) { StatusCode = serviceResult.StatusCode.GetHashCode() };
        }
    }
}
