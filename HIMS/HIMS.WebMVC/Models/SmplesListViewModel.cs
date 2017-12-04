using System.Collections.Generic;

namespace HIMS.WebMVC.Models
{
    public class SmplesListViewModel
    {
        public IEnumerable<SampleViewModel> Samples { get; set; }
        public int SamplesAmount { get; set; }
    }
}