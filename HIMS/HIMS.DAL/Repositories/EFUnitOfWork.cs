using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Interfaces;
using HIMS.Data;
using HIMS.Data.EntityClasses;
using HIMS.DAL.Interfaces;
using HIMS.DAL.Repositories;

namespace HIMS.Data.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private HIMSDataContext db;
        private SampleRepository _sampleRepository;
        private VUserProfileRepository _vUserProfileRepository;
        private VUserTaskRepository _vUserTaskRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new HIMSDataContext(connectionString);
        }
        public IRepository<Sample> Samples
        {
            get
            {
                if (_sampleRepository == null)
                    _sampleRepository = new SampleRepository(db);
                return _sampleRepository;
            }
        }

        public IRepository<VUserProfile> VUserProfiles
        {
            get
            {
                if (_vUserProfileRepository == null)
                    _vUserProfileRepository = new VUserProfileRepository(db);
                return _vUserProfileRepository;
            }
        }

        public IVUserTaskRepository<VUserTask> VUserTasks => _vUserTaskRepository == null ? new VUserTaskRepository(db) : _vUserTaskRepository;

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
