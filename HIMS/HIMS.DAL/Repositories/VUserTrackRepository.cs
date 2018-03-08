using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data;
using HIMS.Data.EntityClasses;
using HIMS.Data.Interfaces;
using HIMS.DAL.Interfaces;

namespace HIMS.DAL.Repositories
{
    public class VUserTrackRepository : IVUserTrackRepository<VUserTrack>
    {
        private HIMSDataContext _db;

        public VUserTrackRepository(HIMSDataContext context)
        {
            _db = context;
        }

        public void Create(VUserTrack item)
        {
            _db.Set<VUserTrack>().Add(item);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VUserTrack> Find(Func<VUserTrack, bool> predicate)
        {
            return _db.VUserTracks.Where(predicate).ToList();
        }

        public IEnumerable<VUserTrack> GetByUserId(int UserId)
        {
            return _db.VUserTracks.Where(item => item.UserId == UserId).ToList();
        }

        public IEnumerable<VUserTrack> GetAll()
        {
            return _db.VUserTracks;
        }

        public VUserTrack Get(int id)
        {
            return _db.VUserTracks.Find(id);
        }

        public IEnumerable<VUserTrack> GetByTaskTrackId(int taskTrackId)
        {
            return _db.VUserTracks.Where(item => item.TaskTrackId == taskTrackId);
        }
    }
}
