using System;
using System.Linq;
using System.Web.Mvc;
using EssexBooking.Models;

namespace EssexBooking.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "TelesaleStaff")]
    public class HomeController : Controller
    {
        private ASPNETDBEntities db = new ASPNETDBEntities();
        //
        // GET: /Admin/Home/

        public ActionResult Index()
        {
            ViewBag.LastBookings = db.Bookings.OrderByDescending(b => b.id).Take(5);
            ViewBag.Today = db.Bookings.Where(b => b.start_date == DateTime.Today);
            return View();
        }
    }
}
