using HIMS.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BusinessLogic.Interfaces
{
    public interface IDirectionService
    {
        IEnumerable<DirectionTransferModel> GetDirections();
        DirectionTransferModel GetDirection(int? id);
        void Dispose();
    }
}
