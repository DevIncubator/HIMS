using System;
using System.ComponentModel.DataAnnotations;

namespace HIMS.WebMVC.Models
{
    public class UserProgressViewModel
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TrackNote { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime TrackDate { get; set; }
    }
}