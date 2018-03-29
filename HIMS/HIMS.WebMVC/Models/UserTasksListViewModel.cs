using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIMS.WebMVC.Models
{
    public class UserTasksListViewModel
    {
        public IEnumerable<UserTaskViewModel> UserTasksList { get; set; }
        public string UserName { get; set; }
    }
}