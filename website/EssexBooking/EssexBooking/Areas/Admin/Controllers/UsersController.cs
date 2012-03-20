using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using EssexBooking.Areas.Admin.ViewModels;
using EssexBooking.Models;

namespace EssexBooking.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "TelesaleStaff")]
    public class UsersController : Controller
    {
        public ASPNETDBEntities db = new ASPNETDBEntities();

        //
        // GET: /Admin/Users/

        public ActionResult Index()
        {
            return View(new UserViewModel(db.aspnet_Users, db.aspnet_Roles));
        }

        public ActionResult MakeStaff(Guid id)
        {
            var user = Membership.GetUser(id);
            if (user != null)
            {
                Roles.AddUserToRole(user.UserName, "TelesaleStaff");
            }
            return RedirectToAction("Index");
        }

        public ActionResult RemoveStaff(Guid id)
        {
            var user = Membership.GetUser(id);
            if (user != null)
            {
                Roles.RemoveUserFromRole(user.UserName, "TelesaleStaff");
            }
            return RedirectToAction("Index");
        }

        public ActionResult MakeAdmin(Guid id)
        {
            var user = Membership.GetUser(id);
            if (user != null)
            {
                Roles.AddUserToRole(user.UserName, "Admin");
            }
            return RedirectToAction("Index");
        }

        public ActionResult RemoveAdmin(Guid id)
        {
            var user = Membership.GetUser(id);
            if (user != null)
            {
                Roles.RemoveUserFromRole(user.UserName, "Admin");
            }
            return RedirectToAction("Index");
        }
    }
}
