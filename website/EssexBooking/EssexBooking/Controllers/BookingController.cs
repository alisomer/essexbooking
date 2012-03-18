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

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            Cart cart = new Cart();
            if (cart.isEmpty()) requestContext.HttpContext.Response.Redirect("/home/");

            base.Initialize(requestContext);
            
        }


        public ActionResult Index()
        {
            ViewBag.TravelTypes = entities.TravelTypes;
            return View(new Cart());
        }

        public ActionResult Passengers()
        {
            return View(new Cart());
        }

        public ActionResult Payments()
        {
            return View(new Cart());
        }

        public ActionResult Checkout()
        {

            Cart cart = new Cart();

            cart.Checkout();

            return View();
        }
    }
}
