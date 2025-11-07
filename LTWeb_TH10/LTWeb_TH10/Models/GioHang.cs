using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTWeb_TH10.Models
{
    public class GioHang
    {
        public List<CartItem> lst;

        public GioHang()
        {
            lst = new List<CartItem>();
        }

        public GioHang(List<CartItem> Lst)
        {
            lst = Lst;
        }

        public int SoMatHang()
        {
            return lst.Count;
        }

        public int TongSLHang()
        {
            return lst.Sum(t => t.SoLuong);
        }

        public double TongThanhTien()
        {
            return lst.Sum(t => t.ThanhTien);
        }

        public int Them(int MaSach)
        {
            CartItem sp = lst.Find(n => n.MaSanPham ==  MaSach);

            if (sp == null)
            {
                CartItem sach = new CartItem(MaSach);
                if (sach == null)
                    return -1;
                lst.Add(sach);
            }
            else
            {
                sp.SoLuong++;
            }
            return 1;
        }
        public void XoaGioHang()
        {
            if (lst != null)
            {
                lst.Clear();
            }
        }
    }
}