using HIMS.BusinessLogic.DTO;
using System.Collections.Generic;

namespace HIMS.BusinessLogic.Interfaces
{
    public interface IVUserProgressService
    {
        IEnumerable<VUserProgressTransferModel> GetProgressByUserId(int id);
        string GetUserNameById(int id);
    }
}