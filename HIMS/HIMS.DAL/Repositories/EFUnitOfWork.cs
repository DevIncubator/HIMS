using HIMS.DAL.Interfaces;
using HIMS.DAL.Repositories;
using HIMS.Data.EntityClasses;
using HIMS.Data.Interfaces;
using System;

namespace HIMS.Data.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private HIMSDataContext db;
        private SampleRepository _sampleRepository;
        private UserProfileRepository _userProfileRepository;

        private UserTaskRepository _userTaskRepository;
        private TaskTrackRepository _taskTrackRepository;
        private DirectionRepository _directionRepository;
        private TaskRepository _taskRepository;

        private VUserProfileRepository _vUserProfileRepository;
        private VUserProgressRepository _vUserProgressRepository;
        private VUserTaskRepository _vUserTaskRepository;
        private VUserTrackRepository _vUserTrackRepository;
        private VTaskRepository _vTaskRepository;


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
                {
                    _vTaskRepository = new VTaskRepository(db);
                }
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

        public IRepository<VUserProgress> VUserProgress
        {
            get
            {
                if (_vUserProgressRepository == null)
                    _vUserProgressRepository = new VUserProgressRepository(db);
                return _vUserProgressRepository;
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

        public IRepository<UserTask> UserTask
        {
            get
            {
                if (_userTaskRepository == null)
                    _userTaskRepository = new UserTaskRepository(db);
                return _userTaskRepository;
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

        public IRepository<TaskTrack> TaskTrack
        {
            get
            {
                if(_taskTrackRepository == null)
                    _taskTrackRepository = new TaskTrackRepository(db);
                return _taskTrackRepository;
            }
        }
    
		public IVUserTaskRepository<VUserTask> VUserTasks => _vUserTaskRepository == null ? new VUserTaskRepository(db) : _vUserTaskRepository;

        public IRepository<Direction> Direction
        {
            get
            {
                if (_directionRepository == null)
                    _directionRepository = new DirectionRepository(db);
                return _directionRepository;
            }
        }

        public IVUserTaskRepository<VUserTrack> VUserTrack
        {
            get
            {
                throw new NotImplementedException();
            }
        }
      
        public IRepository<TaskTrack> TaskTracks
        {
            get
            {
                throw new NotImplementedException();
            }
        }

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
