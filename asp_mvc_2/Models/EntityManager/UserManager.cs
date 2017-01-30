using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using asp_mvc_2.Models.DB;
using asp_mvc_2.Models.ViewModel;

namespace asp_mvc_2.Models.EntityManager
{
    public class UserManager
    {
        public void AddUserAccount(UserSignUpView user)
        {
            using (DemoDBEntities db = new DemoDBEntities())
            {
                SYSUser SU = new SYSUser(); 
                SU.LoginName = user.LoginName; 
                SU.PasswordEncryptedText = user.Password; 
                SU.RowCreatedSYSUserID = user.SYSUserID > 0 ? user.SYSUserID : 1;
                SU.RowModifiedSYSUserID = user.SYSUserID > 0 ? user.SYSUserID : 1; 
                SU.RowCreatedDateTime = DateTime.Now; 
                SU.RowMOdifiedDateTime = DateTime.Now; 
                
                db.SYSUser.Add(SU); 
                db.SaveChanges(); 
                
                SYSUserProfile SUP = new SYSUserProfile(); 
                SUP.SYSUserID = SU.SYSUserID; 
                SUP.FirstName = user.FirstName; 
                SUP.LastName = user.LastName; 
                SUP.Gender = user.Gender; 
                SUP.RowCreatedSYSUserID = user.SYSUserID > 0 ? user.SYSUserID : 1; 
                SUP.RowModifiedSYSUserID = user.SYSUserID > 0 ? user.SYSUserID : 1; 
                SUP.RowCreatedDateTime = DateTime.Now; 
                SUP.RowModifiedDateTime = DateTime.Now; 
                
                db.SYSUserProfile.Add(SUP); 
                db.SaveChanges(); 
                
                if (user.LOOKUPRoleID > 0) { 
                    SYSUserRole SUR = new SYSUserRole(); 
                    SUR.LOOKUPRoleID = user.LOOKUPRoleID; 
                    SUR.SYSUserID = user.SYSUserID; 
                    SUR.IsActive = true; 
                    SUR.RowCreatedSYSUserID = user.SYSUserID > 0 ? user.SYSUserID : 1; 
                    
                    SUR.RowModifiedSYSUserID = user.SYSUserID > 0 ? user.SYSUserID : 1; 
                    
                    SUR.RowCreatedDateTime = DateTime.Now; 
                    SUR.RowModifiedDateTime = DateTime.Now; 
                    
                    db.SYSUserRole.Add(SUR); 
                    db.SaveChanges(); 
                }
            }
        } 
        public bool IsLoginNameExist(string loginName) { 
            using (DemoDBEntities db = new DemoDBEntities()) { 
                return db.SYSUser.Where(o => o.LoginName.Equals(loginName)).Any(); 
            } 
        }
    }
}