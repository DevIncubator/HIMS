using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIMS.WebMVC.Models
{
    public class TaskTracksListViewModel
    {
        public IEnumerable<TaskTrackViewModel> TaskTracks { get; set; }
    }
}