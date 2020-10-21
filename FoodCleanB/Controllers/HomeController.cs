using System.Linq;
using System.Web.Mvc;

namespace ThietBiBosch.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.DMTB = db.DanhMucThietBis.ToList();            
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }

    }
}