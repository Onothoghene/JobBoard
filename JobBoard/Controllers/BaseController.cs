using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers
{
    public class BaseController : Controller
    {
        public int UserId { get; }
        public string Role { get; }

        private readonly IHttpContextAccessor _httpContext;
        public BaseController(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;

            if (_httpContext.HttpContext.User.Claims.ToList().Find(r => r.Type == "UserId") != null)
            {
                string userId = _httpContext.HttpContext.User.Claims.ToList().Find(r => r.Type == "UserId").Value;
                UserId = Convert.ToInt32(userId);
            }

            //UserId = Convert.ToInt32(_httpContext.HttpContext?.User?.FindFirstValue("UserId"));
            //Role = _httpContext.HttpContext?.User?.FindFirstValue("UserRole");

        }

    }
}
