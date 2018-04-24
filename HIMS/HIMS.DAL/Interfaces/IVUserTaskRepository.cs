using HIMS.Data.Interfaces;
using System.Collections.Generic;

namespace HIMS.DAL.Interfaces
{
    public interface IVUserTaskRepository<T> : IRepository<T> where T:class
    {
        void Delete(int userId, int taskId);
        T Get(int userId, int taskId);
        IEnumerable<T> Find(int? id);
    }
}