using HIMS.BusinessLogic.DTO;

namespace HIMS.BusinessLogic.Interfaces
{
    public interface IUserTaskTService
    {
        UserTaskTTransferModel Get(int userId, int taskId);
    }
}