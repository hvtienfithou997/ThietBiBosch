using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThietBiBosch.Models
{
    public class NhanVienModel
    {
        public string MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public bool? GioiTinh { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public string ChucVu { get; set; }
    }

    public class Utils
    {
        public static string addNode(string node, string str)
        {
            return String.Format("<{0}>{1}</{0}>", node, str);
        }
    }
}