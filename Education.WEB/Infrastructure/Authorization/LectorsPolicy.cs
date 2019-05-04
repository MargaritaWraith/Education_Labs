using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education.Entityes.EF.Identity;

namespace Education.WEB.Infrastructure.Authorization
{
    internal class LectorsPolicy : RolesPolicy
    {
        public LectorsPolicy() : base(Role.Lector, Role.Admin) { }
    }
}
