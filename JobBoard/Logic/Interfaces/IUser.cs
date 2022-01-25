using JobBoard.DTO.EdiModel;
using JobBoard.DTO.OutputModel;
using JobBoard.Enum;

namespace JobBoard.Logic.Interfaces
{
    public interface IUser
    {
        RequestStatus Delete(int Id);
        UserOM GetById(int Id);
        UserOM GetByEmail(string email);
        RequestStatus Edit(UserEM model);
    }
}
