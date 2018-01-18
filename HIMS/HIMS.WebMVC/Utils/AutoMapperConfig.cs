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
                cfg.CreateMap<VUserProfileTransferModel, VUserProfile>();
                cfg.CreateMap<VUserProfile, VUserProfileTransferModel>();
                cfg.CreateMap<VUserProfileTransferModel, UserTransferModel>().ForMember(x => x.Id, opt => opt.Ignore());
            });
        }
    }
}