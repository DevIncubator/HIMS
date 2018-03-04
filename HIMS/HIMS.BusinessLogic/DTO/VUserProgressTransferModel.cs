using System;

namespace HIMS.BusinessLogic.DTO
{
    public class VUserProgressTransferModel
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TrackNote { get; set; }
        public DateTime TrackDate { get; set; }
    }
}