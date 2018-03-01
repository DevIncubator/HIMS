using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data;
using HIMS.Data.Interfaces;
using HIMS.Data.EntityClasses;

namespace HIMS.DAL.Repositories
{
    class VTaskRepository : IRepository<VTask>
    {
        private HIMSDataContext db;
        public VTaskRepository(HIMSDataContext context)
        {
            this.db = context;
        }

       public IEnumerable<VTask> GetAll()
        {
            return db.VTasks;
        }
        public VTask Get(int id)
        {
            return db.VTasks.Find(id);
        }
        public IEnumerable<VTask> Find(Func<VTask, Boolean> predicate)
        {
            return db.VTasks.Where(predicate).ToList();
        }
        public void Create(VTask vTask)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
