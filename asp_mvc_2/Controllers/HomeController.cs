using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using asp_mvc_2.Security;
using System.Web.Security;
using asp_mvc_2.Models.ViewModel;
using asp_mvc_2.Models.EntityManager;

namespace asp_mvc_2.Controllers
{
    public class HomeController : Controller
    {
        [AuthorizeRole("Admin")]
        public ActionResult DeleteUser(int userID)
        {
            UserManager UM = new UserManager();
            UM.DeleteUser(userID);
            return Json(new { success = true });
        }
        [AuthorizeRole("Admin")]
        public ActionResult UpdateUserData(int userID, string loginName, string password, string firstName, string lastName, string gender, int roleID = 0)
        {
            UserProfileView UPV = new UserProfileView();
            UPV.SYSUserID = userID;
            UPV.LoginName = loginName;
            UPV.Password = password;
            UPV.FirstName = firstName;
            UPV.LastName = lastName;
            UPV.Gender = gender;

            if (roleID > 0)
                UPV.LOOKUPRoleID = roleID;

            UserManager UM = new UserManager();
            UM.UpdateUserAccount(UPV);

            return Json(new { sucess = true });
        }
        [AuthorizeRole("Admin")]
        public ActionResult ManageUserPartial(string status = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                string loginName = User.Identity.Name;
                UserManager UM = new UserManager();
                UserDataView UDV = UM.GetUserDataView(loginName);

                string message = string.Empty;
                if (status.Equals("update"))
                    message = "Update successfull";
                else if (status.Equals("delete"))
                    message = "Delete successful";

                ViewBag.Message = message;

                return PartialView(UDV);
            }
            return RedirectToAction("Index", "Home");
        }
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