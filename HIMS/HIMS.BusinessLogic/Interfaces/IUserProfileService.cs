using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.BusinessLogic.DTO;

namespace HIMS.BusinessLogic.Interfaces
{
    public interface IUserProfileService
    {
        void SaveUserProfile(UserProfileTransferModel item);
        UserProfileTransferModel GetUserProfile(int? id);
        IEnumerable<UserProfileTransferModel> GetUserProfiles();
        void UpdateUserProfile(UserProfileTransferModel item);
        void DeleteUserProfile(int? id);
        void Dispose();
    }
}
