using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIMS.WebMVC.Models
{
    public class UserTaskViewModel
    {
        public int userId { get; set; }
        public int taskId { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
    }
}