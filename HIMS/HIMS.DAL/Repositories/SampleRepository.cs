using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Interfaces;
using HIMS.Data;
using HIMS.Data.EntityClasses;

namespace HIMS.Data.Repositories
{
    public class SampleRepository : IRepository<Sample>
    {
        private HIMSDataContext db;

        public SampleRepository(HIMSDataContext context)
        {
            this.db = context;
        }

        public IEnumerable<Sample> GetAll()
        {
            return db.Samples;
        }

        public Sample Get(int id)
        {
            return db.Samples.Find(id);
        }

        public void Create(Sample sample)
        {
            db.Samples.Add(sample);
        }

        public IEnumerable<Sample> Find(Func<Sample, Boolean> predicate)
        {
            return db.Samples.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Sample sample = db.Samples.Find(id);
            if (sample != null)
                db.Samples.Remove(sample);
        }
    }
}
