using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssexBooking.Models;

namespace EssexBooking.Areas.Admin.Controllers
{ 
    public class ExtrasController : Controller
    {
        private ASPNETDBEntities db = new ASPNETDBEntities();

        //
        // GET: /Admin/Extras/
        // GET: /Admin/Extras/?resort_id=5

        public ViewResult Index(int? resort_id)
        {
            var extras = db.Extras.Include("Resort");
            if (resort_id.HasValue)
            {
                extras = (ObjectQuery<Extra>) extras.Where(h => h.resort_id == resort_id);
            }
            return View(extras.ToList());
        }

        //
        // GET: /Admin/Extras/Details/5

        public ViewResult Details(int id)
        {
            Extra extra = db.Extras.Single(e => e.id == id);
            return View(extra);
        }

        //
        // GET: /Admin/Extras/Create
        // GET: /Admin/Extras/Create?resort_id=5

        public ActionResult Create(int? resort_id)
        {
            ViewBag.resort_id = new SelectList(db.Resorts, "id", "name");
            if (resort_id.HasValue)
            {
                ViewBag.Resort = resort_id.ToString();
            }
            return View();
        } 

        //
        // POST: /Admin/Extras/Create

        [HttpPost]
        public ActionResult Create(Extra extra)
        {
            if (ModelState.IsValid)
            {
                db.Extras.AddObject(extra);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.resort_id = new SelectList(db.Resorts, "id", "name", extra.resort_id);
            return View(extra);
        }
        
        //
        // GET: /Admin/Extras/Edit/5
 
        public ActionResult Edit(int id)
        {
            Extra extra = db.Extras.Single(e => e.id == id);
            ViewBag.resort_id = new SelectList(db.Resorts, "id", "name", extra.resort_id);
            return View(extra);
        }

        //
        // POST: /Admin/Extras/Edit/5

        [HttpPost]
        public ActionResult Edit(Extra extra)
        {
            if (ModelState.IsValid)
            {
                db.Extras.Attach(extra);
                db.ObjectStateManager.ChangeObjectState(extra, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.resort_id = new SelectList(db.Resorts, "id", "name", extra.resort_id);
            return View(extra);
        }

        //
        // GET: /Admin/Extras/Delete/5
 
        public ActionResult Delete(int id)
        {
            Extra extra = db.Extras.Single(e => e.id == id);
            return View(extra);
        }

        //
        // POST: /Admin/Extras/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Extra extra = db.Extras.Single(e => e.id == id);
            db.Extras.DeleteObject(extra);
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