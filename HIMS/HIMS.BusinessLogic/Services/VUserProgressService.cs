using AutoMapper;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Interfaces;
using HIMS.Data.EntityClasses;
using HIMS.Data.Interfaces;
using System.Collections.Generic;

namespace HIMS.BusinessLogic.Services
{
    public class VUserProgressService : IVUserProgressService
    {
        private IUnitOfWork Database { get; set; }

        public VUserProgressService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<VUserProgressTransferModel> GetProgressByUserId(int id)
        {
            return Mapper.Map<IEnumerable<VUserProgress>, List<VUserProgressTransferModel>>(Database.VUserProgress.GetProgressByUserId(id));
        }
    }
}