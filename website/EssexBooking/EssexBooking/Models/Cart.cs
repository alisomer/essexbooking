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
        public List<string> WhyNotValid;

        public Cart()
        {
            Cart c = this.GetFromSession();
            if (c != null)
            {
                ctx = c.ctx;
                WhyNotValid = c.WhyNotValid;
            }
            else
            {
                ctx = new ASPNETDBEntities();
                WhyNotValid = new List<string>();
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

        public bool Checkout()
        {
            if (ValidCart())
            {
                //add payments for each booking
                foreach(Booking b in GetBookings()){
                    Payment p = new Payment();
                    p.id = Guid.NewGuid();
                    p.amount = b.GetBookingTotal();
                    p.payment_date = DateTime.Now;
                    p.Booking = b;
                    ctx.AddToPayments(p);
                }


                ctx.SaveChanges();
                Empty();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidCart()
        {
            WhyNotValid.Clear();

            foreach (Booking booking in GetBookings())
            {
                if (booking.customer_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))//dummy id
                {
                    WhyNotValid.Add("You must be looged in to make a booking");
                    return false;
                }
            }
            return true;
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