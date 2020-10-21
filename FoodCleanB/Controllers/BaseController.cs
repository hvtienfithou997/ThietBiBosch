using System.Web.Mvc;
using FoodCleanB.Database;
using FoodCleanB.Helpers;

namespace FoodCleanB.Controllers
{
    [GetSession]
    public class BaseController : Controller
    {
        protected readonly ThietBiNoiThatEntities db = new ThietBiNoiThatEntities();
        
    }
}