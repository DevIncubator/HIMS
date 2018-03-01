using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BusinessLogic.DTO
{
    public class VUserTrackTransferModel
    {
        public System.String Name { get; set; }
  
        public System.Int32 TaskId { get; set; }

        public System.Int32 TaskTrackId { get; set; }

        public System.DateTime TrackDate { get; set; }

        public System.String TrackNote { get; set; }

        public System.Int32 UserId { get; set; }
    }
}
