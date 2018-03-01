using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HIMS.Data.Interfaces;
using HIMS.Data.EntityClasses;
using HIMS.Data;

namespace HIMS.DAL.Repositories
{
   public class TaskRepository: IRepository<Task>
    {
        private HIMSDataContext db;
        public TaskRepository(HIMSDataContext context)
        {
            this.db = context;
        }

       public IEnumerable<Task> GetAll()
        {
            return db.Tasks;
        }
        public Task Get(int id)
        {
            return db.Tasks.Find(id);
        }
        public IEnumerable<Task> Find(Func<Task, Boolean> predicate)
        {
            return db.Tasks.Where(predicate).ToList();
        }
        public void Create(Task task)
        {
            db.Tasks.Add(new Task
            {
                TaskId = 0,
                Name = task.Name,
                Description=task.Description,
                StartDate=task.StartDate,
                DeadlineDate=task.DeadlineDate
            });
        }
        public void Delete(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task != null)
            {
                db.CallSpDeleteTask(id);
            }
        }
    }
}
