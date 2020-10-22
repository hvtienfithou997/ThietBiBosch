using OfficeOpenXml;
using System;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using ThietBiBosch.Database;
using ThietBiBosch.Helpers;

namespace ThietBiBosch.Controllers
{
    [Login]
    [Admin]
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        #region Nhóm hàng hóa

        [HttpGet]
        public ActionResult NhomHang()
        {
            var nhoms = db.DanhMucThietBis.ToList();
            return View(nhoms);
        }

        [HttpGet]
        public ActionResult ThemNhomHang()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemNhomHang(DanhMucThietBi m)
        {
            try
            {
                if (m.TenDMTB == null || m.TenDMTB.Trim().Length == 0)
                {
                    ModelState.AddModelError("Ten", "Tên danh mục bị bỏ trống.");
                    return View(m);
                }

                if (db.DanhMucThietBis.Any(o => o.TenDMTB == m.TenDMTB))
                {
                    ModelState.AddModelError("Ten", "Danh mục thiết bị này đã tồn tại");
                    return View(m);
                }
                m.MaDMTB = Guid.NewGuid().ToString();
                db.DanhMucThietBis.Add(m);
                db.SaveChanges();
                TempData["Message"] = $"Thêm thành công {m.TenDMTB}";

                return RedirectToAction("NhomHang");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();
        }

        #endregion Nhóm hàng hóa

        //      #region Sản phẩm

        [HttpGet]
        public ActionResult SanPham()
        {
            var list = db.ThietBis.OrderByDescending(x => x.MaThietBi).ToList();
            var list1 = db.PhieuNhaps.Select(x => x.MaNhanVien);
            return View(list);
        }

        [HttpGet]
        public ActionResult ThemSanPham()
        {
            ViewBag.DMTB = db.DanhMucThietBis.Select(o => new SelectListItem { Value = o.MaDMTB, Text = o.TenDMTB }).ToList();
            //ViewBag.NhomHang = db.DanhMucThietBis.Select(o => new SelectListItem { Value = o.MaDMTB.ToString(), Text = o.TenDMTB }).ToList();
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemSanPham(ThietBi m)
        {
            if (m?.TenThietBi == null || m.TenThietBi.Trim().Length == 0)
            {
                ModelState.AddModelError("TenThietBi", "Tên thiết bị bị bỏ trống.");
            }

            //if (m == null || !ModelState.IsValid)
            //{
            //    ViewBag.NhaCungCap = Db.NhaCungCap.Select(o => new SelectListItem { Value = o.MaSo.ToString(), Text = o.Ten }).ToList();
            //    ViewBag.NhomHang = Db.NhomHang.Select(o => new SelectListItem { Value = o.MaSo.ToString(), Text = o.Ten }).ToList();

            //    return View(m);
            //}

            try
            {
                m.MaThietBi = Guid.NewGuid().ToString();
                m.TenThietBi = m.TenThietBi.Trim();

                db.ThietBis.Add(m);
                db.SaveChanges();
                TempData["Message"] = $"Thêm thành công {m.MaThietBi}";

                return RedirectToAction("SanPham");
            }
            catch (Exception ex)
            {
                TempData["Message"] = (ex.InnerException ?? ex).Message;

                return RedirectToAction("SanPham");
            }
        }

        [HttpGet]
        // GET: Admin/Edit/5
        public ActionResult EditSanPham(string maHang)
        {
            //ViewBag.NhaCungCap = Db.NhaCungCap.Select(o => new SelectListItem { Value = o.MaSo.ToString(), Text = o.Ten }).ToList();
            //ViewBag.NhomHang = Db.NhomHang.Select(o => new SelectListItem { Value = o.MaSo.ToString(), Text = o.Ten }).ToList();
            var item = db.ThietBis.FirstOrDefault(o => o.MaThietBi == maHang);

            if (item == null)
            {
                TempData["Message"] = $"Không có sản phẩm với mã số {maHang}";

                return RedirectToAction("SanPham");
            }

            return View(item);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditSanPham(ThietBi m)
        {
            if (m?.TenThietBi == null || m.TenThietBi.Trim().Length == 0)
            {
                ModelState.AddModelError("TenHang", "Tên sản phẩm bị bỏ trống.");
            }

            if (m == null || !ModelState.IsValid)
            {
                //ViewBag.NhaCungCap = Db.NhaCungCap.Select(o => new SelectListItem { Value = o.MaSo.ToString(), Text = o.Ten }).ToList();
                //ViewBag.NhomHang = Db.NhomHang.Select(o => new SelectListItem { Value = o.MaSo.ToString(), Text = o.Ten }).ToList();
                return View(m);
            }

            try
            {
                // Tìm sản phẩm
                ThietBi existed = db.ThietBis.FirstOrDefault(o => o.MaThietBi == m.MaThietBi);
                if (existed == null)
                {
                    TempData["Message"] = $"Không có thiết bị với mã số {m.MaThietBi}";

                    return RedirectToAction("SanPham");
                }

                existed.TenThietBi = m.TenThietBi?.Trim();
                existed.AnhSanPham = m.AnhSanPham;
                existed.MaNhaCungCap = m.MaNhaCungCap;
                existed.MaNhomHang = m.MaNhomHang;
                existed.GiaThanh = m.GiaThanh;
                existed.KhoiLuong = m.KhoiLuong;
                existed.MoTa = m.MoTa;
                existed.SoLuong = m.SoLuong;

                db.SaveChanges();
                TempData["Message"] = $"Cập nhật xong mã số {m.MaThietBi}";

                return RedirectToAction("SanPham");
            }
            catch (Exception ex)
            {
                TempData["Message"] = (ex.InnerException ?? ex).Message;

                return RedirectToAction("SanPham");
            }
        }

        // GET: Admin/Delete/5
        public ActionResult DeleteSanPham(string maHang)
        {
            // Tìm sản phẩm
            ThietBi existed = db.ThietBis.FirstOrDefault(o => o.MaThietBi == maHang);

            if (existed == null)
            {
                TempData["Message"] = $"Không có sản phẩm với mã số {maHang}";

                return RedirectToAction("SanPham");
            }

            db.ThietBis.Remove(existed);
            db.SaveChanges();

            TempData["Message"] = $"Đã xóa mã số {maHang}";

            return RedirectToAction("SanPham");
        }

        public ActionResult NhanVien(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                var nv = db.NhanViens.ToList();
                return View(nv);
            }
            else
            {
                var nv = db.NhanViens.Where(x => x.TenNhanVien.ToLower().Contains(search.ToLower()));
                return View(nv);
            }
        }

        //Thêm mới nhân viên
        [HttpGet]
        public ActionResult ThemNhanVien()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemNhanVien(NhanVien m)
        {
            if (m?.TenNhanVien == null || m.TenNhanVien.Trim().Length == 0)
            {
                ModelState.AddModelError("TenNhanVien", "Tên nhân viên bị bỏ trống.");
            }

            m.MaNhanVien = Guid.NewGuid().ToString();
            m.TenNhanVien = m.TenNhanVien.Trim();
            db.NhanViens.Add(m);
            db.SaveChanges();
            TempData["Message"] = $"Thêm thành công {m.TenNhanVien}";

            return RedirectToAction("NhanVien");
        }

        [HttpGet]
        public ActionResult EditNhanVien(string maNhanVien)
        {
            var nhanVien = db.NhanViens.FirstOrDefault(o => o.MaNhanVien == maNhanVien);

            if (nhanVien == null)
            {
                TempData["Message"] = $"Không có nhân viên với mã số {maNhanVien}";

                return RedirectToAction("SanPham");
            }
            else
            {
                return View(nhanVien);
            }
        }

        [HttpPost]
        public ActionResult EditNhanVien(NhanVien m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("NhanVien");
            }
            return View(m);
        }

        public ActionResult DeleteNhanVien(string maNhanVien)
        {
            var nhanVien = db.NhanViens.SingleOrDefault(x => x.MaNhanVien == maNhanVien);
            db.NhanViens.Remove(nhanVien);
            db.SaveChanges();
            return RedirectToAction("NhanVien");
        }

        public ActionResult PhieuNhapKho()
        {
            return View();
        }

        public ActionResult XuatExcel()
        {
            var thietBi = db.ThietBis.ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "THỐNG KÊ THIẾT BỊ";
            ws.Cells["B1"].Value = "THÔNG TIN";

            ws.Cells["A2"].Value = "Report";
            ws.Cells["B2"].Value = "Report1";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "MaThietBi";
            ws.Cells["B6"].Value = "TenThietBi";
            ws.Cells["C6"].Value = "GiaThanh";
            ws.Cells["D6"].Value = "SoLuong";
            ws.Cells["E6"].Value = "KhoiLuong";
            ws.Cells["F6"].Value = "ThanhTien";

            int rowStart = 7;
            ws.Cells[string.Format("A{0}", rowStart + thietBi.Count + 1)].Value = "TongSo";
            foreach (var item in thietBi)
            {
                ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.MaThietBi;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.TenThietBi;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.GiaThanh;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.SoLuong;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.KhoiLuong;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.GiaThanh * item.SoLuong;
                rowStart++;
            }

            ws.Cells[string.Format("C{0}", rowStart + 1)].Value = thietBi.Sum(x => x.GiaThanh);
            ws.Cells[string.Format("D{0}", rowStart + 1)].Value = thietBi.Sum(x => x.SoLuong);
            ws.Cells[string.Format("E{0}", rowStart + 1)].Value = thietBi.Sum(x => x.KhoiLuong);
            ws.Cells[string.Format("F{0}", rowStart + 1)].Value = thietBi.Sum(x => x.SoLuong * x.GiaThanh);
            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ThongKeThietBi.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

            return View();
        }

        //#endregion Sản phẩm

        //      [HttpGet]
        //      public ActionResult NhaCungCap()
        //      {
        //          var nhaCungCap = Db.NhaCungCap.ToList();
        //          return View(nhaCungCap);
        //      }

        //      [HttpGet]
        //      public ActionResult ThemNhaCungCap()
        //      {
        //          return View();
        //      }

        //      [HttpPost]
        //      public ActionResult ThemNhaCungCap(NhaCungCap m)
        //      {
        //          if (!ModelState.IsValid)
        //          {
        //              //
        //              return View(m);
        //          }

        //          m.MaSo = Guid.NewGuid();

        //          Db.NhaCungCap.Add(m);
        //          Db.SaveChanges();

        //          TempData["Message"] = $"Thêm thành công {m.Ten}";
        //          return RedirectToAction("NhaCungCap");
        //      }

        //      public ActionResult Coupon()
        //      {
        //          throw new NotImplementedException();
        //      }
    }
}