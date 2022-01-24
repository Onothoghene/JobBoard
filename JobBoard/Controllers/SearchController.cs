using AutoMapper;
using JobBoard.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers
{
    public class SearchController : BaseController
    {
        readonly ISearch _search;
        readonly IMapper _mapper;
        readonly IHttpContextAccessor _httpContext;

        public SearchController(ISearch search, IMapper mapper, IHttpContextAccessor httpContext): base(httpContext)
        {
            _search = search;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        [Route("seacrh-result")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
