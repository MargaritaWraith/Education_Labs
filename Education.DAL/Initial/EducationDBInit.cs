using System;
using System.Linq;
using System.Threading.Tasks;
using Education.DAL.Context;
using Education.Entityes.EF.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            _db.Database.Migrate();
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
                 var creation_result = await _RoleManager.CreateAsync(role);
                 if (!creation_result.Succeeded)
                     throw new InvalidOperationException($"Ошибка создания новоq роли {role.Name} - {string.Join(", ", creation_result.Errors.Select(error => error.Description))}");

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
                    var creation_result = await _UserManager.CreateAsync(user, Password);
                    if(!creation_result.Succeeded)
                        throw new InvalidOperationException($"Ошибка создания нового пользователя {user.Name} - {string.Join(", ", creation_result.Errors.Select(error => error.Description))}");
                }

                foreach (var role in Roles)
                {
                    if (await _UserManager.IsInRoleAsync(user, role)) continue;
                    var add_role_result = await _UserManager.AddToRoleAsync(user, role);
                    if(!add_role_result.Succeeded)
                        throw new InvalidOperationException($"Ошибка добавления роли {role} пользователю {user.Name} - {string.Join(", ", add_role_result.Errors.Select(error => error.Description))}");
                }

                var roles = await _UserManager.GetRolesAsync(user);

                return user;
            }

            await CheckUserAsync(User.Admin, User.AdminDefaultPassword, Role.Admin);

            await CheckUserAsync("TestLector", "TestLectorPass", Role.Lector);
            await CheckUserAsync("TestStudent", "TestStudentPass", Role.Student);
        }
    }
}