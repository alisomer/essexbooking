using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssexBooking.Models;
using System.Web.Security;

namespace EssexBooking.Controllers
{
    public class ProfileController : Controller
    {

        ASPNETDBEntities entities = new ASPNETDBEntities();
        //
        // GET: /Profile/

        public ActionResult Index()
        {
            MembershipUser user = Membership.GetUser();
            if(user == null) return Redirect("Home");

            Guid userId = (System.Guid)user.ProviderUserKey;

            IEnumerable<Booking> bookings = entities.Bookings.Where(b => b.customer_id == userId);


            return View(bookings);
        }

    }
}
