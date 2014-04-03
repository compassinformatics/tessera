using System.Web.Mvc;

namespace SampleWebApplication.Controllers
{
    /// <summary>
    /// The Home controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}