using HIMS.BusinessLogic.DTO;

namespace HIMS.BusinessLogic.Interfaces
{
    public interface IUserTaskService
    {
        UserTaskTTransferModel Get(int userId, int taskId);
    }
}