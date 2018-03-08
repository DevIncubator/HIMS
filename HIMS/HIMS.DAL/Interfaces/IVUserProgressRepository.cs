using HIMS.Data.EntityClasses;
using System.Collections.Generic;

namespace HIMS.DAL.Interfaces
{
    public interface IVUserProgressRepository
    {
        IEnumerable<VUserProgress> GetProgressByUserId(int id);
    }
}