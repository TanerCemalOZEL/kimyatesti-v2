using kimyatesti.identity;
using kimyatesti.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kimyatesti.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        public AdminController()
        {
            userManager = new UserManager<AppUser>(new UserStore<AppUser>(new IdentityDataContext()));
        }
        // GET: Admin
        public ActionResult Index()
        {

            return View(userManager.Users);
        }
    }
}