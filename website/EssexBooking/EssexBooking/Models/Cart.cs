using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace EssexBooking.Models
{
    public class Cart
    {
        public ASPNETDBEntities ctx;

        public Cart()
        {
            Cart c = this.GetFromSession();
            if (c != null)
            {
                ctx = c.ctx;
            }
            else
            {
                ctx = new ASPNETDBEntities();
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
            ctx = new ASPNETDBEntities();
        }

        public void Checkout()
        {
            ctx.SaveChanges();
            Empty();
        }

        public void Add(Travel travel)
        {
            ctx.Travels.AddObject(travel);
        }

        public void Add(Passanger passenger)
        {
            ctx.Passangers.AddObject(passenger);
        }

        public Booking GetBooking(Guid id)
        {
            return GetBookings().FirstOrDefault(b => b.id== id);

        }

        public IEnumerable<Booking> GetBookings()
        {
            return
                ctx.ObjectStateManager.GetObjectStateEntries(EntityState.Added).Select(obj => obj.Entity).OfType
                    <Booking>();
        }

        public bool isEmpty(){
            return GetBookings().Count() == 0;
        }

        public decimal GetCartTotal()
        {
            decimal total = 0;
            foreach(Booking b in GetBookings()){
                total += b.GetBookingTotal();
            }
            return total;

        }

    }
}