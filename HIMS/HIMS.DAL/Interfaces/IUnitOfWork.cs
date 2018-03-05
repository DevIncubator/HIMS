using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HIMS.DAL.Interfaces;
using HIMS.Data.EntityClasses;

namespace HIMS.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Sample> Samples { get; }
        IVUserProfileRepository<VUserProfile> VUserProfiles { get; }
        IRepository<UserProfile> UserProfiles { get; }
		IRepository<VTask> VTasks { get; }
		IRepository<Task> Tasks { get; }
        IRepository<TaskTrack> TaskTracks { get; }
        IRepository<Direction> Directions { get; }
        IVUserTaskRepository<VUserTask> VUserTasks { get; }
        IVUserTrackRepository<VUserTrack> VUserTracks { get; }
        void Save();
    }
}
