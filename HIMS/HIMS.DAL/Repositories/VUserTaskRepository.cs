using HIMS.DAL.Interfaces;
using HIMS.Data;
using HIMS.Data.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HIMS.DAL.Repositories
{
    public class VUserTaskRepository : IVUserTaskRepository<VUserTask>
    {
        private HIMSDataContext db;

        public VUserTaskRepository(HIMSDataContext ctx)
        {
            db = ctx;
        }

        public void Create(VUserTask item)
        {
            db.Set<VUserTask>().Add(item);
        }

        public void Delete(int userId, int taskId)
        {
            var userTask = db.Set<UserTask>().FirstOrDefault(ut =>ut.UserId == userId&& ut.TaskId == taskId);

            if (userTask != null)
            {
                db.Set<UserTask>().Remove(userTask);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VUserTask> Find(int? id)
        {
            return db.VUserTasks.Where(w=>w.UserId==id);
        }

        public IEnumerable<VUserTask> Find(int userId, int taskId)
        {
            return db.VUserTasks.Where(w=>w.UserId==userId && w.TaskId==taskId);
        }

        public IEnumerable<VUserTask> Find(Func<VUserTask, bool> predicate)
        {
            return db.VUserTasks.Where(predicate);
        }

        public VUserTask Get(int userId, int taskId)
        {
            return db.Set<VUserTask>().FirstOrDefault(ut => ut.UserId == userId&& ut.TaskId == taskId);
        }

        public VUserTask Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VUserTask> GetAll()
        {
            return db.Set<VUserTask>();
        }

    }
}
