using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data;
using HIMS.Data.EntityClasses;
using HIMS.Data.Interfaces;

namespace HIMS.DAL.Repositories
{
    class TaskTrackRepository : IRepository<TaskTrack>
    {
        private HIMSDataContext db;

        public TaskTrackRepository(HIMSDataContext context)
        {
            this.db = context;
        }

        public void Create(TaskTrack item)
        {
            db.TaskTracks.Add(item);
        }

        public void Delete(int id)
        {
            TaskTrack taskTrack = db.TaskTracks.Find(id);
            if (taskTrack != null)
                db.TaskTracks.Remove(taskTrack);
        }

        public IEnumerable<TaskTrack> Find(Func<TaskTrack, bool> predicate)
        {
            return db.TaskTracks.Where(predicate).ToList();
        }

        public TaskTrack Get(int id)
        {
            return db.TaskTracks.Find(id);
        }

        public IEnumerable<TaskTrack> GetAll()
        {
            return db.TaskTracks;
        }
    }
}
