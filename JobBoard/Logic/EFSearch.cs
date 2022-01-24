using AutoMapper;
using JobBoard.Logic.Interfaces;
using JobBoard.Models;

namespace JobBoard.Logic
{
    public class EFSearch : ISearch
    {
        readonly JobBoardContext _jobBoardContext;
        readonly IMapper _mapper;

        public EFSearch(IMapper mapper)
        {
            _jobBoardContext = new JobBoardContext();
            _mapper = mapper;
        }
    }
}
