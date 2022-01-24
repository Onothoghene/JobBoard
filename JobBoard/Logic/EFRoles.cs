using AutoMapper;
using JobBoard.DTO;
using JobBoard.Enum;
using JobBoard.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Logic.Interfaces
{
    public class EFRoles : IRoles
    {
        private RoleManager<IdentityRole> roleManager;
        readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public EFRoles(RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userManager)
        {
            roleManager = roleMgr;
            _userManager = userManager;
        }

        public async Task<RequestStatus> Create()
        {
            IdentityResult result = new IdentityResult();
            var Roles = new List<string>
            {
                RoleEnum.Administrator.ToString(),
                RoleEnum.Recruiter.ToString(),
                RoleEnum.JobSeeker.ToString()
            };
            if (Roles != null && Roles.Count() > 0)
            {
                foreach (var item in Roles)
                {
                    var existingRole = roleManager.FindByNameAsync(item);
                    if (existingRole != null)
                    {
                        continue;
                    }
                    result = await roleManager.CreateAsync(new IdentityRole(item));
                }
            }
            if (result.Succeeded)
                return RequestStatus.Success;
            else
                return RequestStatus.FatalError;
        }

    }
}
