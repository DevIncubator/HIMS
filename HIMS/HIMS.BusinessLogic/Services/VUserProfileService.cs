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
        private IUserService userService;

        public VUserProfileService(IUnitOfWork uow, IUserService us)
        {
            Database = uow;
            userService = us;
        }

        private void VUserProfileValidation(VUserProfileTransferModel userProfileDto, bool isUpdating)
        {
            var existVUserProfiles = Database.VUserProfiles.Find(up => up.Email == userProfileDto.Email);
            if (!(
                  (existVUserProfiles.Count() == 0) ||
                  (existVUserProfiles.Count() == 1 && isUpdating)
                  )
                )
                throw new ValidationException($"User with the same Email already exist", nameof(userProfileDto.Email));

            if (userProfileDto.MathScore > 10 || userProfileDto.MathScore < 0)
                throw new ValidationException($"The MathScore value cannot be less than 0 and greater then 10",
                    nameof(userProfileDto.MathScore));
            if (userProfileDto.UniversityAverageScore > 10 || userProfileDto.UniversityAverageScore < 0)
                throw new ValidationException($"The UniversityAverageScore value cannot be less than 0 and greater then 10",
                    nameof(userProfileDto.UniversityAverageScore));
            else return;
        }

        public void DeleteVUserProfile(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The VUserProfile's id value is not set", String.Empty);

            var userToDeletion = Database.VUserProfiles.Get(id.Value);
            OperationDetails operationDetails = null;

            if (userToDeletion == null)
                throw new ValidationException("User with such id not found", String.Empty);
            else
                operationDetails = userService.Delete(userToDeletion.Email).Result;

            if (operationDetails.Succedeed == true)
            {
                Database.VUserProfiles.Delete(id.Value);
                Database.Save();
            }
            else
                throw new ValidationException(operationDetails.Message, operationDetails.Property);
        }

        public void Dispose()
        {
            Database.Dispose();
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

        public void SaveVUserProfile(VUserProfileTransferModel vUserProfileDto)
        {
            // Validation
            VUserProfileValidation(vUserProfileDto, false);

            //try to create user in Identity DB
            UserTransferModel userDto = Mapper.Map<VUserProfileTransferModel, UserTransferModel>(vUserProfileDto);
            OperationDetails identityResult = userService.Create(userDto).Result;

            if (identityResult.Succedeed == true)
            {
                var userProfile = Mapper.Map<VUserProfileTransferModel, VUserProfile>(vUserProfileDto);
                Database.VUserProfiles.Create(userProfile);
                Database.Save();
            }
            else
                throw new ValidationException(identityResult.Message, vUserProfileDto.Email); //return message from Create() method of UserService
        }

        public void UpdateVUserProfile(VUserProfileTransferModel vUserProfileDto)
        {
            VUserProfileValidation(vUserProfileDto, true);

            var vUserProfile = Database.VUserProfiles.Get(vUserProfileDto.UserId);

            if (vUserProfile != null)
            {
                Mapper.Map(vUserProfileDto, vUserProfile);
                Database.Save();
            }
        }
    }
}
