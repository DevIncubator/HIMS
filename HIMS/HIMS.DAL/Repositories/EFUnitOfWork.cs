using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private UserProfileRepository _userProfileRepository;
        private DirectionRepository _directionRepository;
		private VTaskRepository _vTaskRepository;
		private TaskRepository _taskRepository;

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

		public IRepository<VTask> VTasks
		{
			get
			{
				if (_vTaskRepository == null)
					_vTaskRepository = new VTaskRepository(db);
				return _vTaskRepository;
			}
		}

		public IRepository<Task> Tasks
		{
			get
			{
				if (_taskRepository == null)
					_taskRepository = new TaskRepository(db);
				return _taskRepository;
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

        public IRepository<UserProfile> UserProfiles
        {
            get
            {
                if (_userProfileRepository == null)
                    _userProfileRepository = new UserProfileRepository(db);
                return _userProfileRepository;
            }
        }

        public IRepository<Direction> Directions
        {
            get
            {
                if (_directionRepository == null)
                    _directionRepository = new DirectionRepository(db);
                return _directionRepository;
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
