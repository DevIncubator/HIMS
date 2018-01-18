using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data;
using HIMS.DAL.Interfaces;

namespace HIMS.DAL.Repositories
{
    public class ProcedureManager : IProcedureManager
    {
        private HIMSDataContext db;

        public ProcedureManager(string connectionString)
        {
            db = new HIMSDataContext(connectionString);
        }

        public int GetSampleEntriesAmount(bool isAdmin)
        {
            int result = 0;
            db.CallSampleEntriesAmount(isAdmin, ref result);
            return result;
        }

        public int CallSpDeleteUser(int id) //method is useless. I did it while try to do the same code as in a Sample (Yauheni)
        {
            int rowsAfected = db.CallSpDeleteUser(id);
            return rowsAfected;
        }

        public int DeleteUser(int userId)
        {
            int rowsAffectedToDb = db.CallSpDeleteUser(userId);
            return rowsAffectedToDb;
        }

        public int SetUserTaskAsFail(int userId, int taskId)
        {
            return db.CallSpSetUserTaskAsFail(userId, taskId);
        }

        public int SetUserTaskAsSuccess(int userId, int taskId)
        {
            return db.CallSpSetUserTaskAsSuccess(userId, taskId);
        }
    }
}
