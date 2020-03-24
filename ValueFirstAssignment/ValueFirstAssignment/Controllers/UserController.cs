using ValueFirstAssignment.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ValueFirstAssignment.DataAccess;
using System.Text;

namespace ValueFirstAssignment.Controllers
{
    
    public class UserController : Controller
    {

        [CustomAuthorize(Roles = nameof(RoleEnum.Admin)+","+ nameof(RoleEnum.Supervisor))]
        [HttpGet]
        public ActionResult RegisterUser()
        {
            var model=AuthenticationDB.GetUsers();
            if (User.IsInRole(nameof(RoleEnum.Supervisor)))
            {
                int adminRole = (int)RoleEnum.Admin;
                model = model.Where(x => !x.Roles.Select(y => y.RoleId).Contains(adminRole)).ToList();
            }
            // View Model Conversation here....
            return View(model);
        }


        [CustomAuthorize(Roles = nameof(RoleEnum.Admin))]
        [HttpGet]
        public ActionResult RegisterUserEdit(int id)
        {
            var model = AuthenticationDB.GetUserById(id);
            // View Model Conversation here....
            return View(model);
        }

        
        [CustomAuthorize(Roles = nameof(RoleEnum.Admin))]
        
        public ActionResult RegisterUserEdit(User user, int id)
        {
            var model = AuthenticationDB.GetUserByEmail(user.Email);
            if (model != null)
            {
                if(model.UserId != user.UserId)
                {
                    ModelState.AddModelError("Warning Email", "Sorry: Email already Exists");
                    return View(user);
                }
            }
            else
            {
                 model = AuthenticationDB.GetUserById(user.UserId);
            }
            model.FullName = user.FullName;
            model.Email = user.Email;
            model.IsActive = user.IsActive;
            model.Phone = user.Phone;
            model.CommunicationAddress = user.CommunicationAddress;
            AuthenticationDB.Save(model);

            return RedirectToAction("RegisterUser");
        }

        [CustomAuthorize(Roles = nameof(RoleEnum.Admin) + "," + nameof(RoleEnum.Supervisor))]
        
        public ActionResult ChangeStatus(int id)
        {
            var model = AuthenticationDB.GetUserById(id);
            model.IsActive = !model.IsActive;
            AuthenticationDB.Save(model);
            return RedirectToAction("RegisterUser");
        }

        [CustomAuthorize(Roles = nameof(RoleEnum.Agent))]
        public ActionResult UserProfile()
        {
            var name = User.Identity.Name;
            var model = AuthenticationDB.GetUserByEmail(name);
            // View Model Conversation here....
            return View(model);
        }
    }
}