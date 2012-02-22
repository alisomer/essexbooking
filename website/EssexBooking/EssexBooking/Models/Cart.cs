using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace EssexBooking.Models
{
    public class Cart
    {
        private List<Booking> bookings;

        public Cart()
        {
            Cart c = this.GetFromSession();
            if (c != null)
            {
                bookings = c.Bookings;
            }
            else
            {
                bookings = new List<Booking>();
                this.AddToSession();
            }
        }

        void AddToSession()
        {
            System.Web.HttpContext.Current.Session["cart"] = this;
        }

        Cart GetFromSession()
        {
            return (Cart)System.Web.HttpContext.Current.Session["cart"];
        }

        public void Empty()
        {
            foreach (Booking b in bookings)
            {
                bookings.Remove(b);
            }
        }

        public void Checkout()
        {
            ASPNETDBEntities entities = new ASPNETDBEntities();
            foreach (Booking b in bookings)
            {
                entities.Bookings.AddObject(b);
                entities.Travels.AddObject(b.Travel);
                foreach (ExtraBooking e in b.Extras)
                {
                    entities.ExtraBookings.AddObject(e);
                }
            }
            entities.SaveChanges();
        }

        public List<Booking> Bookings
        {
            get { return bookings; }
        }
    }
}