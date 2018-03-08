using System;

namespace HIMS.WebMVC.Models
{
    public class UserProgressViewModel
    {
        public int TaskId { get; set; }
        public string UserName { get; set; }
        public string TaskName { get; set; }
        public string TrackNote { get; set; }
        public DateTime TrackDate { get; set; }
    }
}