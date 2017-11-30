using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Identity.Repositories;
using HIMS.Data.Interfaces;
using HIMS.Data.Repositories;
using Ninject.Modules;

namespace HIMS.BusinessLogic.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string _connectionString;
        private string _identityConnectionString;
        public ServiceModule(string connection, string identityConnection)
        {
            _connectionString = connection;
            _identityConnectionString = identityConnection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(_connectionString);
            Bind<Data.Identity.Interfaces.IUnitOfWork>().To<IdentityUnitOfWork>().WithConstructorArgument(_identityConnectionString);
        }
    }
}
