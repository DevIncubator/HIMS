using System;

namespace HIMS.BusinessLogic.DTO
{
    public class UserTaskTransferModel
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public string State { get; set; }
    }
}
