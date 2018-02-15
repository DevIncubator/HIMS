using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Identity.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HIMS.Data.Identity.EF
{
    public class IdentityContext : IdentityDbContext<UserSecurity>
    {
        public IdentityContext(string conectionString) : base(conectionString) { }


    }
}
