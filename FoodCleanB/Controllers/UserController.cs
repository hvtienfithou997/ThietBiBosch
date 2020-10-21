using FoodCleanB.Database;
using FoodCleanB.Helpers;
using FoodCleanB.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace FoodCleanB.Controllers
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

        //[HttpPost]
        //[Route("dia-chi")]
        //[ValidateAntiForgeryToken]
   //     public ActionResult AddShipping(ThongTinKhachHang m)
   //     {
   //         if (!ModelState.IsValid)
   //         {
   //             return View(m);
   //         }

   //         TaiKhoan user = (TaiKhoan)Session["User"];

   //         if (!Db.ThongTinKhachHang.Any(o => o.MaTaiKhoan == user.MaTaiKhoan))
   //         {
   //             m.MacDinh = true;
   //         }

   //         m.MaSo = Guid.NewGuid();
   //         m.MaTaiKhoan = user.MaTaiKhoan;

   //         Db.ThongTinKhachHang.Add(m);
   //         Db.SaveChanges();

   //         TempData["Message"] = "Thêm địa chỉ thành công.";

   //         return RedirectToAction("Shipping");
   //     }

   //     [HttpGet]
   //     [Route("chon-dia-chi")]
   //     public ActionResult SelectShippingDefault(string id)
   //     {
   //         TaiKhoan user = (TaiKhoan)Session["User"];

   //         var mShip = Db.ThongTinKhachHang.Where(o => o.MaTaiKhoan == user.MaTaiKhoan);

   //         if (mShip.Any())
   //         {
   //             mShip.ForEach(c => c.MacDinh = false);
   //             mShip.First(o => o.MaSo == new Guid(id)).MacDinh = true;

   //             Db.SaveChanges();

   //             TempData["Message"] = "Đã đổi địa chỉ mặc định.";
   //         }

   //         return RedirectToAction("Shipping");
   //     }

   //     public ActionResult DeleteShipping(Guid id)
   //     {
   //         TaiKhoan user = (TaiKhoan)Session["User"];
   //         var mShip = Db.ThongTinKhachHang.FirstOrDefault(o => o.MaTaiKhoan == user.MaTaiKhoan && o.MaSo == id);

   //         if (mShip != null)
   //         {
   //             Db.ThongTinKhachHang.Remove(mShip);
   //             Db.SaveChanges();

   //             TempData["Message"] = "Đã xóa địa chỉ.";
   //         }

   //         return RedirectToAction("Shipping");
   //     }

   //     [HttpGet]
   //     [Route("don-hang")]
   //     public ActionResult Order()
   //     {
   //         TaiKhoan user = (TaiKhoan)Session["User"];
   //         var orders = Db.DonHang.Where(o => o.MaTaiKhoan == user.MaTaiKhoan);
   //         return View(orders);
   //     }

   //     [HttpGet]
   //     [Route("san-pham-ua-thich")]
   //     public ActionResult WishList()
   //     {
			//return View();
   //     }
    }
}