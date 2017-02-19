using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using asp_mvc_2.Security;

namespace asp_mvc_2.Controllers
{
    public class HomeController : Controller
    {
        [AuthorizeRole("Admin")]
        public ActionResult AdminOnly()
        {
            return View();
        }

        public ActionResult UnAuthorized()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Welcome() { 
            return View(); 
        }
    }
}