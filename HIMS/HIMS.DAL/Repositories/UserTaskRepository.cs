using HIMS.Data;
using HIMS.Data.EntityClasses;
using HIMS.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HIMS.DAL.Repositories
{
    public class UserTaskRepository : IRepository<UserTask>
    {
        private HIMSDataContext db;

        public UserTaskRepository(HIMSDataContext context)
        {
            db = context;
        }

        public void Create(UserTask item)
        {
            db.UserTasks.Add(new UserTask
            {
                UserTaskId = 0,
                TaskId = item.TaskId,
                UserId = item.UserId,
                StateId = item.StateId
            });
        }

        public void Delete(int id)
        {
            UserTask userTask = db.UserTasks.Find(id);
            if (userTask != null)
            {
                db.CallSpDeleteTask(id);
            }
        }

        public IEnumerable<UserTask> Find(Func<UserTask, bool> predicate)
        {
            return db.UserTasks.Where(predicate).ToList();
        }

        public UserTask Get(int id)
        {
            return db.UserTasks.Find(id);
        }

        public IEnumerable<UserTask> GetAll()
        {
            return db.UserTasks;
        }
    }
}