using System.Web.Mvc;

namespace ThietBiBosch.Controllers
{
    public class AboutController : BaseController
    {
        [Route("lien-he.html")]
        public ActionResult Contact()
        {
            return View();
        }
    }
}