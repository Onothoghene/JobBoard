using AutoMapper;
using JobBoard.DTO.EdiModel;
using JobBoard.Enum;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( UserModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "Please Input the Required Information(s)";
                    return View("Index", model);
                }
                var resp = _mapper.Map<UserEM>(model);
                var response = _user.Edit(resp);
                if (response == RequestStatus.Success)
                {
                    TempData["Success"] = "Updated Successfully";
                    return View("Index", model);
                }
                
                if (response == RequestStatus.NoEntryFound)
                {
                    TempData["Error"] = "No Record Found For this user";
                    return View("Index", model);
                }

                return View("Index", model);
            }
            catch
            {
                return View("Index");
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
