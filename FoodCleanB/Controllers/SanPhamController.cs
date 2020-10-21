using ThietBiBosch.Database;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ThietBiBosch.Controllers
{
    public class SanPhamController : BaseController
    {
        public PartialViewResult HomeProduct(string nhomHang)
        {
            IQueryable<ThietBi> list;
            if (!string.IsNullOrEmpty(nhomHang))
            {
                list = db.ThietBis.Where(o => o.MaDMTB == nhomHang);
            }
            else
            {
                list = (IQueryable<ThietBi>)db.ThietBis.Take(12);
            }

            return PartialView(list.ToList());
        }

        //[Route("san-pham/{title}-{itemId}.html")]
        //[HttpGet]
        //public ActionResult Detail(int ItemId, string title)
        //{
        //    var sanPham = Db.SanPham.FirstOrDefault(o => o.MaHang == ItemId);

        //    return View(sanPham);
        //}

        [Route("nhom-hang/{title}-{id}-trang-{page}.html")]
        [HttpGet]
        public ActionResult List(int page = 1, string id = "", string title = null)
        {
            const int pageSize = 12;

            ViewBag.NhomHang = id;
            ViewBag.NhomHangTen = title;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;

            List<ThietBi> lstSanPham = new List<ThietBi>();
            if (string.IsNullOrEmpty(id))
            {
                var getNameCate = db.DanhMucThietBis.FirstOrDefault(x => x.MaDMTB == id);
                if (getNameCate != null)
                {
                    ViewBag.Category = getNameCate.TenDMTB;
                    ViewBag.Total = getNameCate.ThietBis.Count;
                    lstSanPham = getNameCate.ThietBis.ToList();
                }
            }
            else
            {
                ViewBag.Total = db.ThietBis.Count();
                lstSanPham = db.ThietBis.ToList();
            }

            // Phân trang, [pageSize] sản phẩm mỗi trang
            return View(lstSanPham.Skip(pageSize * (page - 1)).Take(pageSize).ToList());
        }

        public ActionResult Search(string search)
        {
            if (search != null)
            {
                //var result = db.ThietBis.Where(x => x.TenThietBi.IndexOf(search, System.StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                var result = db.ThietBis.Where(x => x.TenThietBi.ToLower().Contains(search.ToLower())).ToList();
                return View("List", result);
            }

            return RedirectToAction("List");
        }
    }
}