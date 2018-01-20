using AutoMapper;
using HIMS.BusinessLogic.DTO;
using HIMS.Data.EntityClasses;
using HIMS.WebMVC.Models;

namespace HIMS.WebMVC.Utils
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Sample, SampleTransferModel>();
                cfg.CreateMap<SampleTransferModel, Sample>();
                cfg.CreateMap<SampleViewModel, SampleTransferModel>();

                cfg.CreateMap<VUserProfile, VUserProfileTransferModel>();
                cfg.CreateMap<UserProfile, UserProfileTransferModel>();
                cfg.CreateMap<UserProfileTransferModel, UserTransferModel>().ForMember(x => x.Id, opt => opt.Ignore());
                cfg.CreateMap<UserProfileTransferModel, UserProfile>();

                cfg.CreateMap<VUserTask, UserTaskTransferModel>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TaskName));
                cfg.CreateMap<UserTaskTransferModel, VUserTask>();
                cfg.CreateMap<UserTaskTransferModel, UserTaskViewModel>();
                cfg.CreateMap<UserTaskViewModel, UserTaskTransferModel>();
            });
        }
    }
}