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
    }
}
