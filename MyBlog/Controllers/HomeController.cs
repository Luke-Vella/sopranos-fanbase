﻿using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        public TelemetryClient TC = new TelemetryClient();
        
        public ActionResult Index()
        {
            TC.TrackEvent("Navigating to Index Section ");
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            TC.TrackEvent("Navigating to About Section");
            return View();
        }
        
        public ActionResult Contact()
        {
            TC.TrackEvent("Navigating To Contact Section");
            return View();
        }

        
        public ActionResult Characters()
        {
            TC.TrackEvent("Navigating To Characters Section");
            return View();
        }



    }
}