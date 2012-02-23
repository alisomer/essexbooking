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
    public class HotelController : Controller
    {
        private ASPNETDBEntities db = new ASPNETDBEntities();

        //
        // GET: /Admin/Hotel/

        public ViewResult Index()
        {
            var hotels = db.Hotels.Include("HotelType").Include("Resort");
            return View(hotels.ToList());
        }

        //
        // GET: /Admin/Hotel/Details/5

        public ViewResult Details(int id)
        {
            Hotel hotel = db.Hotels.Single(h => h.id == id);
            return View(hotel);
        }

        //
        // GET: /Admin/Hotel/Create

        public ActionResult Create()
        {
            ViewBag.hotel_type_id = new SelectList(db.HotelTypes, "id", "name");
            ViewBag.resort_id = new SelectList(db.Resorts, "id", "name");
            return View();
        } 

        //
        // POST: /Admin/Hotel/Create

        [HttpPost]
        public ActionResult Create(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Hotels.AddObject(hotel);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.hotel_type_id = new SelectList(db.HotelTypes, "id", "name", hotel.hotel_type_id);
            ViewBag.resort_id = new SelectList(db.Resorts, "id", "name", hotel.resort_id);
            return View(hotel);
        }
        
        //
        // GET: /Admin/Hotel/Edit/5
 
        public ActionResult Edit(int id)
        {
            Hotel hotel = db.Hotels.Single(h => h.id == id);
            ViewBag.hotel_type_id = new SelectList(db.HotelTypes, "id", "name", hotel.hotel_type_id);
            ViewBag.resort_id = new SelectList(db.Resorts, "id", "name", hotel.resort_id);
            return View(hotel);
        }

        //
        // POST: /Admin/Hotel/Edit/5

        [HttpPost]
        public ActionResult Edit(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Hotels.Attach(hotel);
                db.ObjectStateManager.ChangeObjectState(hotel, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.hotel_type_id = new SelectList(db.HotelTypes, "id", "name", hotel.hotel_type_id);
            ViewBag.resort_id = new SelectList(db.Resorts, "id", "name", hotel.resort_id);
            return View(hotel);
        }

        //
        // GET: /Admin/Hotel/Delete/5
 
        public ActionResult Delete(int id)
        {
            Hotel hotel = db.Hotels.Single(h => h.id == id);
            return View(hotel);
        }

        //
        // POST: /Admin/Hotel/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Hotel hotel = db.Hotels.Single(h => h.id == id);
            db.Hotels.DeleteObject(hotel);
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