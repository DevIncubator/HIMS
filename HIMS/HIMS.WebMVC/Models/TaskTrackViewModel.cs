﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HIMS.WebMVC.Models
{
    public class TaskTrackViewModel
    {
        public int TaskTrackId { get; set; }
        [DisplayName("Date")]
        public DateTime TrackDate { get; set; }
        [DisplayName("Note")]
        public string TrackNote { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public int UserTaskId { get; set; }

    }
}