using HIMS.Data.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BusinessLogic.DTO
{
    public class UserTaskTransferModel
    {
        public int userId { get; set; }
        public int taskId { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime Deadline { get; set; }
        public TaskState Status { get; set; }
    }
}
