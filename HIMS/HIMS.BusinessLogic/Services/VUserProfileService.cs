using HIMS.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.BusinessLogic.DTO;
using HIMS.Data.Interfaces;
using HIMS.Data.EntityClasses;
using HIMS.BusinessLogic.Infrastructure;
using AutoMapper;
using HIMS.Data.Identity.Entities;

namespace HIMS.BusinessLogic.Services
{
    public class VUserProfileService : IVUserProfileService
    {
        private IUnitOfWork Database;
        //private IUserService userService;

        public VUserProfileService(IUnitOfWork uow)//, IUserService us)
        {
            Database = uow;
            //userService = us;
        }

        public VUserProfileTransferModel GetVUserProfile(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The VUserProfile's id value is not set", String.Empty);

            VUserProfile userProfile = Database.VUserProfiles.Get(id.Value);
            return Mapper.Map<VUserProfile, VUserProfileTransferModel>(userProfile);
        }

        public IEnumerable<VUserProfileTransferModel> GetVUserProfiles()
        {
            return Mapper.Map<IEnumerable<VUserProfile>, List<VUserProfileTransferModel>>(Database.VUserProfiles.GetAll());
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public VUserProfileTransferModel GetVUserProfile(string Email)
        {
            if (Email == null)
                throw new ValidationException("The VUserProfile's Email value is not set", String.Empty);


            var user = Database.VUserProfiles.GetByUserEmail(Email).First();
            return Mapper.Map<VUserProfile, VUserProfileTransferModel>(user);
        }
    }
}
