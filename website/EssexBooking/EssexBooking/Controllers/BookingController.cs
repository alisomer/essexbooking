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

            if (cart.isEmpty())
            {
                return RedirectToAction("Index","Home");
            }

            ViewBag.TravelTypes = entities.TravelTypes;
            return View(cart);
        }

        public ActionResult Payments()
        {
            Cart cart = new Cart();
            ViewBag.cart = cart;

            Dictionary<Guid, decimal> hotel_amount = new Dictionary<Guid, decimal>();
            Dictionary<Guid, decimal> travel_amount = new Dictionary<Guid, decimal>();
            Dictionary<Guid, decimal> extra_amount = new Dictionary<Guid, decimal>();

            decimal total_hotel_amount = 0;
            decimal total_travel_amount = 0;
            decimal extra_price = 0;


            foreach (var booking in ViewBag.cart.GetBookings())
            {

                hotel_amount.Add(booking.id, booking.GetHotelTotal());

                travel_amount.Add(booking.id, booking.GetTravelTotal());
                extra_amount.Add(booking.id, booking.GetExtraTotal(booking.id));
            }
            ViewBag.hotel_amount = hotel_amount;
            ViewBag.travel_amount = travel_amount;
            ViewBag.extra_amount = extra_amount;
            //total_hotel_amount = hotel_amount.Values.Sum();
            //total_travel_amount = travel_amount.Values.Sum();
            ViewBag.total_amount = hotel_amount.Values.Sum() + travel_amount.Values.Sum() + extra_amount.Values.Sum();
            return View(ViewBag.cart.GetBookings());
        }
    }
}
