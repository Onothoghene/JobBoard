using AutoMapper;
using JobBoard.DTO;
using JobBoard.Enum;
using JobBoard.Handlers;
using JobBoard.Logic.Interfaces;
using JobBoard.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace JobBoard.Controllers
{
    public class AccountController : BaseController
    {
        readonly IAccount _account;
        readonly IMapper _mapper;
        readonly IHttpContextAccessor _httpContext;
        public AccountController(IAccount account, IMapper mapper, IHttpContextAccessor httpContext) : base(httpContext)
        {
            _account = account;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        [Route("authentication")]
        public IActionResult Authentication()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "Please ensure you fill in the required Information";
                    return View("Authentication", model);
                }
                var data = _mapper.Map<LoginModel>(model);
                var response = await _account.LoginUser(data);
                if (response != null)
                {
                    TempData["Error"] = "Authenticated Successfully";
                    return RedirectToAction("Index", "Home");
                }
                TempData["Error"] = "Please ensure you supplied the correct credentials or register if you don't have an account";
                return View("Authentication", model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error occured, please try again!!!...";
                return View("Authentication", model);
                throw ex;
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> LoginLite(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "Please ensure you fill in the required Information";
                    return View("Authentication", model);
                }
                var data = _mapper.Map<LoginModel>(model);
                var response = await _account.LoginUserLite(data);
                switch (response.Item1)
                {
                    case RequestStatus.Success:
                        TempData["Sucess"] = "Authenticated Successfully";
                        return RedirectToAction("Index", "Home");
                    
                    case RequestStatus.FatalError:
                        TempData["Error"] = "Error Occurred...";
                        return View("Authentication", model);

                    case RequestStatus.NoEntryFound:
                        TempData["Error"] = $"No Account associated with {model.Email}";
                        return View("Authentication", model);

                    default:
                        TempData["Error"] = "Invalid Login Attempt";
                        return View("Authentication", model);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error occured, please try again!!!...";
                return View("Authentication", model);
                throw ex;
            }
        }

        [Route("register/job-seeker")]
        public IActionResult RegsiterJobSeeker()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateJobSeeker(RegistrationViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "Please ensure you fill in the required Information";
                    return View("RegsiterJobSeeker", model);
                }
                string uploadPath = Environment.CurrentDirectory + "\\uploads";
                bool dirExists = Directory.Exists(uploadPath);
                if (!dirExists)
                    Directory.CreateDirectory(uploadPath);

                var fileInput = new FileViewModel();
                if (model.Files != null)
                {
                    var file = model.Files;
                    if (file != null && file.Length > 0)
                    {
                        var fileExt = FileHandler.GetFileExtensionFromFileName(file.FileName);
                        var filename = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                        var uploadPathWithfileName = Path.Combine(uploadPath, filename);

                        using (var fileStream = new FileStream(uploadPathWithfileName, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            fileInput.FileUniqueName = filename;
                            fileInput.FileExt = fileExt;
                            fileInput.IsCV = true;
                            fileInput.IsCompanyImage = false;
                            fileInput.IsProfileImage = false;
                        }
                        model.CV = fileInput;
                    }
                }

                var data = _mapper.Map<RegistrationModel>(model);
                var response = await _account.RegisterJobSeeker(data);

                if (response == RequestStatus.EntryAlreadyExist)
                {
                    TempData["Error"] = "The Email used has been taken please use another one";
                    return View("RegsiterJobSeeker", model);
                }
                else if (response == RequestStatus.FatalError)
                {
                    TempData["Error"] = "Error Occurred while trying to create";
                    return View("RegsiterJobSeeker", model);
                }
                else if (response == RequestStatus.BarredRequest)
                {
                    TempData["Error"] = "Error Occurred while trying to create";
                    return View("RegsiterJobSeeker", model);
                }
                else if (response == RequestStatus.Success)
                {
                    TempData["Success"] = "Registration Succesful, Please Login to get started";
                    return RedirectToAction("Authentication");
                }
                return View("RegsiterJobSeeker", model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error occured, please try again!!!...";
                return View("RegsiterJobSeeker", model);
                throw ex; 
            }
        }

        [Route("register/recruiter")]
        public IActionResult RegsiterRecruiter()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecruiter(RegistrationViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "Please ensure you fill in the required Information";
                    return View("RegsiterRecruiter", model);
                }
                var data = _mapper.Map<RegistrationModel>(model);
                var response = await _account.RegisterRecruiter(data);

                if (response == RequestStatus.EntryAlreadyExist)
                {
                    TempData["Error"] = "The Email used has been taken please use another one";
                    return View("RegsiterRecruiter", model);
                }
                else if (response == RequestStatus.FatalError)
                {
                    TempData["Error"] = "Error Occurred while trying to create";
                    return View("RegsiterRecruiter", model);
                }
                else if (response == RequestStatus.BarredRequest)
                {
                    TempData["Error"] = "Error Occurred while trying to create";
                    return View("RegsiterRecruiter", model);
                }
                else if (response == RequestStatus.Success)
                {
                    TempData["Success"] = "Registration Succesful, Please Login to get started";
                    return RedirectToAction("Authentication");
                }
                return View("RegsiterRecruiter", model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error occured, please try again!!!...";
                return View("RegisterJobSeeker", model);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var response = await _account.Logout(UserId);
                if (response == RequestStatus.NoEntryFound)
                {
                    TempData["Error"] = "How the hell did you login if you weren't found";
                    return View("Authentication");
                }
                
                if (response == RequestStatus.Success)
                {
                    TempData["Success"] = "Logout successful";
                    return View("Signout");
                }
            }
            catch (Exception)
            {
                TempData["Success"] = "Hmmm something fishy happened";
                return View("Authentication");
                throw;
            }    
            return View("Authentication");
        }

        public IActionResult Signout()
        {
            TempData["Success"] = "Please do Come back";
            return View();
        }

        [Route("user/manage/account")]
        public IActionResult Manage()
        {
            return View();
        }

        [Route("accaount/preferences")]
        public IActionResult UserSettings()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult ChangePasword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteAccount()
        {
            return View();
        }

    }
}
