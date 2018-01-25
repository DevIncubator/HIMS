using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.DAL.Interfaces;
using HIMS.Data.EntityClasses;

namespace HIMS.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Sample> Samples { get; }
        IRepository<VUserProfile> VUserProfiles { get; }
        IRepository<UserProfile> UserProfiles { get; }
        IRepository<Direction> Directions { get; }
        IVUserTaskRepository<VUserTask> VUserTasks { get; }
        void Save();
    }
}
