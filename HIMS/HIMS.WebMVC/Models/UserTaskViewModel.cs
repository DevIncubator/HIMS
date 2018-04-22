using System;
using System.ComponentModel.DataAnnotations;

namespace HIMS.WebMVC.Models
{
    public class UserTaskViewModel
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh\\:mm}")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh\\:mm}")]
        public DateTime DeadlineDate { get; set; }
        public string State { get; set; }
    }
}