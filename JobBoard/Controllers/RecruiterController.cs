using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers
{
    public class RecruiterController : BaseController
    {
        readonly IHttpContextAccessor _httpContext;
        public RecruiterController(IHttpContextAccessor httpContext) : base(httpContext)
        {
            _httpContext = httpContext;
        }

        [Route("recruiter/home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
