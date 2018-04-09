using System.Linq;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Interfaces;
using HIMS.Data;
using HIMS.Data.EntityClasses;
using HIMS.Data.Interfaces;
using AutoMapper;

namespace HIMS.BusinessLogic.Services
{
    public class UserTaskTService : IUserTaskTService
    {
        private IUnitOfWork Database { get; set; }


        public UserTaskTService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void Dispose()
        {
            Database.Dispose();
        }

        

        public UserTaskTTransferModel Get(int userId, int taskId)
        {
            var item = Database.UserTasks.Find(ut => ut.UserId == userId && ut.TaskId == taskId).FirstOrDefault();
            return Mapper.Map<UserTask, UserTaskTTransferModel>(item);
        }
    }
}