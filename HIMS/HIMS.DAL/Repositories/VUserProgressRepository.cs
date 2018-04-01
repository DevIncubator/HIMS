using HIMS.DAL.Interfaces;
using HIMS.Data;
using HIMS.Data.EntityClasses;
using System.Collections.Generic;
using System.Linq;

namespace HIMS.DAL.Repositories
{
    public class VUserProgressRepository : IVUserProgressRepository
    {
        private HIMSDataContext db;

        public VUserProgressRepository(HIMSDataContext context)
        {
            db = context;
        }

        public IEnumerable<VUserProgress> GetProgressByUserId(int id)
        {
            return db.VUserProgresses.Where(p => p.UserId == id);
        }

        public string GetUserNameById(int id)
        {
            var item = db.VUserProgresses.Where(p => p.UserId == id).FirstOrDefault();
            return item?.UserName;
        }
    }
}