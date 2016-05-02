using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace outdoor.rocks.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "OUTDOOR.ROCKS";            
            return View();
        }        

        public ActionResult Trail(string id)
        {
            ViewBag.TrailId = id;
            return View();
        }
    }
}
