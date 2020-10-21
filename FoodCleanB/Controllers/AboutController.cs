using System.Web.Mvc;

namespace FoodCleanB.Controllers
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