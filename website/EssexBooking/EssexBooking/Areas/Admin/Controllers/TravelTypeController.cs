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
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "TelesaleStaff")]
    public class TravelTypeController : Controller
    {
        private ASPNETDBEntities db = new ASPNETDBEntities();

        //
        // GET: /Admin/TravelType/

        public ViewResult Index()
        {
            return View(db.TravelTypes.ToList());
        }

        //
        // GET: /Admin/TravelType/Details/5

        public ViewResult Details(int id)
        {
            TravelType traveltype = db.TravelTypes.Single(t => t.id == id);
            return View(traveltype);
        }

        //
        // GET: /Admin/TravelType/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/TravelType/Create

        [HttpPost]
        public ActionResult Create(TravelType traveltype)
        {
            if (ModelState.IsValid)
            {
                db.TravelTypes.AddObject(traveltype);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(traveltype);
        }
        
        //
        // GET: /Admin/TravelType/Edit/5
 
        public ActionResult Edit(int id)
        {
            TravelType traveltype = db.TravelTypes.Single(t => t.id == id);
            return View(traveltype);
        }

        //
        // POST: /Admin/TravelType/Edit/5

        [HttpPost]
        public ActionResult Edit(TravelType traveltype)
        {
            if (ModelState.IsValid)
            {
                db.TravelTypes.Attach(traveltype);
                db.ObjectStateManager.ChangeObjectState(traveltype, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(traveltype);
        }

        //
        // GET: /Admin/TravelType/Delete/5
 
        public ActionResult Delete(int id)
        {
            TravelType traveltype = db.TravelTypes.Single(t => t.id == id);
            return View(traveltype);
        }

        //
        // POST: /Admin/TravelType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            TravelType traveltype = db.TravelTypes.Single(t => t.id == id);
            db.TravelTypes.DeleteObject(traveltype);
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