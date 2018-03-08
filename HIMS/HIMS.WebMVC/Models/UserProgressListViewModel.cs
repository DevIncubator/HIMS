using System.Collections.Generic;

namespace HIMS.WebMVC.Models
{
    public class UserProgressListViewModel
    {
        public IEnumerable<UserProgressViewModel> UserProgress { get; set; }
    }
}