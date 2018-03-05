using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Interfaces;
using HIMS.Data.Interfaces;
using HIMS.BusinessLogic.Infrastructure;
using HIMS.Data.EntityClasses;

namespace HIMS.BusinessLogic.Services
{
    public class VUserTrackService : IVUserTrackService
    {
        private IUnitOfWork Database;

        public VUserTrackService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public VUserTrackTransferModel Get(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The VUserProfile's id value is not set", String.Empty);

            var VUserTrack = Database.VUserTracks.Get(id.Value);
            return Mapper.Map<VUserTrack, VUserTrackTransferModel>(VUserTrack);
        }

        public IEnumerable<VUserTrackTransferModel> GetVUserTrack(int? UserId)
        {
            if(!UserId.HasValue)
                throw  new ValidationException("The User's id value is not set", String.Empty);

            return Mapper.Map<IEnumerable<VUserTrack>, List<VUserTrackTransferModel>>(Database.VUserTracks.GetByUserId(UserId.Value));

        }

        public void UpdateUserTrack(VUserTrackTransferModel itemDto)
        {
            var item = Database.VUserTracks.Get(itemDto.TaskTrackId);
            itemDto.TrackDate = DateTime.Now;

            if (item != null)
            {
                Mapper.Map(itemDto, item);
                Database.Save();
            }
        }
    }
}
