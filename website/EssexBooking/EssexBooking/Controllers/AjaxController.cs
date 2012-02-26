using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssexBooking.Models;

namespace EssexBooking.Controllers
{
    public class AjaxController : Controller
    {
        public ASPNETDBEntities entities = new ASPNETDBEntities();
        //
        // GET: /Ajax/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddHotelToCart(int hotel_id)
        {
            Cart cart = new Cart();
            Booking newbooking = new Booking();

            Random r = new Random();
            newbooking.temp_id = r.Next();//as a temp id to give ids to radios etc
            newbooking.Hotel = entities.Hotels.FirstOrDefault(h => h.id == hotel_id);
            cart.bookings.Add(newbooking);
            cart.AddToSession();
            return PartialView("_CartPartial", cart);
        }

        public ActionResult RemoveHotelFromCart(int hotel_id)
        {
            Cart cart = new Cart();
            cart.bookings.Remove(cart.bookings.Find(b => b.hotel_id == hotel_id));
            cart.AddToSession();
            return PartialView("_CartPartial", cart);
        }

    }
}
