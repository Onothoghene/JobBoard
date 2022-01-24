using AutoMapper;
using JobBoard.DTO;
using JobBoard.DTO.OutputModel;
using JobBoard.Enum;
using JobBoard.Logic.Interfaces;
using JobBoard.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;

namespace JobBoard.Logic
{
    public class EFAccount : IAccount
    {
        readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JobBoardContext _context;
        readonly IMapper _mapper;
        readonly IUser _user;

        public EFAccount(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, IUser user, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = new JobBoardContext();
            _mapper = mapper;
            _user = user;
            _roleManager = roleManager;
        }

        public async Task<RequestStatus> RegisterJobSeeker(RegistrationModel model)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var user = new ApplicationUser
                    {
                        //UserName = $"{model.Surname} {model.Firstname}",
                        UserName = model.Email,
                        Email = model.Email,
                    };
                    var existingEmail = await _userManager.FindByEmailAsync(model.Email);
                    if (existingEmail != null) return RequestStatus.EntryAlreadyExist;

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var role = await _roleManager.FindByNameAsync(RoleEnum.JobSeeker.ToString());
                        if (role == null)
                        {
                            IdentityRole identityRole = new IdentityRole
                            {
                                Name = RoleEnum.JobSeeker.ToString()
                            };
                            await _roleManager.CreateAsync(identityRole);
                        }
                        await _userManager.AddToRoleAsync(user, RoleEnum.JobSeeker.ToString());
                        var userProfile = new UserProfile
                        {
                            Firstname = model.Firstname,
                            Surname = model.LastName,
                            PhoneNumber = model.PhoneNumber,
                            Email = model.Email,
                            DateAdded = DateTime.Now,
                        };
                        await _context.UserProfile.AddAsync(userProfile);

                        int bit = _context.SaveChanges();
                        ts.Complete();

                        if (bit > 0)
                        {
                            return RequestStatus.Success;
                        }
                        else
                        {
                            await _userManager.DeleteAsync(user);
                            return RequestStatus.BarredRequest;
                        }

                    }
                    else
                    {
                        await _userManager.DeleteAsync(user);
                        return RequestStatus.FatalError;
                    }
                }
            }
            catch (Exception ex)
            {
                var existingEmail = await _userManager.FindByEmailAsync(model.Email);
                await _userManager.DeleteAsync(existingEmail);
                throw ex;
            }
        }

        public async Task<RequestStatus> RegisterRecruiter(RegistrationModel model)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var user = new ApplicationUser
                    {
                        // UserName = $"{model.Surname} {model.Firstname}",
                        UserName = model.Email,
                        Email = model.Email,
                    };
                    var existingEmail = await _userManager.FindByEmailAsync(model.Email);
                    if (existingEmail != null) return RequestStatus.EntryAlreadyExist;

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var role = await _roleManager.FindByNameAsync(RoleEnum.Recruiter.ToString());
                        if (role == null)
                        {
                            IdentityRole identityRole = new IdentityRole
                            {
                                Name = RoleEnum.Recruiter.ToString()
                            };
                            await _roleManager.CreateAsync(identityRole);
                        }
                        await _userManager.AddToRoleAsync(user, RoleEnum.Recruiter.ToString());
                        var userProfile = new UserProfile
                        {
                            Firstname = model.Firstname,
                            Surname = model.LastName,
                            PhoneNumber = model.PhoneNumber,
                            Email = model.Email,
                            DateAdded = DateTime.Now,
                        };
                        await _context.UserProfile.AddAsync(userProfile);

                        int bit = _context.SaveChanges();
                        ts.Complete();

                        if (bit > 0)
                        {
                            return RequestStatus.Success;
                        }
                        else
                        {
                            await _userManager.DeleteAsync(user);
                            return RequestStatus.BarredRequest;
                        }

                    }
                    else
                    {
                        await _userManager.DeleteAsync(user);
                        return RequestStatus.FatalError;
                    }
                }
            }
            catch (Exception ex)
            {
                var existingEmail = await _userManager.FindByEmailAsync(model.Email);
                await _userManager.DeleteAsync(existingEmail);
                return RequestStatus.FatalError;
                throw ex;
            }
        }

        public async Task<UserOM> LoginUser(LoginModel model)
        {
            try
            {
                UserOM response = new UserOM();
                var user = await Task.Run(() => _userManager.FindByEmailAsync(model.Email));
                if (user == null) return response = new UserOM();

                var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var userdetails = _user.GetByEmail(model.Email);
                    response = _mapper.Map<UserOM>(userdetails);
                    var role = await _userManager.GetRolesAsync(user);
                    response.UserRole = role.FirstOrDefault();
                    return response;
                }
                else
                {
                    return new UserOM();
                }
            }
            catch (Exception ex)
            {
                return new UserOM();
                throw ex;
            }
        }

        public async Task<(RequestStatus, UserOM)> LoginUserLite(LoginModel model)
        {
            try
            {
                UserOM response = new UserOM();
                var user = await Task.Run(() => _userManager.FindByEmailAsync(model.Email));
                if (user == null) return (RequestStatus.NoEntryFound, response = new UserOM());

                var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    await GenerateUserClaims(user);
                    var role = await _userManager.GetRolesAsync(user);
                    var userdetails =  await Task.Run(()=> _user.GetByEmail(model.Email));
                    response = _mapper.Map<UserOM>(userdetails);
                    response.UserRole = role.FirstOrDefault();
                    return (RequestStatus.Success, response);
                }
                else
                {
                    return (RequestStatus.InvalidRequest, new UserOM());
                }
            }
            catch (Exception ex)
            {
                return (RequestStatus.FatalError, new UserOM());
                throw ex;
            }
        }

        public async Task<RequestStatus> Logout(int Id)
        {
            try
            {
                UserOM response = new UserOM();
                var userEmail = _user.GetById(Id).Email;
                var user = await Task.Run(() => _userManager.FindByEmailAsync(userEmail));
                if (user == null) return RequestStatus.NoEntryFound;

                 await _signInManager.SignOutAsync();
                return RequestStatus.Success;
            }
            catch (Exception ex)
            {
                return RequestStatus.FatalError;
                throw ex;
            }
        }

        private async Task GenerateUserClaims(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var userProfile = _user.GetByEmail(user.Email);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim("Email", user.Email),
                new Claim("UserId", userProfile.Id.ToString()),
                new Claim("UserRole", roles.FirstOrDefault()),
            }
            .Union(userClaims)
            .Union(roleClaims);
            //await  _userManager.AddClaimsAsync(user, claims);
            //return jwtSecurityToken;
        }

    }
}
