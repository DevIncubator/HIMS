using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BusinessLogic.DTO
{
    public class UserTaskTTransferModel
    {
        public System.Int32 StateId { get; set; }
        public System.Int32 TaskId { get; set; }
        public System.Int32 UserId { get; set; }
        public System.Int32 UserTaskId { get; set; }
    }
}
