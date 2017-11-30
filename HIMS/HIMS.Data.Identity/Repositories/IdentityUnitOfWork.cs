using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Identity.EF;
using HIMS.Data.Identity.Entities;
using HIMS.Data.Identity.Identity;
using HIMS.Data.Identity.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HIMS.Data.Identity.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private IdentityContext _db;

        private UserSecurityManager _userManager;
        private ApplicationRoleManager _roleManager;

        public IdentityUnitOfWork(string connectionString)
        {
            _db = new IdentityContext(connectionString);
            _userManager = new UserSecurityManager(new UserStore<UserSecurity>(_db));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_db));
        }

        public UserSecurityManager UserSecurityManager
        {
            get { return _userManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager; }
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _userManager.Dispose();
                    _roleManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
