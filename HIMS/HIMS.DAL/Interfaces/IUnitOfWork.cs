using HIMS.DAL.Interfaces;
using HIMS.Data.EntityClasses;
using System;

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
        IVUserProgressRepository VUserProgress { get; }
        IVUserTaskRepository<VUserTask> VUserTasks { get; }
        IVUserTrackRepository<VUserTrack> VUserTracks { get; }
        void Save();
    }
}