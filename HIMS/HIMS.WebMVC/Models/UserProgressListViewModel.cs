using System.Collections.Generic;

namespace HIMS.WebMVC.Models
{
    public class UserProgressListViewModel
    {
        public IEnumerable<UserProgressViewModel> UserProgressList { get; set; }
        public string UserName { get; set; }
    }
}