using JobBoard.Enum;
using JobBoard.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JobBoard.Controllers
{
    //[Route("{controler}")]
    public class RoleController : Controller
    {
        private readonly IRoles _roles;
        public RoleController(IRoles roles)
        {
            _roles = roles;
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> CreateRole()
        {
            try
            {
                var response = await _roles.Create();
                if (response == RequestStatus.Success)
                {
                    TempData["Success"] = "Great Job";
                }
                else
                {
                    TempData["Error"] = "Awww snap";
                }
                return View("Create");
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
