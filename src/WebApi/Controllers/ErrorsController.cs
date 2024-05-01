using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Index()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (feature != null)
            {
                var (statusCode, title) = ExceptionMapper.Map(feature.Error);
                return Problem(statusCode: statusCode, title: title);
            }
            return Problem();
        }
    }
}
