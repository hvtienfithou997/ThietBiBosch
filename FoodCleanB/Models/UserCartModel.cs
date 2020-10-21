using System;

namespace FoodCleanB.Models
{
    public class UserCartItemModel
    {
        public int ItemId { get; set; }

        public int SoLuong { get; set; } = 1;

        public string MaTaiKhoan { get; set; }
        public decimal GiaThanh { get; set; }
        public string TenHang { get; set; }
        public string AnhSanPham { get; set; }
        public Guid MaNhaCungCap { get; set; }
    }
}