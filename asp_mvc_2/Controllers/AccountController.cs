using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using asp_mvc_2.Models.ViewModel;
using asp_mvc_2.Models.EntityManager;

namespace asp_mvc_2.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult SignUp() { 
            return View(); 
        }

        [HttpPost]

        public ActionResult SignUp(UserSignUpView USV) { 
            if (ModelState.IsValid) { 
                UserManager UM = new UserManager(); 
                if (!UM.IsLoginNameExist(USV.LoginName)) { 
                    UM.AddUserAccount(USV); 
                    FormsAuthentication.SetAuthCookie(USV.FirstName, false); 
                    return RedirectToAction("Welcome", "Home");
                } 
                else 
                    ModelState.AddModelError("", "Login Name already taken."); 
            } 
            return View(); 
        }
    }
}