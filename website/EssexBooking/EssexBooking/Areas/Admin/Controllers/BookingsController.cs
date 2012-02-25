using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssexBooking.Models;

namespace EssexBooking.Areas.Admin.Controllers
{ 
    public class BookingsController : Controller
    {
        private ASPNETDBEntities db = new ASPNETDBEntities();

        //
        // GET: /Admin/Bookings/

        public ViewResult Index()
        {
            var bookings = db.Bookings.Include("Extra").Include("Hotel").Include("Travel");
            return View(bookings.ToList());
        }

        //
        // GET: /Admin/Bookings/Details/5

        public ViewResult Details(int id)
        {
            Booking booking = db.Bookings.Single(b => b.id == id);
            return View(booking);
        }

        //
        // GET: /Admin/Bookings/Create

        public ActionResult Create()
        {
            ViewBag.extra_id = new SelectList(db.Extras, "id", "name");
            ViewBag.hotel_id = new SelectList(db.Hotels, "id", "name");
            ViewBag.travel_id = new SelectList(db.Travels, "id", "id");
            return View();
        } 

        //
        // POST: /Admin/Bookings/Create

        [HttpPost]
        public ActionResult Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.AddObject(booking);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.extra_id = new SelectList(db.Extras, "id", "name", booking.extra_id);
            ViewBag.hotel_id = new SelectList(db.Hotels, "id", "name", booking.hotel_id);
            ViewBag.travel_id = new SelectList(db.Travels, "id", "id", booking.travel_id);
            return View(booking);
        }
        
        //
        // GET: /Admin/Bookings/Edit/5
 
        public ActionResult Edit(int id)
        {
            Booking booking = db.Bookings.Single(b => b.id == id);
            ViewBag.extra_id = new SelectList(db.Extras, "id", "name", booking.extra_id);
            ViewBag.hotel_id = new SelectList(db.Hotels, "id", "name", booking.hotel_id);
            ViewBag.travel_id = new SelectList(db.Travels, "id", "id", booking.travel_id);
            return View(booking);
        }

        //
        // POST: /Admin/Bookings/Edit/5

        [HttpPost]
        public ActionResult Edit(Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Attach(booking);
                db.ObjectStateManager.ChangeObjectState(booking, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.extra_id = new SelectList(db.Extras, "id", "name", booking.extra_id);
            ViewBag.hotel_id = new SelectList(db.Hotels, "id", "name", booking.hotel_id);
            ViewBag.travel_id = new SelectList(db.Travels, "id", "id", booking.travel_id);
            return View(booking);
        }

        //
        // GET: /Admin/Bookings/Delete/5
 
        public ActionResult Delete(int id)
        {
            Booking booking = db.Bookings.Single(b => b.id == id);
            return View(booking);
        }

        //
        // POST: /Admin/Bookings/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Booking booking = db.Bookings.Single(b => b.id == id);
            db.Bookings.DeleteObject(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}