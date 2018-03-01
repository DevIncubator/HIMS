using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Interfaces;
using HIMS.Data.Interfaces;
using HIMS.DAL.Interfaces;
using AutoMapper;
using HIMS.BusinessLogic.Infrastructure;
using HIMS.Data.EntityClasses;

namespace HIMS.BusinessLogic.Services
{
    public class TaskTrackService : ITaskTrackService
    {
        private IUnitOfWork Database { get; set; }
        private IProcedureManager Pm { get; set; }

        public TaskTrackService(IUnitOfWork uow, IProcedureManager pm)
        {
            Database = uow;
            Pm = pm;
        }
        public IEnumerable<TaskTrackTransferModel> GetTaskTracks(int UserId)
        {
            
            return Mapper.Map<IEnumerable<TaskTrack>, List<TaskTrackTransferModel>>(Database.TaskTracks.GetAll());
        }

        public void SaveTaskTrack(TaskTrackTransferModel itemDTO)
        {
            var item = new TaskTrack
            {
                TrackDate = itemDTO.TrackDate,
                TrackNote = itemDTO.TrackNote,
                UserTaskId = itemDTO.UserTaskId
            };

            Database.TaskTracks.Create(item);
            Database.Save();
        }

        public void UpdateTaskTrack(TaskTrackTransferModel itemDTO)
        {
            var item = Database.TaskTracks.Get(itemDTO.TaskTrackId);

            if (item != null)
            {
                Mapper.Map(itemDTO, item);
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
