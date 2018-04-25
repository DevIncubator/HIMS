using AutoMapper;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Interfaces;
using HIMS.DAL.Interfaces;
using HIMS.Data.EntityClasses;
using HIMS.Data.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System;

namespace HIMS.BusinessLogic.Services
{
    public class VUserTaskService : IVUserTaskService
    {
        private IUnitOfWork Database { get; set; }
        private IProcedureManager Pm { get; set; }

        public VUserTaskService(IUnitOfWork uow, IProcedureManager pm)
        {
            Database = uow;
            Pm = pm;
        }

        public IEnumerable<UserTaskTransferModel> GetAllTasksForUser(int? userId)
        {
            var tasks = Database.VUserTasks.Find(userId);
            if (tasks == null)
                throw new ValidationException($"The Tasks with id = {userId} was not found");
            return Mapper.Map<IEnumerable<VUserTask>, List<UserTaskTransferModel>>(tasks); 
        }
        public IEnumerable<UserTaskTransferModel> GetAllUsersForTask(int? taskId)
        {
            var users = Database.UserTasks.Find(f => f.TaskId == taskId).ToList();
            if (users == null)
                throw new ValidationException($"The Users with TaskId = {taskId} was not found");
            //var usersDto = new List<UserTaskTransferModel>();
            //foreach (var item in users)
            //{
            //    usersDto.

            //}
            //return
            return Mapper.Map<IEnumerable<UserTask>, List<UserTaskTransferModel>>(users);
        }

        public UserTaskTransferModel GetTaskForUser(int userId, int taskId)
        {
            //var userTask = Database.VUserTasks.Find(f => f.UserId == userId && f.TaskId == taskId).FirstOrDefault();
            var userTask = Database.VUserTasks.Find(userId,taskId).FirstOrDefault();

            if (userTask == null)
            {
                throw new ValidationException($"User task with such userId = {userId} and taskId = {taskId} does not exist");
            }

            return Mapper.Map<VUserTask, UserTaskTransferModel>(userTask);
        }

        public void UpdateTaskForUser(UserTaskTransferModel userDTO)
        {
            if (userDTO.TaskName.Length > 50)
                throw new ValidationException($"The length of {nameof(userDTO.TaskName)} must be less then 50");

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

            if (userDTO.TaskName.Length > 25)
                throw new ValidationException($"The length of {nameof(userDTO.TaskName)} must be less then 25");
            var userTask = new UserTask
            {
                TaskId = userDTO.TaskId,
                UserId = userDTO.UserId,
                StateId = 1

            };

            //Mapper.Map<UserTaskTransferModel, UserTask>(userDTO);
            Database.UserTasks.Create(userTask);
            Database.Save();
        }
        public void DeleteUserTask(int? userId, int? taskId)
        {
            if (!taskId.HasValue && !userId.HasValue)
            {
                throw new ValidationException("The UserTask's id value is not set");
            }

            Database.VUserTasks.Delete(userId.Value, taskId.Value);
            Database.Save();
               

        }
    }
}
