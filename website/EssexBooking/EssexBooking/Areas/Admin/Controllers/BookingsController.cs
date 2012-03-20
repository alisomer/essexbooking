using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssexBooking.Areas.Admin.ViewModels;
using EssexBooking.Models;

namespace EssexBooking.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "TelesaleStaff")]
    public class BookingsController : Controller
    {
        private ASPNETDBEntities db = new ASPNETDBEntities();

        //
        // GET: /Admin/Bookings/

        public ViewResult Index()
        {
            var bvm = new BookingsViewModel(db.Bookings.Include("ExtraBookings").Include("Hotel").Include("Travel"));
            return View(bvm);
        }

        //
        // GET: /Admin/Bookings/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Booking booking = db.Bookings.Single(b => b.id == id);
            ViewBag.resort_id = new SelectList(db.Resorts, "id", "name", booking.Hotel.resort_id);
            ViewBag.hotel_id = new SelectList(db.Hotels, "id", "name", booking.hotel_id);
            ViewBag.hotels = db.Hotels.Where(h => h.resort_id == booking.Hotel.resort_id);
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
            ViewBag.hotel_id = new SelectList(db.Hotels, "id", "name", booking.hotel_id);
            return View(booking);
        }

        //
        // GET: /Admin/Bookings/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Booking booking = db.Bookings.Single(b => b.id == id);
            return View(booking);
        }

        public ActionResult Details(Guid id)
        {
            Booking booking = db.Bookings.Single(b => b.id == id);
            return View(booking);
        }

        //
        // POST: /Admin/Bookings/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
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

        public ViewResult NewExtraRow(int resort_id)
        {
            return View("ExtraEditorRow", new ExtraBookingViewModel(new ExtraBooking(), resort_id));
        }

        public ViewResult NewHotelRow(int resort_id)
        {
            return View("HotelsEditorRow", db.Hotels.Where(h => h.resort_id == resort_id));
        }
    }
}