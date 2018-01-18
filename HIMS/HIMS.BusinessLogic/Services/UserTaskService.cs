using AutoMapper;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Interfaces;
using HIMS.DAL.Interfaces;
using HIMS.Data.EntityClasses;
using HIMS.Data.Interfaces;
using System;
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

        public IEnumerable<UserTaskTransferModel> GetAllUserTasks(int userId)
        {
            return Mapper.Map<IEnumerable<VUserTask>, List<UserTaskTransferModel>>(Database.VUserTasks.Find(ut => ut.UserId == userId));
        }

        public UserTaskTransferModel GetUserTask(int userId, int taskId)
        {
            var userTask = Database.VUserTasks.Find(ut => ut.UserId == userId && ut.TaskId == taskId).FirstOrDefault();

            if(userTask == null)
            {
                throw new ValidationException($"User task with such userId and taskId does not exist");
            }

            return Mapper.Map<VUserTask, UserTaskTransferModel>(userTask);
        }

        public void SaveUserTask(UserTaskTransferModel userDTO)
        {
            var userTask = Mapper.Map<UserTaskTransferModel, VUserTask>(userDTO);
            Database.VUserTasks.Create(userTask);
            Database.Save();
        }

        public void UpdateUserTask(UserTaskTransferModel userDTO)
        {
            var userTask = Database.VUserTasks.Get(userDTO.userId, userDTO.taskId);

            if(userTask == null)
            {
                throw new ValidationException($"User task with such userId and taskId does not exist");
            }
            
                Mapper.Map(userDTO, userTask);
                Database.Save();
            
        }

        public void UpdateUserTaskStatus(int userId, int taskId, bool isSuccess = false)
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
    }
}
