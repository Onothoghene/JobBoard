using AutoMapper;
using JobBoard.DTO;
using JobBoard.DTO.EdiModel;
using JobBoard.DTO.InputModel;
using JobBoard.DTO.OutputModel;
using JobBoard.Models;
using JobBoard.ViewModels;

namespace JobBoard.Handlers
{
    public class MappingAction : Profile
    {
        public MappingAction()
        {
            CreateMap<Job, JobOM>()
                 .ForMember(dest => dest.JobType, opt => opt.MapFrom(src => src.JobType.Name));

            CreateMap<JobIM, Job>();

            CreateMap<JobInputModel, JobIM>();

            CreateMap<JobOM, JobOutputModel>()
                .ForMember(dest => dest.JobType, opt => opt.MapFrom(src => src.JobType));

            CreateMap<Files, FileModel>();

            CreateMap<FileViewModel, FileModel>();

            CreateMap<FileModel, FileViewModel>();

            CreateMap<FileModel, Files>();

            CreateMap<RegistrationViewModel, RegistrationModel>()
                   .ForMember(dest => dest.CV, opt => opt.MapFrom(src => src.CV ?? null));

            CreateMap<RegistrationModel, UserProfile>()
                .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.CV ?? null));

            CreateMap<LoginViewModel, LoginModel>();

            CreateMap<UserProfile, UserOM>();

            CreateMap<UserOM, UserViewModel>();

            CreateMap<UserOM, UserModel>();

            CreateMap<UserModel, UserIM>();

            CreateMap<UserIM, UserProfile>();

            CreateMap<UserEM, UserProfile>();

            CreateMap<UserModel, UserEM>();
        }
    }

}
