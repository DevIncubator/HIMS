using System.Collections.Generic;
using HIMS.BusinessLogic.DTO;

namespace HIMS.BusinessLogic.Interfaces
{
    public interface IVUserTrackService
    {
        IEnumerable<VUserTrackTransferModel> GetVUserTrack(int? UserId);
        VUserTrackTransferModel Get(int? id);
        void UpdateUserTrack(VUserTrackTransferModel item);
        VUserTrackTransferModel GetByTaskTrack(int? id);
        void Dispose();
    }
}