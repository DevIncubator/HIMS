﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.BusinessLogic.DTO;

namespace HIMS.BusinessLogic.Interfaces
{
    public interface ITaskService
    {
        void SaveTask(TaskTransferModel taskDTO);
        int GetLastTaskId();
        void SetMember(UserTaskTransferModel userDTO);
        TaskTransferModel GetTask(int? taskId);
        IEnumerable<TaskTransferModel> GetAllTasks();
        void UpdateTask(TaskTransferModel taskDTO);
        void DeleteTask(int? taskId);
        void SetTaskStatusActive(int userId, int taskId);
    }
}
