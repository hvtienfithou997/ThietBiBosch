using ThietBiBosch.Database;
using ThietBiBosch.Helpers;
using ThietBiBosch.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace ThietBiBosch.Controllers
{
    [LoginAttribute]
    [RoutePrefix("tai-khoan")]
    public class UserController : BaseController
    {
        [Route("thong-tin")]
        [HttpGet]
        public ActionResult UserProfile()
        {
			TaiKhoan user = (TaiKhoan)Session["User"];
			ViewBag.User = user;
			return View(user);
        }

        [HttpGet]
        [Route("doi-math-khau")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("doi-math-khau")]
        [ValidateAntiForgeryToken]
        public ActionResult SavePassword(ResetPassModel m)
        {
            if (!ModelState.IsValid)
            {
                return View("ChangePassword", m);
            }

            if (m.MatKhauMoi != m.XacNhanMatKhau || m.MatKhauMoi.Trim().Length == 0)
            {
                ModelState.AddModelError("XacNhanMatKhau", "Xác nhận mật khẩu mới không trùng nhau.");
                return View("ChangePassword", m);
            }

            TaiKhoan user = (TaiKhoan)Session["User"];

            var existed = db.TaiKhoans.FirstOrDefault(o => o.MaTaiKhoan == user.MaTaiKhoan);

            if (existed == null)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (EncryptHelper.GenerateSHA256String(m.MatKhau) != existed.MatKhau)
            {
                ModelState.AddModelError("MatKhau", "Sai mật khẩu hiện tại.");
                return View("ChangePassword", m);
            }

            existed.MatKhau = EncryptHelper.GenerateSHA256String(m.MatKhauMoi.Trim());

            db.SaveChanges();

            TempData["Message"] = "Đổi mật khẩu thành công.";

            return RedirectToAction("UserProfile");
        }

        //[HttpGet]
        //[Route("danh-sach-dia-chi")]
        //public ActionResult Shipping()
        //{
        //    TaiKhoan user = (TaiKhoan)Session["User"];

        //    var listDaiChi = Db.ThongTinKhachHang.Where(o => o.MaTaiKhoan == user.MaTaiKhoan);

        //    return View(listDaiChi);
        //}

        [HttpGet]
        [Route("dia-chi")]
        public ActionResult AddShipping()
        {
            return View();
        }
    }
}