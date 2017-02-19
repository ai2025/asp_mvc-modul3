using System.Web;
using System.Web.Mvc;
using asp_mvc_2.Models.DB;
using asp_mvc_2.Models.EntityManager;

namespace asp_mvc_2.Security
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        private readonly string[] userAssignedRole;
        public AuthorizeRoleAttribute(params string[] role)
        {
            this.userAssignedRole = role;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            using (DemoDBEntities db = new DemoDBEntities())
            {
                UserManager UM = new UserManager();
                foreach (var role in userAssignedRole)
                {
                    authorize = UM.IsUserInRole(httpContext.User.Identity.Name, role);

                    if (authorize)
                        return authorize;
                }
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Home/UnAuthorized");
        }
    }
}