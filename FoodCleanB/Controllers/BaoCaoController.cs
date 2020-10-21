using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ThietBiBosch.Controllers
{
    public class BaoCaoController : BaseController
    {
        // GET: BaoCao
        public ActionResult ThongKeThietBi()
        {            
            return View();
        }
        public ActionResult ThongKeThietBi(string tb)
        {
            var allThietBi = db.ThietBis.ToList();
            StringBuilder sb = new StringBuilder();
            sb.Append("<DKAC>");

            sb.Append("<DK>");
           

            return View();
        }
    }
}