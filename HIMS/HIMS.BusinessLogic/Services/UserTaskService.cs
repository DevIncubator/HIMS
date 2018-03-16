using AutoMapper;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Interfaces;
using HIMS.DAL.Interfaces;
using HIMS.Data.EntityClasses;
using HIMS.Data.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HIMS.BusinessLogic.Services
{
    public class UserTaskService : IUserTaskService
    {
        private IUnitOfWork Database { get; set; }
        private IProcedureManager Pm { get; set; }

        public UserTaskService(IUnitOfWork uow, IProcedureManager pm)
        {
            Database = uow;
            Pm = pm;
        }

        public IEnumerable<UserTaskTransferModel> GetAllTasksForUser(int userId)
        {
            var tasks = Database.VUserTasks.Find(f => f.UserId == userId).ToList();
            if (tasks == null)
                throw new ValidationException($"The Tasks with id = {userId} was not found");
            return Mapper.Map<IEnumerable<VUserTask>, List<UserTaskTransferModel>>(tasks);
        }

        public UserTaskTransferModel GetTaskForUser(int userId, int taskId)
        {
            var userTask = Database.VUserTasks.Find(f => f.UserId == userId && f.TaskId == taskId).FirstOrDefault();

            if (userTask == null)
            {
                throw new ValidationException($"User task with such userId = {userId} and taskId = {taskId} does not exist");
            }

            return Mapper.Map<VUserTask, UserTaskTransferModel>(userTask);
        }

        public void UpdateTaskForUser(UserTaskTransferModel userDTO)
        {
            if (userDTO.Name.Length > 50)
                throw new ValidationException($"The length of {nameof(userDTO.Name)} must be less then 50");

            var userTask = Database.VUserTasks.Get(userDTO.UserId, userDTO.TaskId);

            if (userTask != null)
            { 
                Mapper.Map(userDTO, userTask);
                Database.Save();
            }  
        }

        public void UpdateTaskStatusForUser(int userId, int taskId, bool isSuccess = false)
        {
            if (isSuccess)
            {
                Pm.SetUserTaskAsSuccess(userId, taskId);
            }
            else
            {
                Pm.SetUserTaskAsFail(userId, taskId);
            }
        }

        public void SaveTaskForUser(UserTaskTransferModel userDTO)
        {
            if (userDTO.Name.Length > 25)
                throw new ValidationException($"The length of {nameof(userDTO.Name)} must be less then 25");
            var userTask = Mapper.Map<UserTaskTransferModel, VUserTask>(userDTO);
            Database.VUserTasks.Create(userTask);
            Database.Save();
        }
    }
}
