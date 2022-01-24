using JobBoard.DTO.EdiModel;
using JobBoard.DTO.InputModel;
using JobBoard.DTO.OutputModel;
using JobBoard.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobBoard.Logic.Interfaces
{
    public interface IJob
    {
        Task<RequestStatus> Update(JobEM model);
        RequestStatus Update(JobEM model, out JobOM resp);
        JobOM GetById(int jobId);
        IEnumerable<JobOM> GetJobs();
        RequestStatus Add(JobIM model);
        RequestStatus Delete(int Id);
        IEnumerable<JobOM> GetJobsByUserId(int userId);
    }
}
