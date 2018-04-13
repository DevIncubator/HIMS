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

                cfg.CreateMap<Task, TaskTransferModel>();
                cfg.CreateMap<TaskTransferModel, Task>();
                cfg.CreateMap<TaskViewModel, TaskTransferModel>();
                cfg.CreateMap<TaskTransferModel, TaskViewModel>()
                .ForMember(x => x.SelectedUsers, opt => opt.Ignore());


                cfg.CreateMap<VUserProfile, VUserProfileTransferModel>();
                cfg.CreateMap<UserProfile, UserProfileTransferModel>();
                cfg.CreateMap<UserProfileTransferModel, UserTransferModel>().ForMember(x => x.Id, opt => opt.Ignore());
                cfg.CreateMap<UserProfileTransferModel, UserProfile>();
                cfg.CreateMap<VUserProfileTransferModel, UserProfileGridViewModel>()
                    .ForMember(x => x.Start, opt => opt.MapFrom(src => src.StartDate));
                cfg.CreateMap<UserProfileTransferModel, UserProfileDetailsViewModel>();
                cfg.CreateMap<Direction, DirectionTransferModel>();
                cfg.CreateMap<DirectionTransferModel, DirectionViewModel>();

                cfg.CreateMap<UserTask, UserTaskTransferModel>()
                    .ForMember(x => x.Name, opt => opt.Ignore())
                     .ForMember(x => x.Deadline, opt => opt.Ignore())
                      .ForMember(x => x.Start, opt => opt.Ignore())
                      .ForMember(x=>x.Status,opt=>opt.Ignore());

                //cfg.CreateMap<UserTaskTransferModel, UserTask>()
                // .ForMember(x => x.StateId, opt => opt.Ignore());

                cfg.CreateMap<VUserTask, UserTaskTransferModel>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TaskName))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.State));
                cfg.CreateMap<UserTaskTransferModel, VUserTask>();
                cfg.CreateMap<UserTaskTransferModel, UserTaskViewModel>();
                cfg.CreateMap<UserTaskViewModel, UserTaskTransferModel>();
            });
        }
    }
}