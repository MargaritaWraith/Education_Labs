using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Education.WEB.Components
{
    public class UserViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke() => User.Identity.IsAuthenticated ? View("UserInfo") : View();
    }
}
