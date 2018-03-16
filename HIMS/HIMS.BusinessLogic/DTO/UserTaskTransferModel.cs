using System;

namespace HIMS.BusinessLogic.DTO
{
    public class UserTaskTransferModel
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
    }
}
