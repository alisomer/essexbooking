using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using EssexBooking.Models;

namespace EssexBooking.Areas.Admin.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(IEnumerable<aspnet_Users> users, IEnumerable<aspnet_Roles> roles)
        {
            UsersList = users;
            RolesList = roles;

            Admins = new List<string>();
            Staff = new List<string>();

            foreach (var user in UsersList)
            {
                if (Roles.IsUserInRole(user.UserName, "Admin"))
                    Admins.Add(user.UserName);
                if (Roles.IsUserInRole(user.UserName, "TelesaleStaff"))
                    Staff.Add(user.UserName);
            }
        }
        public IEnumerable<aspnet_Users> UsersList { get; set; }
        public IEnumerable<aspnet_Roles> RolesList { get; set; }
        public List<String> Admins { get; set; }
        public List<String> Staff { get; set; }
    }
}
