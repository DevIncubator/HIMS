﻿using System;
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
        int SetUserTaskAsFail(int userId, int taskId);
        int SetUserTaskAsSuccess(int userId, int taskId);
		int DeleteTask(int taskId);
    }
}