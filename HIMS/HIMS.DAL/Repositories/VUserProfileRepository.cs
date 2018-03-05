using HIMS.Data.EntityClasses;
using HIMS.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using HIMS.DAL.Interfaces;

namespace HIMS.Data.Repositories
{
    public class VUserProfileRepository : IVUserProfileRepository<VUserProfile>
    {
        private HIMSDataContext _db;

        public VUserProfileRepository(HIMSDataContext context)
        {
            _db = context;
        }

        public void Create(VUserProfile item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VUserProfile> Find(Func<VUserProfile, bool> predicate)
        {
            return _db.VUserProfiles.Where(predicate);
        }

        public IEnumerable<VUserProfile> GetByUserEmail(string Email)
        {
            return _db.VUserProfiles.Where(item => item.Email == Email).ToList();
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
