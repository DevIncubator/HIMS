using HIMS.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.BusinessLogic.DTO;
using HIMS.Data.Interfaces;
using HIMS.Data.EntityClasses;
using AutoMapper;
using HIMS.BusinessLogic.Infrastructure;

namespace HIMS.BusinessLogic.Services
{
    public class DirectionService : IDirectionService
    {

        private IUnitOfWork Database { get; set; }

        public DirectionService(IUnitOfWork uow)
        {
            Database = uow;
        }


        public IEnumerable<DirectionTransferModel> GetDirections()
        {
            var directions = Database.Directions.GetAll();
            return Mapper.Map<IEnumerable<Direction>, List<DirectionTransferModel>>(directions);
        }

        public DirectionTransferModel GetDirection(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The Direction's id value is not set", String.Empty);

            var direction = Database.Directions.Get(id.Value);

            if (direction == null)
                throw new ValidationException($"The Sample with id = {id} was not found", String.Empty);

            return Mapper.Map<Direction, DirectionTransferModel>(direction);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
