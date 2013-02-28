using EventTracker.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventTracker.Web.Controllers
{
    public class ChartsController : Controller
    {
        //
        // GET: /Charts/

        public ActionResult Index()
        {
            return RedirectToAction("LoggedOn");
        }

        public ActionResult LoggedOn()
        {
            // http://jsfiddle.net/gh/get/jquery/1.7.2/highslide-software/highcharts.com/tree/master/samples/highcharts/demo/spline-irregular-time/
            var results = ChartEvent.GetLoggedInChart();
            return View(results);
        }
    }
}
