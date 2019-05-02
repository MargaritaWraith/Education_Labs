using System.Threading.Tasks;
using Education.DAL.Context;
using Education.Entityes.EF.Identity;
using Microsoft.AspNetCore.Identity;

namespace Education.DAL.Initial
{
    public class EducationDBInit
    {
        private readonly EducationDB _db;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<Role> _RoleManager;

        public EducationDBInit(EducationDB db, UserManager<User> UserManager, RoleManager<Role> RoleManager)
        {
            _db = db;
            _UserManager = UserManager;
            _RoleManager = RoleManager;
        }

        public void Initialize()
        {
            _db.Initialize();
            IdentityInitializeAsync().Wait();
            _db.SaveChanges();
        }

        private async Task IdentityInitializeAsync()
        {
            async Task<Role> CheckRoleAsync(string RoleName)
            {
                var role = await _RoleManager.FindByNameAsync(RoleName);
                if (role != null) return role;
                role = new Role { Name = RoleName };
                await _RoleManager.CreateAsync(role);

                return role;
            }

            await CheckRoleAsync(Role.Admin);
            await CheckRoleAsync(Role.User);
            await CheckRoleAsync(Role.Lector);
            await CheckRoleAsync(Role.Student);

            async Task<User> CheckUserAsync(string UserName, string Password, params string[] Roles)
            {
                var user = await _UserManager.FindByNameAsync(UserName);
                if (user is null)
                {
                    user = new User { UserName = UserName };
                    await _UserManager.CreateAsync(user, Password);
                }

                foreach (var role in Roles)
                {
                    if (await _UserManager.IsInRoleAsync(user, role)) continue;
                    await _UserManager.AddToRoleAsync(user, Role.Admin);
                }

                return user;
            }

            await CheckUserAsync(User.Admin, User.AdminDefaultPassword, Role.Admin);
        }
    }
}