using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Identity.Entities;
using Microsoft.AspNet.Identity;

namespace HIMS.Data.Identity.Identity
{
    public class UserSecurityManager : UserManager<UserSecurity>
    {
        public UserSecurityManager(IUserStore<UserSecurity> store)
                : base(store)
        {
        }
    }
}
