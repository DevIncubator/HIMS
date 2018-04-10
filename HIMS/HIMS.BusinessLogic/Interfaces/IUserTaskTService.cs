using HIMS.BusinessLogic.DTO;

namespace HIMS.BusinessLogic.Interfaces
{
    public interface IVUserTaskTService
    {
        UserTaskTTransferModel Get(int userId, int taskId);
    }
}