using HIMS.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Infrastructure;
using HIMS.Data.Interfaces;
using HIMS.Data.EntityClasses;
using AutoMapper;

namespace HIMS.BusinessLogic.Services
{
    public class UserProfileService : IUserProfileService
    {
        private IUnitOfWork Database;
        private IUserService userService;

        public UserProfileService(IUnitOfWork uow, IUserService us)
        {
            Database = uow;
            userService = us;
        }

        private void UserProfileValidation(UserProfileTransferModel userProfileDto, bool isUpdating)
        {
            var existUserProfiles = Database.UserProfiles.Find(up => up.Email == userProfileDto.Email);
            if (!(
                  (existUserProfiles.Count() == 0) ||
                  (existUserProfiles.Count() == 1 && isUpdating)
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

        public void DeleteUserProfile(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The VUserProfile's id value is not set", String.Empty);

            var userToDeletion = Database.UserProfiles.Get(id.Value);
            OperationDetails operationDetails = null;

            if (userToDeletion == null)
                throw new ValidationException("User with such id not found", String.Empty);
            else
                operationDetails = userService.Delete(userToDeletion.Email).Result; //try to delete User from Identity DB

            if (operationDetails.Succedeed == true)
            {
                Database.UserProfiles.Delete(id.Value);
                Database.Save();
            }
            else
                throw new ValidationException(operationDetails.Message, operationDetails.Property);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public UserProfileTransferModel GetUserProfile(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The UserPrifile's id value is not set", String.Empty);

            var userProfile = Database.UserProfiles.Get(id.Value);

            if (userProfile == null)
                throw new ValidationException($"The UserProfile with id = {id} was not found", String.Empty);

            return Mapper.Map<UserProfile, UserProfileTransferModel>(userProfile);
        }

        public IEnumerable<UserProfileTransferModel> GetUserProfiles()
        {
            return Mapper.Map<IEnumerable<UserProfile>, List<UserProfileTransferModel>>(Database.UserProfiles.GetAll());
        }

        public void SaveUserProfile(UserProfileTransferModel userProfileDto)
        {
            // Validation
            UserProfileValidation(userProfileDto, false);

            //try to create user in the Identity DB
            UserTransferModel userDto = Mapper.Map<UserProfileTransferModel, UserTransferModel>(userProfileDto);
            OperationDetails identityResult = userService.Create(userDto).Result;

            if (identityResult.Succedeed == true)
            {
                var userProfile = Mapper.Map<UserProfileTransferModel, UserProfile>(userProfileDto);
                Database.UserProfiles.Create(userProfile);
                Database.Save();
            }
            else
                throw new ValidationException(identityResult.Message, userProfileDto.Email); //return message from Create() method of UserService
        }

        public void UpdateUserProfile(UserProfileTransferModel userProfileDto)
        {
            UserProfileValidation(userProfileDto, true);

            var userProfile = Database.UserProfiles.Get(userProfileDto.UserId);

            if (userProfile != null)
            {
                Mapper.Map(userProfileDto, userProfile);
                Database.Save();
            }
        }
    }
}
