using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.BusinessLogic.DTO;

namespace HIMS.BusinessLogic.Interfaces
{
    public interface ISampleService
    {
        void SaveSample(SampleTransferModel sampleTM);
        SampleTransferModel GetSample(int? id);
        IEnumerable<SampleTransferModel> GetSamples();
        void UpdateSample(SampleTransferModel sampleDTO);
        void DeleteSample(int? id);
        int GetSampleEntriesAmout(bool isAdmin);
        void Dispose();
    }
}
