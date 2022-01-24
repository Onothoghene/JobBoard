using AutoMapper;
using JobBoard.Logic.Interfaces;
using JobBoard.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers
{
    public class UserController : BaseController
    {
        readonly IHttpContextAccessor _httpContext;
        readonly IUser _user;
        readonly IMapper _mapper;

        public UserController(IHttpContextAccessor httpContext, IUser user, IMapper mapper) : base(httpContext)
        {
            _httpContext = httpContext;
            _user = user;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            var data = GetUserById(UserId);
            return View(data);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public UserModel GetUserById(int Id)
        {
            var data = _user.GetById(Id);
            var response = _mapper.Map<UserModel>(data);
            return response;
        }

    }
}
