﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssexBooking.Models;

namespace EssexBooking.Controllers
{
    public class HomeController : Controller
    {
        public ASPNETDBEntities entities = new ASPNETDBEntities();
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Hotels()
        {
            return View(entities.Hotels);
        }
    }
}
