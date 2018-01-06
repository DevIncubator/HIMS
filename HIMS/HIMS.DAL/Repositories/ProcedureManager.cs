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

        public int DeleteUser(int userId)
        {
            int rowsAffectedToDb = db.CallSpDeleteUser(userId);
            return rowsAffectedToDb;
        }
    }
}
