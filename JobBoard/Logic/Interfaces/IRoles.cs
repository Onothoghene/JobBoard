using JobBoard.Enum;
using System.Threading.Tasks;

namespace JobBoard.Logic.Interfaces
{
    public interface IRoles
    {
        Task<RequestStatus> Create();
    }
}
