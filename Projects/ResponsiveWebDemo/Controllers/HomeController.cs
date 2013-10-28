using System.Web.Mvc;

namespace ResponsiveWebDesignDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult BootstrapComponents()
        {
            return View();
        }

        public ActionResult BootstrapJavascript()
        {
            return View();
        }

        public ActionResult BootstrapNavbar()
        {
            return View();
        }

        public ActionResult BootstrapNavbarFixedTop()
        {
            return View();
        }

        public ActionResult BootstrapNavbarStaticTop()
        {
            return View();
        }

        public ActionResult BootstrapTheme()
        {
            return View();
        }

        public ActionResult HotKeys()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Duration = 60, VaryByCustom = "Mobile")]
        public ActionResult OutputCacheMobileHiding()
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

        public ActionResult ResponsiveGs()
        {
            return View();
        }

        public ActionResult ResponsiveMedia()
        {
            return View();
        }

        public ActionResult Typography()
        {
            return View();
        }
    }
}