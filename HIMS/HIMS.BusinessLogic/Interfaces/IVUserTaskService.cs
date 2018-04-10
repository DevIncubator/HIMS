using HIMS.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BusinessLogic.Interfaces
{
    public interface IVUserTaskService
    {
        IEnumerable<UserTaskTransferModel> GetAllTasksForUser(int? userId);
        UserTaskTransferModel GetTaskForUser(int userId, int taskId);
        void UpdateTaskForUser(UserTaskTransferModel userDTO);
        void UpdateTaskStatusForUser(int userId, int taskId, bool isSuccess = false);
        void SaveTaskForUser(UserTaskTransferModel userDTO);
    }
}
