using System;
using System.Collections.Generic;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Interfaces;
using HIMS.Data.Interfaces;
using AutoMapper;
using HIMS.BusinessLogic.Infrastructure;
using HIMS.Data.EntityClasses;

namespace HIMS.BusinessLogic.Services
{
    public class TaskTrackService : ITaskTrackService
    {
        private IUnitOfWork Database { get; set; }

        public TaskTrackService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public IEnumerable<TaskTrackTransferModel> GetTaskTracks()
        {    
            return Mapper.Map<IEnumerable<TaskTrack>, List<TaskTrackTransferModel>>(Database.TaskTracks.GetAll());
        }

        public void SaveTaskTrack(TaskTrackTransferModel itemDto)
        {
            var item = new TaskTrack
            {
                TrackDate = DateTime.Now,
                TrackNote = itemDto.TrackNote,
                UserTaskId = itemDto.UserTaskId
            };

            Database.TaskTracks.Create(item);
            Database.Save();
        }

        public void UpdateTaskTrack(TaskTrackTransferModel itemDto)
        {
            var item = Database.TaskTracks.Get(itemDto.TaskTrackId);

            if (item != null)
            {
                item = Mapper.Map<TaskTrackTransferModel, TaskTrack>(itemDto);
                Database.Save();
            }
        }

        public void DeleteTaskTrack(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The TaskTrack's id value is not set", String.Empty);

            Database.TaskTracks.Delete(id.Value);
            Database.Save();
        }

        public TaskTrackTransferModel GetTaskTrack(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The TaskTrack's id value is not set", String.Empty);

            var item = Database.TaskTracks.Get(id.Value);

            if (item == null)
                throw new ValidationException($"The TaskTrack's with id = {id} was not found", String.Empty);

            return Mapper.Map<TaskTrack, TaskTrackTransferModel>(item);
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
