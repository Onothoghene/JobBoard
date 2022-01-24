using AutoMapper;
using JobBoard.DTO.EdiModel;
using JobBoard.DTO.InputModel;
using JobBoard.DTO.OutputModel;
using JobBoard.Enum;
using JobBoard.Logic.Interfaces;
using JobBoard.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace JobBoard.Logic
{
    public class EFJob : IJob
    {
        readonly JobBoardContext _jobBoardContext;
        readonly IMapper _mapper;

        public EFJob(IMapper mapper)
        {
            _jobBoardContext = new JobBoardContext();
            _mapper = mapper;
        }

        public async Task<RequestStatus> Update(JobEM model)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var data = await _jobBoardContext.Jobs.FindAsync(model.Id);

                    if (data != null)
                    {
                        data.Id = model.Id;

                        if (model.CompanyDescription != null)
                            data.CompanyDescription = model.CompanyDescription;

                        if (model.JobTitle != null)
                            data.JobTitle = model.JobTitle;

                        if (model.JobDescription != null)
                            data.JobDescription = model.JobDescription;

                        if (model.JobLocation != null)
                            data.JobLocation = model.JobLocation;

                        if (model.Region != null)
                            data.Region = model.Region;

                        if (model.CompanyName != null)
                            data.CompanyName = model.CompanyName;

                        if (model.PhoneNumber != null)
                            data.Region = model.PhoneNumber;

                        if (model.JobTypeId > 0)
                            data.JobTypeId = model.JobTypeId;

                        _jobBoardContext.SaveChanges();
                        ts.Complete();
                        return RequestStatus.Success;
                    };
                    return RequestStatus.NoEntryFound;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public RequestStatus Update(JobEM model, out JobOM resp)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var data = _jobBoardContext.Jobs.Find(model.Id);

                    if (data != null)
                    {
                        data.Id = model.Id;

                        if (model.CompanyDescription != null)
                            data.CompanyDescription = model.CompanyDescription;

                        if (model.JobTitle != null)
                            data.JobTitle = model.JobTitle;

                        if (model.JobDescription != null)
                            data.JobDescription = model.JobDescription;

                        if (model.JobLocation != null)
                            data.JobLocation = model.JobLocation;

                        if (model.Region != null)
                            data.Region = model.Region;

                        if (model.CompanyName != null)
                            data.CompanyName = model.CompanyName;

                        if (model.PhoneNumber != null)
                            data.Region = model.PhoneNumber;

                        if (model.JobTypeId > 0)
                            data.JobTypeId = model.JobTypeId;

                        _jobBoardContext.SaveChanges();
                        resp = _mapper.Map<JobOM>(data);
                        ts.Complete();
                        return RequestStatus.Success;
                    }
                    else
                    {
                        resp = new JobOM();
                        return RequestStatus.NoEntryFound;
                    }

                }
            }
            catch (Exception ex)
            {
                resp = new JobOM();
                throw ex;
            }

        }

        public JobOM GetById(int Id)
        {
            try
            {
                var data = _jobBoardContext.Jobs.Find(Id);
                if (data != null)
                {
                    var result = _mapper.Map<JobOM>(data);
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

        public IEnumerable<JobOM> GetJobs()
        {
            try
            {
                var data = _jobBoardContext.Jobs.Include(x => x.JobType).Include(x => x.Files);
                var result = _mapper.Map<IEnumerable<JobOM>>(data);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RequestStatus Add(JobIM model)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var res = _mapper.Map<Job>(model);
                    _jobBoardContext.Add(res);
                    int bit = _jobBoardContext.SaveChanges();
                    ts.Complete();

                    if (bit > 0)
                        return RequestStatus.Success;

                    return RequestStatus.InvalidRequest;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public RequestStatus Delete(int Id)
        {
            try
            {
                var data = _jobBoardContext.Jobs.Find(Id);
                if (data != null)
                {
                    _jobBoardContext.Jobs.Remove(data);
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

        public IEnumerable<JobOM> GetJobsByUserId(int userId)
        {
            try
            {
                var data = _jobBoardContext.Jobs.Where(refs => refs.CreatedBy == userId)
                    .Include(x => x.JobType);
                var result = _mapper.Map<IEnumerable<JobOM>>(data);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
