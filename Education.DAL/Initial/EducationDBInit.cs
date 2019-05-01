using Education.DAL.Context;
using Education.Entityes.EF.Identity;
using Microsoft.AspNetCore.Identity;

namespace Education.DAL.Initial
{
    public class EducationDBInit
    {
        private readonly EducationDB _DB;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<Role> _RoleManager;

        public EducationDBInit(EducationDB DB, UserManager<User> UserManager, RoleManager<Role> RoleManager)
        {
            _DB = DB;
            _UserManager = UserManager;
            _RoleManager = RoleManager;
        }

        public void Initialize()
        {
            _DB.Initialize();
            IdentityInitialize();
            _DB.SaveChanges();
        }

        private void IdentityInitialize()
        {
            var admin_role = _RoleManager.FindByNameAsync("admin").Result;
            if (admin_role is null)
            {
                admin_role = new Role
                {
                    Name = "admin"
                };
                _RoleManager.CreateAsync(admin_role).Wait();
            }

            var admin_user = _UserManager.FindByNameAsync("admin").Result;
            if (admin_user is null)
            {
                admin_user = new User
                {
                    UserName = "admin",
                    Email = "admin@server.ru"
                };
                var result = _UserManager.CreateAsync(admin_user, "1147").Result;
                _UserManager.AddToRoleAsync(admin_user, "admin").Wait();
            }
        }
    }
}