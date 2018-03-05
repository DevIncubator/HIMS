using System.Collections.Generic;
using HIMS.Data.EntityClasses;
using HIMS.Data.Interfaces;

namespace HIMS.DAL.Interfaces
{
    public interface IVUserProfileRepository<T> : IRepository<T> where T : class
    {
        IEnumerable<VUserProfile> GetByUserEmail(string Email);
    }
}