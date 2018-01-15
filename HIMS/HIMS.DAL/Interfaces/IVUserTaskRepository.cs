using HIMS.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.DAL.Interfaces
{
    public interface IVUserTaskRepository<T> : IRepository<T> where T:class
    {
        T Get(int userId, int taskId);
        void Delete(int userId, int taskId);
    }
}
