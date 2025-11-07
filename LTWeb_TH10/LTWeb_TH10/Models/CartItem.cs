using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTWeb_TH10.Models
{
    public class CartItem
    {
        public int MaSanPham { get; set; }
        public string TenSP { get; set; }
        public string AnhBia { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien
        {
            get { return SoLuong * DonGia; }
        }

        QL_BanHangEntities data = new QL_BanHangEntities();

        public CartItem(int MaSach)
        {
            SanPham sp = data.SanPhams.Single(n => n.MaSanPham == MaSach);
            if (sp != null)
            {
                MaSanPham = MaSach;
                TenSP = sp.TenSP;
                AnhBia = sp.HinhAnh;
                DonGia = double.Parse(sp.DonGia.ToString());
                SoLuong = 1;
            }
        }
    }
}