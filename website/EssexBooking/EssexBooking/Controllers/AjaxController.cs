using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssexBooking.Models;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EssexBooking.Controllers
{
    public class AjaxController : Controller
    {


        [HttpPost]
        public ActionResult AddExtraToBooking(Guid booking_id, int extra_id, int number, DateTime extra_date)
        {
            Cart cart = new Cart();
            ExtraBooking extraBooking = new ExtraBooking();
            extraBooking.extra_id = extra_id;
            extraBooking.participants = number;
            extraBooking.booked_date = extra_date;
            Extra extra = cart.ctx.Extras.FirstOrDefault(x => x.id == extra_id);
            extraBooking.Extra = extra;
            cart.GetBooking(booking_id).ExtraBookings.Add(extraBooking);
            return PartialView("_ExtraBookingCartPartial", cart.GetBooking(booking_id).ExtraBookings);
        }


        public ActionResult AddHotelToCart(int hotel_id)
        {
            Cart cart = new Cart();
            Booking newbooking = new Booking
            {
                id = Guid.NewGuid(),
                Hotel = cart.ctx.Hotels.FirstOrDefault(h => h.id == hotel_id)
            };

            //init empty travel
            Travel travel = new Travel
            {
                id = Guid.NewGuid()
            };

            cart.ctx.AddToTravels(travel);
            newbooking.Travel = travel;

            newbooking.Travel.Passangers.Add(new Passanger { id = Guid.NewGuid() });//at least one passanger

            cart.ctx.Bookings.AddObject(newbooking);

            return PartialView("_CartPartial", cart);
        }
       
        public ActionResult RemoveBookingFromCart(Guid booking_id)
        {
            
            Cart cart = new Cart();
            cart.ctx.DeleteObject(cart.GetBooking(booking_id));

            return PartialView("_CartPartial", cart);
        }



        [HttpPost]
        public JsonResult UpdateBooking(BookingRequest br)
        {

            Cart cart = new Cart();

            BookingValidator v = new BookingValidator();
            v.Validate(br);

            List<String> errors = v.errors;

            foreach (ModelState state in ModelState.Values)
                foreach (ModelError error in state.Errors)
                    errors.Add(error.ErrorMessage);

            Boolean success = errors.Count == 0;

            if (success)
            {
                Booking b = cart.GetBooking(br.booking_id);
                b.guests = br.guests;
                b.duration = br.duration;
                b.start_date = br.start_date;

                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("TelesaleStaff"))
                    {
                        b.booker_id = (System.Guid)Membership.GetUser().ProviderUserKey;
                        Customer c = (Customer) System.Web.HttpContext.Current.Session["customer"];

                        b.customer_id = c.MembershipID;
                    }
                    else//normal customer
                    {
                        b.customer_id = (System.Guid)Membership.GetUser().ProviderUserKey;
                    }
                }

                b.Travel.TravelType = cart.ctx.TravelTypes.SingleOrDefault(t => t.id == br.travel_type_id);
                b.Travel.departure = br.start_date;
                b.Travel.arrival = br.start_date;
            }
            return Json(new { success = success, id = br.booking_id , errors = errors});
        }

        public ActionResult SetGuests(Guid booking_id, int guests)
        {

            Cart cart = new Cart();
            Booking b = cart.GetBooking(booking_id);
            
            foreach(Passanger p in b.Travel.Passangers.ToList()){
                b.Travel.Passangers.Remove(p);
                cart.ctx.DeleteObject(p);
            }

            //b.Travel.Passangers.Clear();
            for(int i=0; i<guests;i++){
                b.Travel.Passangers.Add(new Passanger { id = Guid.NewGuid() });
            }
            

            return PartialView("_PassengersFormPartial", b.Travel.Passangers);
        }

        public ActionResult SetRooms(Guid booking_id, int guests)
        {
            Cart cart = new Cart();
            cart.GetBooking(booking_id).guests = guests;
            return PartialView("_RoomsFormPartial", guests);
        }

        public ActionResult SetDoubles(Guid booking_id, int rooms, int guests)
        {
            Cart cart = new Cart();
            return PartialView("_DoublesFormPartial", guests-rooms);
        }

        [HttpPost]
        public JsonResult AddPassengers(Passanger passenger)
        {
            if (ModelState.IsValid)
            {

                Cart cart = new Cart();
                Booking booking = cart.GetBookings().SingleOrDefault(b => b.travel_id == passenger.travel_id);
                Passanger existing_passenger = booking.Travel.Passangers.SingleOrDefault(p => p.id == passenger.id);
                //existing_passenger = passenger;
                existing_passenger.first_name = passenger.first_name;
                existing_passenger.last_name = passenger.last_name;
                existing_passenger.passaport_no = passenger.passaport_no;

                String nextpage = "/Booking/Payments";
                if (booking.customer_id == Guid.Parse("00000000-0000-0000-0000-000000000000"))//dummy id
                {
                    nextpage = "/Account/Register";
                }

                return Json(new { success = true, id = existing_passenger.id , nextpage = nextpage});
            }
            return Json(new { success = false });
        }


        public JsonResult Checkout(CreditCard creditCard)
        {

            Cart cart = new Cart();

            if (User.IsInRole("TelesaleStaff"))
            {
                Customer c = (Customer)System.Web.HttpContext.Current.Session["customer"];
                MembershipCreateStatus createStatus;
                Membership.CreateUser(c.FirstName + c.LastName, "dummypassword", "", null, null, true, c.MembershipID, out createStatus);
                if (createStatus == MembershipCreateStatus.Success)
                {
                    cart.ctx.AddToCustomers(c);
                }
            }

            List<string> reasons = new List<string>();
            if (ModelState.IsValid)//Check if credit card is ok
            {
                
                var cost = cart.GetCartTotal();

                if (creditCard.isValid() && creditCard.hasAmount(cost)) //confirm that the money is there
                {
                    if (cart.Checkout())
                    {
                        creditCard.Charge(cost);
                        return Json(new { success = true , });
                    }
                    else
                    {
                        return Json(new { success = false , errors = cart.WhyNotValid});
                    }
                }
                else
                {

                    
                    reasons.Add("We havent ben able to charge your credit card. ");
                    return Json(new { success = false, errors = reasons });
                }
            }
            else
            {
                //This means that something is not valid with the card. 
                reasons.Add("The card details you have entered are not valid. ");
                return Json(new { success = false, errors = reasons });
            }

        }

    }
}
