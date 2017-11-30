using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Infrastructure;

namespace HIMS.BusinessLogic.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserTransferModel userDto);
        Task<ClaimsIdentity> Authenticate(UserTransferModel userDto);
        Task SetInitialData(UserTransferModel adminDto, List<string> roles);
    }
}
