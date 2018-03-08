using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HIMS.WebMVC.Models
{
    public class VUserTrackViewModel
    {
        public int TaskTrackId { get; set; }
        [DisplayName("Track date")]
        public DateTime TrackDate { get; set; }
        [DisplayName("Track note")]
        public string TrackNote { get; set; }
        [DisplayName("Task name")]
        public string Name { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
    }
}