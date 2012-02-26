using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace EssexBooking.Models
{
    public class Cart
    {
        public Dictionary<int,Booking> bookings{get;set;}

        public Cart()
        {
            Cart c = this.GetFromSession();
            if (c != null)
            {
                bookings = c.bookings;
            }
            else
            {
                bookings = new Dictionary<int, Booking>();
                this.AddToSession();
            }
        }
        //Turns out that this method is unecessary.
        public void AddToSession()
        {
            System.Web.HttpContext.Current.Session["cart"] = this;
        }

        Cart GetFromSession()
        {
            return (Cart)System.Web.HttpContext.Current.Session["cart"];
        }

        public void Empty()
        {
            bookings = new Dictionary<int, Booking>();
        }

        public void Checkout()
        {
            ASPNETDBEntities entities = new ASPNETDBEntities();
            foreach (Booking b in bookings.Values)
            {
                entities.Bookings.AddObject(b);
                entities.Travels.AddObject(b.Travel);
               /* foreach (ExtraBooking e in b.ExtraBooking.)
                {
                    entities.ExtraBookings.AddObject(e);
                }*/
            }
            entities.SaveChanges();
        }

    }
}