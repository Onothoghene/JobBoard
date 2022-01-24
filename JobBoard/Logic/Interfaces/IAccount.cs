using JobBoard.DTO;
using JobBoard.DTO.OutputModel;
using JobBoard.Enum;
using System.Threading.Tasks;

namespace JobBoard.Logic.Interfaces
{
    public interface IAccount
    {
        Task<RequestStatus> RegisterJobSeeker(RegistrationModel model);
        Task<UserOM> LoginUser(LoginModel model);
        Task<RequestStatus> RegisterRecruiter(RegistrationModel model);
        Task<(RequestStatus, UserOM)> LoginUserLite(LoginModel model);
        Task<RequestStatus> Logout(int Id);
    }
}
