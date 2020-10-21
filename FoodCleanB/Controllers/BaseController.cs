using System.Web.Mvc;
using ThietBiBosch.Database;
using ThietBiBosch.Helpers;

namespace ThietBiBosch.Controllers
{
    [GetSession]
    public class BaseController : Controller
    {
        protected readonly ThietBiNoiThatEntities db = new ThietBiNoiThatEntities();
        
    }
}