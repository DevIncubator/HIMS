using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Infrastructure;
using HIMS.Data.EntityClasses;
using HIMS.Data.Interfaces;
using HIMS.BusinessLogic.Interfaces;

namespace HIMS.BusinessLogic.Services
{
    public class SampleService : ISampleService
    {
        IUnitOfWork Database { get; set; }

        public SampleService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void SaveSample(SampleTransferModel sampleDTO)
        {
            // Validation
            if (sampleDTO.Name.Length > 25)
                throw new ValidationException($"The length of {nameof(sampleDTO.Name)} must be less then 25"
                    , nameof(sampleDTO.Name));
            if (sampleDTO.Description.Length > 255)
                throw new ValidationException($"The length of {nameof(sampleDTO.Description)} must be less then 25"
                    , nameof(sampleDTO.Description));

            var sample = new Sample
            {
                Name = sampleDTO.Name,
                Description = sampleDTO.Description
            };

            Database.Samples.Create(sample);
            Database.Save();
        }
        public void UpdateSample(SampleTransferModel sampleDTO)
        {
            // Validation
            if (sampleDTO.Name.Length > 25)
                throw new ValidationException($"The length of {nameof(sampleDTO.Name)} must be less then 25"
                    , nameof(sampleDTO.Name));
            if (sampleDTO.Description.Length > 255)
                throw new ValidationException($"The length of {nameof(sampleDTO.Description)} must be less then 25"
                    , nameof(sampleDTO.Description));

            var sample = Database.Samples.Get(sampleDTO.SampleId);

            if (sample != null)
            {
                Mapper.Map(sampleDTO, sample);
                Database.Save();
            }           
        }

        public IEnumerable<SampleTransferModel> GetSamples()
        {
            return Mapper.Map<IEnumerable<Sample>, List<SampleTransferModel>>(Database.Samples.GetAll());
        }

        public SampleTransferModel GetSample(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The Sample's id value is not set", String.Empty);

            var sample = Database.Samples.Get(id.Value);

            if (sample == null)
                throw new ValidationException($"The Sample with id = {id} was not found", String.Empty);
          
            return Mapper.Map<Sample, SampleTransferModel>(sample);
        }

        public void DeleteSample(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The Sample's id value is not set", String.Empty);

            Database.Samples.Delete(id.Value);
            Database.Save();
        }

        public void SaveChanges()
        {
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
