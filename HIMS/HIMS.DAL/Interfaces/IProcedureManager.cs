using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.DAL.Interfaces
{
    public interface IProcedureManager
    {
        int GetSampleEntriesAmount(bool isAdmin);
        int DeleteUser(int userId);
		int DeleteTask(int taskId);
        int DeleteTaskTrack(int taskTrackId);
        int SetUserTaskAsSuccess(int userId, int taskId);
        int SetUserTaskAsFail(int userId, int taskId);
        
    }
}
