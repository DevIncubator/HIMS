using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoMapper;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Infrastructure;
using HIMS.Data.EntityClasses;
using HIMS.Data.Interfaces;
using HIMS.BusinessLogic.Interfaces;
using HIMS.DAL.Interfaces;

namespace HIMS.BusinessLogic.Services
{
    public class TaskService : ITaskService
    {
        private IUnitOfWork Database { get; set; }
        private IProcedureManager Pm { get; set; }

        public TaskService(IUnitOfWork uow, IProcedureManager pm)
        {
            Database = uow;
            Pm = pm;
        }

        public void SaveTask(TaskTransferModel taskDTO)
        {
            //Validation
            if (taskDTO.Name.Length > 25)
                throw new ValidationException($"The length of {nameof(taskDTO.Name)} must be less then 25"
                    , nameof(taskDTO.Name));
            if (taskDTO.Description.Length > 255)
                throw new ValidationException($"The length of {nameof(taskDTO.Description)} must be less then 25"
                    , nameof(taskDTO.Description));

            var task = new Task
            {
                Name = taskDTO.Name,
                Description = taskDTO.Description,
                StartDate = taskDTO.Start,
                DeadlineDate = taskDTO.Deadline

            };

            Database.Tasks.Create(task);
            Database.Save();

        }

        public void UpdateTask(TaskTransferModel taskDTO)
        {
            if (taskDTO.Name.Length > 25)
                throw new ValidationException($"The length of {nameof(taskDTO.Name)} must be less then 25"
                    , nameof(taskDTO.Name));
            if (taskDTO.Description.Length > 255)
                throw new ValidationException($"The length of {nameof(taskDTO.Description)} must be less then 25"
                    , nameof(taskDTO.Description));

            var task = Database.Tasks.Get(taskDTO.TaskId);

            if (task != null)
            {
                Mapper.Map(taskDTO, task);
                Database.Save();
            }
        }

        public void SetTaskStatusActive(int userId, int taskId)
        {
            Pm.SetUserTaskAsActive(userId, taskId);
        }

        public void SetMember(UserTaskTransferModel userDTO)
        {
            var userTask = Mapper.Map<UserTaskTransferModel, VUserTask>(userDTO);
            Database.VUserTasks.Create(userTask);
            Database.Save();
        }

        public void DeleteTask(int? taskId)
        {
            if (!taskId.HasValue)
            {
                throw new ValidationException("The Task's id value is not set", String.Empty);

            }
            Database.Tasks.Delete(taskId.Value);
            Database.Save();
        }
        public TaskTransferModel GetTask(int taskId)
        {
            var task = Database.Tasks.Find(ut => ut.TaskId == taskId).FirstOrDefault();

            if (task == null)
            {
                throw new ValidationException("The Task's id value is not set", String.Empty);
            }

            return Mapper.Map<Task, TaskTransferModel>(task);
        }
        public IEnumerable<TaskTransferModel> GetAllTasks(int taskId)
        {
            var tasks = Database.Tasks.Find(ut => ut.TaskId == taskId).ToList();
            return Mapper.Map<IEnumerable<Task>, List<TaskTransferModel>>(tasks);
        }
    }
}
