using HIMS.Data.EntityClasses;
using HIMS.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HIMS.Data.Repositories
{
    public class VUserProfileRepository : IRepository<VUserProfile>
    {
        private HIMSDataContext _db;

        public VUserProfileRepository(HIMSDataContext context)
        {
            _db = context;
        }

        public void Create(VUserProfile item)
        {
            _db.VUserProfiles.Add(item);
        }

        public void Update(VUserProfile item)
        {
                _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            VUserProfile userProfile = _db.VUserProfiles.Find(id);
            if (userProfile != null)
            _db.CallSpDeleteUser(id); //directly call the stored-procedure-method from LLBLGen auto generated class HIMSDataContext
        }

        public IEnumerable<VUserProfile> Find(Func<VUserProfile, bool> predicate)
        {
            return _db.VUserProfiles.Where(predicate);
        }

        public VUserProfile Get(int id)
        {
            return _db.VUserProfiles.Find(id);
        }

        public IEnumerable<VUserProfile> GetAll()
        {
            return _db.VUserProfiles;
        }
    }
}
