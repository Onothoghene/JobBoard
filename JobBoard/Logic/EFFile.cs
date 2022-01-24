using AutoMapper;
using JobBoard.Logic.Interfaces;
using JobBoard.Models;

namespace JobBoard.Logic.Logic
{
    public class EFFile : IFile
    {
        readonly JobBoardContext _jobBoardContext;
        readonly IMapper _mapper;

        public EFFile(IMapper mapper)
        {
            _jobBoardContext = new JobBoardContext();
            _mapper = mapper;
        }

    }
}
