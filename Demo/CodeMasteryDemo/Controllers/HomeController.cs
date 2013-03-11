using System;
using System.Web.Mvc;

namespace CodeMasteryDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Duration = 60, VaryByCustom = "Mobile")]
        public ActionResult OutputCacheMobileHiding()
        {
            return View();
        }

        public ActionResult ResponsiveGs()
        {
            return View();
        }

        public ActionResult ResponsiveMedia()
        {
            return View();
        }

        public ActionResult Regex()
        {
            return View();
        }

        public ActionResult ResponsiveForm()
        {
            return View();
        }
    }
}
