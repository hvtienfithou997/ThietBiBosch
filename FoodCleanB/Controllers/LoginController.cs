using ThietBiBosch.Database;
using ThietBiBosch.Helpers;
using ThietBiBosch.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThietBiBosch.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        [HttpGet]
        [Route("dang-nhap")]
        public ActionResult Login()
        {
            // Da login tu truoc
            if (Session["User"] != null)
            {
                RedirectToAction("Index", "Home");
            }

            Request.Cookies.Clear();
            return View();
        }

        [HttpPost]
        [Route("dang-nhap")]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel log)
        {
            if (!ModelState.IsValid)
            {
                return View(log);
            }

            // Mã hóa mật khẩu
            //var hashedPass = EncryptHelper.GenerateSHA256String(log.MatKhau);

            TaiKhoan user = db.TaiKhoans.FirstOrDefault(x => x.TenDangNhap == log.TenDangNhap && x.MatKhau == log.MatKhau);

            if (user != null)
            {
                HttpCookie myCookie = new HttpCookie("MyAccount");
                DateTime now = DateTime.Now;

                // ma hoa theo danh Base64 va luu trong cookie
                myCookie.Value = EncryptHelper.Base64Encode(user.MaTaiKhoan.ToString());
                Session["User"] = user;
                // Set the cookie expiration date.
                myCookie.Expires = now.AddDays(30);

                // Add the cookie.
                Response.Cookies.Add(myCookie);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        [Route("dang-ky")]
        public ActionResult DangKy()
        {
            // Da login tu truoc
            if (Session["User"] != null)
            {
                RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [Route("dang-ky")]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(RegisterModel m)
        {
            if (!ModelState.IsValid)
            {
                return View(m);
            }
           
            // Mật khẩu không trùng nhau
            if (m.MatKhau != m.XacNhanMatKhau)
            {
                ModelState.AddModelError("MatKhau", "Mật khẩu không trùng nhau");
                return View(m);
            }

            var user = db.TaiKhoans.FirstOrDefault(x => x.TenDangNhap == m.TenDangNhap);

            // Tên đăng nhập đã được dùng
            if (user != null)
            {
                ModelState.AddModelError("TenDangNhap", "Tên đăng nhập đã được sử dụng");
                return View(m);
            }


            // Mã hóa mật khẩu trước khi lưu
            var hashedPass = EncryptHelper.GenerateSHA256String(m.MatKhau);

            var newUser = new TaiKhoan { MaTaiKhoan = Guid.NewGuid().ToString(), MatKhau = hashedPass, TenDangNhap = m.TenDangNhap};
            db.TaiKhoans.Add(newUser);
            db.SaveChanges();

            return RedirectToAction("Login");
        }

        [Route("dangxuat")]
        public ActionResult LogOut()
        {
            Request.Cookies.Clear();
            return RedirectToAction("Login", "Login");
        }

        [HttpGet]
        [Route("khoi-phuc")]
        public ActionResult ResetPass()
        {
            // Da login tu truoc
            if (Session["User"] != null)
            {
                RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}