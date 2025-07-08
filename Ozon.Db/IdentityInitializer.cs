using Microsoft.AspNetCore.Identity;
using Ozon.Db.Models;

namespace Ozon.Db
{
    public class IdentityInitializer
    {
        public static void Initialize(UserManager<User> usersManager, RoleManager<Role> rolesManager)
        {
            if (rolesManager.FindByNameAsync(Consts.AdminRoleName).Result == null)
            {
                rolesManager.CreateAsync(new Role(Consts.AdminRoleName)).Wait();
            }
            if (rolesManager.FindByNameAsync(Consts.UserRoleName).Result == null)
            {
                rolesManager.CreateAsync(new Role(Consts.UserRoleName)).Wait();
            }
            if (usersManager.FindByNameAsync(Consts.AdminEmail).Result == null)
            {
                var admin = new User {Email = Consts.AdminEmail, UserName = Consts.AdminEmail, EmailConfirmed = true };
                var result = usersManager.CreateAsync(admin, Consts.Password).Result;
                if (result.Succeeded)
                {
                    usersManager.AddToRoleAsync(admin, Consts.AdminRoleName).Wait();
                }
            }
        }
    }
}