﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIMS.WebMVC.Models
{
    public class TaskTrackDeleteViewModel
    {
        public int TaskTrackId { get; set; }
        public DateTime TrackDate { get; set; }
        public string TrackNote { get; set; }
        public int UserTaskId { get; set; }
    }
}