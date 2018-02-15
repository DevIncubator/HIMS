using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Identity.Identity;

namespace HIMS.Data.Identity.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        UserSecurityManager UserSecurityManager { get; }
        ApplicationRoleManager RoleManager { get; }
        Task SaveAsync();
    }
}
