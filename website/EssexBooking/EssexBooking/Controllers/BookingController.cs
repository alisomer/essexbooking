using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssexBooking.Models;

namespace EssexBooking.Controllers
{
    public class BookingController : Controller
    {
        //
        // GET: /Booking/

        ASPNETDBEntities entities = new ASPNETDBEntities();

        public ActionResult Index()
        {
            Cart cart = new Cart();
            ViewBag.TravelTypes = entities.TravelTypes;
            return View(cart);
        }

    }
}
