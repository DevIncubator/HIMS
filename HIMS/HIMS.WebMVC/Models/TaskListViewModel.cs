using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIMS.WebMVC.Models
{
    public class TaskListViewModel
    {
        public IEnumerable<TaskViewModel> Tasks { get; set; }
    }
}