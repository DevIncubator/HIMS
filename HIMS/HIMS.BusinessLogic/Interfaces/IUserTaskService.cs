using HIMS.BusinessLogic.DTO;
using System.Collections.Generic;

namespace HIMS.BusinessLogic.Interfaces
{
    public interface IUserTaskService
    {
        void SaveUserTask(UserTaskTransferModel userTaskDTO);
        UserTaskTransferModel GetUserTask(int userId, int taskId);
        IEnumerable<UserTaskTransferModel> GetAllUserTasks(int userId);
        void UpdateUserTask(UserTaskTransferModel userTaskDTO);
        void UpdateUserTaskStatus(int userId, int taskId, bool isSuccess = false);
    }
}
