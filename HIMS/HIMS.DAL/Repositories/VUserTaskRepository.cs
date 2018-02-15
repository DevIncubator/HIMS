using HIMS.DAL.Interfaces;
using HIMS.Data;
using HIMS.Data.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HIMS.DAL.Repositories
{
    class VUserTaskRepository : IVUserTaskRepository<VUserTask>
    {
        private HIMSDataContext _db;

        public VUserTaskRepository(HIMSDataContext context)
        {
            _db = context;
        }

        public void Create(VUserTask item)
        {
            _db.Set<VUserTask>().Add(item);
        }

        public void Delete(int userId, int taskId)
        {
            var userTask = _db.Set<VUserTask>().FirstOrDefault(ut => ut.TaskId == taskId && ut.UserId == userId);

            if(userTask != null)
            {
                _db.Set<VUserTask>().Remove(userTask); 
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VUserTask> Find(Func<VUserTask, bool> predicate)
        {
            return _db.Set<VUserTask>().Where(predicate);
        }

        public VUserTask Get(int userId, int taskId)
        {
            return _db.Set<VUserTask>().FirstOrDefault(ut => ut.TaskId == taskId && ut.UserId == userId);
        }

        public VUserTask Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VUserTask> GetAll()
        {
            return _db.Set<VUserTask>();
        }
    }
}
