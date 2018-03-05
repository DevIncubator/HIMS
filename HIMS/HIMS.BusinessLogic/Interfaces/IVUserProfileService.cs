using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.BusinessLogic.DTO;


namespace HIMS.BusinessLogic.Interfaces
{
    public interface IVUserProfileService
    {
        //void SaveVUserProfile(VUserProfileTransferModel item);
        VUserProfileTransferModel GetVUserProfile(int? id);
        VUserProfileTransferModel GetVUserProfile(string Email);
        IEnumerable<VUserProfileTransferModel> GetVUserProfiles();
        //void UpdateVUserProfile(VUserProfileTransferModel item);
        //void DeleteVUserProfile(int? id);
        void Dispose();
    }
}
