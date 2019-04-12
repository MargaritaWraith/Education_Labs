using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Education.WEB.Components
{
    public class UserViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (User.Identity.Name == null)
                return View();

            return View("UserInfo");
        }
    }
}
