using AutoMapper;
using JobBoard.DTO.EdiModel;
using JobBoard.DTO.OutputModel;
using JobBoard.Enum;
using JobBoard.Logic.Interfaces;
using JobBoard.Models;
using System;
using System.Linq;
using System.Transactions;

namespace JobBoard.Logic
{
    public class EFUser : IUser
    {
        readonly JobBoardContext _jobBoardContext;
        readonly IMapper _mapper;

        public EFUser(IMapper mapper)
        {
            _jobBoardContext = new JobBoardContext();
            _mapper = mapper;
        }

        public RequestStatus Delete(int Id)
        {
            try
            {
                var data = _jobBoardContext.UserProfile.Find(Id);
                if (data != null)
                {
                    _jobBoardContext.UserProfile.Remove(data);
                    _jobBoardContext.SaveChanges();

                    return RequestStatus.Success;
                }
                else
                {
                    return RequestStatus.NoEntryFound;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserOM GetById(int Id)
        {
            try
            {
                var data = _jobBoardContext.UserProfile.Find(Id);
                if (data != null)
                {
                    var result = _mapper.Map<UserOM>(data);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        
        public UserOM GetByEmail(string email)
        {
            try
            {
                var data = _jobBoardContext.UserProfile.Where(r => r.Email == email).FirstOrDefault();
                if (data != null)
                {
                    var result = _mapper.Map<UserOM>(data);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public RequestStatus Edit(UserEM model)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var data = _jobBoardContext.UserProfile.Find(model.Id);

                    if (data != null)
                    {
                        if (model.PhoneNumber != null)
                            data.PhoneNumber = model.PhoneNumber;

                        if (model.Firstname != null)
                            data.Firstname = model.Firstname;

                        if (model.Surname != null)
                            data.Surname = model.Surname;

                        if (model.Email != null)
                            data.Email = model.Email;
                        
                        if (model.CompanyName != null)
                            data.CompanyName = model.CompanyName;

                        _jobBoardContext.SaveChanges();

                        ts.Complete();
                        return RequestStatus.Success;
                    }
                    else
                    {
                        return RequestStatus.NoEntryFound;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
