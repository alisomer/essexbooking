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
    public class HotelTypeController : Controller
    {
        private ASPNETDBEntities db = new ASPNETDBEntities();

        //
        // GET: /Admin/HotelType/

        public ViewResult Index()
        {
            return View(db.HotelTypes.ToList());
        }

        //
        // GET: /Admin/HotelType/Details/5

        public ViewResult Details(int id)
        {
            HotelType hoteltype = db.HotelTypes.Single(h => h.id == id);
            return View(hoteltype);
        }

        //
        // GET: /Admin/HotelType/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/HotelType/Create

        [HttpPost]
        public ActionResult Create(HotelType hoteltype)
        {
            if (ModelState.IsValid)
            {
                db.HotelTypes.AddObject(hoteltype);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(hoteltype);
        }
        
        //
        // GET: /Admin/HotelType/Edit/5
 
        public ActionResult Edit(int id)
        {
            HotelType hoteltype = db.HotelTypes.Single(h => h.id == id);
            return View(hoteltype);
        }

        //
        // POST: /Admin/HotelType/Edit/5

        [HttpPost]
        public ActionResult Edit(HotelType hoteltype)
        {
            if (ModelState.IsValid)
            {
                db.HotelTypes.Attach(hoteltype);
                db.ObjectStateManager.ChangeObjectState(hoteltype, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hoteltype);
        }

        //
        // GET: /Admin/HotelType/Delete/5
 
        public ActionResult Delete(int id)
        {
            HotelType hoteltype = db.HotelTypes.Single(h => h.id == id);
            return View(hoteltype);
        }

        //
        // POST: /Admin/HotelType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            HotelType hoteltype = db.HotelTypes.Single(h => h.id == id);
            db.HotelTypes.DeleteObject(hoteltype);
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