//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodCleanB.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class ThietBi
    {
        public string MaThietBi { get; set; }
        public string TenThietBi { get; set; }
        public Nullable<int> GiaThanh { get; set; }
        public Nullable<int> GiaKhuyenMai { get; set; }
        public string MoTa { get; set; }
        public string AnhSanPham { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public string MaNhomHang { get; set; }
        public Nullable<int> KhoiLuong { get; set; }
        public string MaNhaCungCap { get; set; }
        public string MaDMTB { get; set; }
        public string XuatXu { get; set; }
    
        public virtual DanhMucThietBi DanhMucThietBi { get; set; }
    }
}
