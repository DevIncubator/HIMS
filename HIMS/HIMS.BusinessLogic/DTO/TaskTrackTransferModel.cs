﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BusinessLogic.DTO
{
    public class TaskTrackTransferModel
    {
        public int TaskTrackId { get; set; }
        public DateTime TrackDate { get; set; }
        public string TrackNote { get; set; }
        public int UserTaskId { get; set; }

    }
}
