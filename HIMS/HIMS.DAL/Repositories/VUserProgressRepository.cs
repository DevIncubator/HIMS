using HIMS.Data;
using HIMS.Data.EntityClasses;
using HIMS.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HIMS.DAL.Repositories
{
    public class VUserProgressRepository : IRepository<VUserProgress>
    {
        private HIMSDataContext db;

        public VUserProgressRepository(HIMSDataContext context)
        {
            db = context;
        }

        public void Create(VUserProgress item)
        {
            db.Set<VUserProgress>().Add(item);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VUserProgress> Find(Func<VUserProgress, bool> predicate)
        {
            return db.VUserProgresses.Where(predicate);
        }

        public VUserProgress Get(int id)
        {
            return db.VUserProgresses.Find(id);
        }

        public IEnumerable<VUserProgress> GetAll()
        {
            return db.VUserProgresses;
        }
    }
}