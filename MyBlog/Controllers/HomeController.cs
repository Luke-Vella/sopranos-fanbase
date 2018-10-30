using Microsoft.ApplicationInsights;
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
            TC.TrackEvent("1st Step");
            return View();
        }

        public ActionResult About()
        {
            TC.TrackEvent("2nd Step");
            return View();
        }

        public ActionResult Contact()
        {
            TC.TrackEvent("3rd Step");

            return View();
        }

        public ActionResult ErrorPage()
        {

            return View();
        }

        public ActionResult PageWithInfiniteWaitingTime()
        {
            for (int i = 1; i>0; i++)
            {
                Console.WriteLine(i);
            }
            return View();
        }

        public ActionResult PageWith10sec()
        {
            System.Threading.Thread.Sleep(10000);
            return View();
        }

        public ActionResult StackOverFlowBugPage()
        {
            int[] arr = new int[10000];
            for (int i=0; i<9999; i++)
            {
                arr[i] = 83045821;
            }
            StackOverFlowBugPage();
            return View();
        }

    }
}