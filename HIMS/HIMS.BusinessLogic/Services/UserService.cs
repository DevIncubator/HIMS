using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Infrastructure;
using HIMS.BusinessLogic.Interfaces;
using HIMS.Data.Identity.Entities;
using HIMS.Data.Identity.Interfaces;
using Microsoft.AspNet.Identity;

namespace HIMS.BusinessLogic.Services
{
    public class UserService : IUserService
    {
       private IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserTransferModel userDto)
        {
            UserSecurity user = Database.UserSecurityManager.FindByEmail(userDto.Email);
            if (user == null)
            {
                user = new UserSecurity { Email = userDto.Email, UserName = userDto.Email };
                var result = Database.UserSecurityManager.Create(user, userDto.Password);

                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                // add a role
                Database.UserSecurityManager.AddToRole(user.Id, userDto.Role);

                await Database.SaveAsync();
                return new OperationDetails(true, "The registration was done successfully!", "");
            }
            else
            {
                return new OperationDetails(false, "The user with the same name already exists!", "Email");
            }
        }

        public async Task<OperationDetails> Delete(string email)
        {
            UserSecurity user = await Database.UserSecurityManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await Database.UserSecurityManager.DeleteAsync(user);

                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                await Database.SaveAsync();
                return new OperationDetails(true, "User deletion by Email was done successfully! Email: ", email);
            }
            else
            {
                return new OperationDetails(false, "The user with such Email not found! Email: ", email);
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserTransferModel userDto)
        {
            ClaimsIdentity claim = null;
            // finding the user
            UserSecurity user = await Database.UserSecurityManager.FindAsync(userDto.Email, userDto.Password);
            // authorize and return the ClaimsIdentity object
            if (user != null)
                claim = await Database.UserSecurityManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // initial data
        public async Task SetInitialData(UserTransferModel adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
