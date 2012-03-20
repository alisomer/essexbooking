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
    public class CustomerController : Controller
    {
        private ASPNETDBEntities db = new ASPNETDBEntities();

        //
        // GET: /Admin/Customer/

        public ViewResult Index()
        {
            var customers = db.Customers.Include("aspnet_Membership");
            return View(customers.ToList());
        }

        //
        // GET: /Admin/Customer/Details/5

        public ViewResult Details(Guid id)
        {
            Customer customer = db.Customers.Single(c => c.MembershipID == id);
            return View(customer);
        }

        //
        // GET: /Admin/Customer/Create

        public ActionResult Create()
        {
            ViewBag.MembershipID = new SelectList(db.aspnet_Membership, "UserId", "Password");
            return View();
        } 

        //
        // POST: /Admin/Customer/Create

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.MembershipID = Guid.NewGuid();
                db.Customers.AddObject(customer);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.MembershipID = new SelectList(db.aspnet_Membership, "UserId", "Password", customer.MembershipID);
            return View(customer);
        }
        
        //
        // GET: /Admin/Customer/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Customer customer = db.Customers.Single(c => c.MembershipID == id);
            ViewBag.MembershipID = new SelectList(db.aspnet_Membership, "UserId", "Password", customer.MembershipID);
            return View(customer);
        }

        //
        // POST: /Admin/Customer/Edit/5

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Attach(customer);
                db.ObjectStateManager.ChangeObjectState(customer, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MembershipID = new SelectList(db.aspnet_Membership, "UserId", "Password", customer.MembershipID);
            return View(customer);
        }

        //
        // GET: /Admin/Customer/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Customer customer = db.Customers.Single(c => c.MembershipID == id);
            return View(customer);
        }

        //
        // POST: /Admin/Customer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Customer customer = db.Customers.Single(c => c.MembershipID == id);
            db.Customers.DeleteObject(customer);
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