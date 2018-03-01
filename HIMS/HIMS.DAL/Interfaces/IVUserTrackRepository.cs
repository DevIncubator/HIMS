using System.Collections.Generic;
using HIMS.Data.Interfaces;
using HIMS.Data.EntityClasses;

namespace HIMS.DAL.Interfaces
{
    public interface IVUserTrackRepository<T> : IRepository<T> where T:class
    {
        IEnumerable<VUserTrack>  GetByUserId(int userId);
    }
}