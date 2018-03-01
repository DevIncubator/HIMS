using System.Collections.Generic;
using HIMS.BusinessLogic.DTO;

namespace HIMS.BusinessLogic.Interfaces
{
    public interface IVUserTrackService
    {
        IEnumerable<VUserTrackTransferModel> GetVUserTrack(int? id);
        VUserTrackTransferModel Get(int? id);
        void Dispose();
    }
}