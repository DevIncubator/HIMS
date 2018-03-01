using System.Collections;
using System.Collections.Generic;
using HIMS.BusinessLogic.DTO;
using HIMS.Data.EntityClasses;

namespace HIMS.BusinessLogic.Interfaces
{
    public interface ITaskTrackService
    {
        IEnumerable<TaskTrackTransferModel> GetTaskTracks(int UserId);
        void SaveTaskTrack(TaskTrackTransferModel item);
        void UpdateTaskTrack(TaskTrackTransferModel item);
        void DeleteTaskTrack(int? id);
        TaskTrackTransferModel GetTaskTrack(int? id);
    }
}