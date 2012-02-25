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
    public class ResortController : Controller
    {
        private ASPNETDBEntities db = new ASPNETDBEntities();

        //
        // GET: /Admin/Resort/

        public ViewResult Index()
        {
            return View(db.Resorts.ToList());
        }

        //
        // GET: /Admin/Resort/Details/5

        public ViewResult Details(int id)
        {
            Resort resort = db.Resorts.Single(r => r.id == id);
            return View(resort);
        }

        //
        // GET: /Admin/Resort/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Resort/Create

        [HttpPost]
        public ActionResult Create(Resort resort)
        {
            if (ModelState.IsValid)
            {
                db.Resorts.AddObject(resort);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(resort);
        }
        
        //
        // GET: /Admin/Resort/Edit/5
 
        public ActionResult Edit(int id)
        {
            Resort resort = db.Resorts.Single(r => r.id == id);
            return View(resort);
        }

        //
        // POST: /Admin/Resort/Edit/5

        [HttpPost]
        public ActionResult Edit(Resort resort)
        {
            if (ModelState.IsValid)
            {
                db.Resorts.Attach(resort);
                db.ObjectStateManager.ChangeObjectState(resort, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resort);
        }

        //
        // GET: /Admin/Resort/Delete/5
 
        public ActionResult Delete(int id)
        {
            Resort resort = db.Resorts.Single(r => r.id == id);
            return View(resort);
        }

        //
        // POST: /Admin/Resort/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Resort resort = db.Resorts.Single(r => r.id == id);
            db.Resorts.DeleteObject(resort);
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