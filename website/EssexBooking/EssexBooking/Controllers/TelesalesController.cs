using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using EssexBooking.Models;

namespace EssexBooking.Controllers
{
    public class TelesalesController : Controller
    {
        private ASPNETDBEntities entities = new ASPNETDBEntities();
        //
        // GET: /Entities/

        public ActionResult TelesalesList()
        {

            TelesalesViewModel vm = new TelesalesViewModel();
            var firstname = vm.FirstName;
            var lastname = vm.LastName;
            var address = vm.Address;
            var postcode = vm.PostCode;
            var telephonenumber = vm.TelephoneNumber;
            var passportnumber = vm.PassportNumber;
            var dateoftravel = vm.DateOfTravel;
            var numberofguests = vm.NumberOfGuests;
            var numberofnights = vm.NumberOfNights;



            var resorts = from r in entities.Resorts
                          select r.name;
            ViewBag.ResortNames = new SelectList(resorts);

            var hoteltypes = from h in entities.HotelTypes
                             select h.name;
            ViewBag.HotelNames = new SelectList(hoteltypes);

            var extras = from e in entities.Extras
                         select e.name;
            ViewBag.Extras = new SelectList(extras);

            var traveltypes = from t in entities.TravelTypes
                              select t.name;
            ViewBag.TravelType = new SelectList(traveltypes);

            return View(new TelesalesViewModel());
        }

    }
}
