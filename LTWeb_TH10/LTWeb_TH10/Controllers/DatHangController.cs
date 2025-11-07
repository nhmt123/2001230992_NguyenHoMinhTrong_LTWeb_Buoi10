using LTWeb_TH10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTWeb_TH10.Controllers
{
    public class DatHangController : Controller
    {
        QL_BanHangEntities data = new QL_BanHangEntities();

        // GET: DatHang
        public ActionResult ThemMatHang(int msp)
        {
            GioHang gh = (GioHang)Session["gh"];
            if (gh == null)
            {
                gh = new GioHang();
            }
            int kq = gh.Them(msp);
            Session["gh"] = gh;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult XemGioHang()
        {
            GioHang gh = (GioHang)Session["gh"];
            return View(gh);
        }

        public ActionResult TaoDonDatHang()
        {
            // Kiểm tra đăng nhập
            if (Session["KhachHang"] == null)
            {
                return RedirectToAction("DangNhap", "Home");
            }

            // Kiểm tra giỏ hàng
            GioHang gh = Session["gh"] as GioHang;
            if (gh == null || gh.lst == null || gh.lst.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            // Lấy thông tin khách hàng
            KhachHang kh = (KhachHang)Session["KhachHang"];
            ViewBag.KhachHang = kh;
            ViewBag.GioHang = gh;

            return View();
        }

        // POST: Xử lý đặt hàng
        [HttpPost]
        public ActionResult TaoDonDatHang(string NgayGiao)
        {
            try
            {
                // Kiểm tra đăng nhập
                if (Session["KhachHang"] == null)
                {
                    return RedirectToAction("DangNhap", "Home");
                }

                // Lấy thông tin
                KhachHang kh = (KhachHang)Session["KhachHang"];
                GioHang gh = Session["gh"] as GioHang;

                if (gh == null || gh.lst == null || gh.lst.Count == 0)
                {
                    return RedirectToAction("Index", "Home");
                }

                // Tạo đơn đặt hàng
                DonDatHang ddh = new DonDatHang();
                ddh.MaKH = kh.MaKhachHang;
                ddh.NgayDat = DateTime.Now;
                ddh.NgayGiao = DateTime.Parse(NgayGiao);
                ddh.TongTien = (decimal)gh.TongThanhTien();
                ddh.TrangThai = "Chờ xử lý";

                data.DonDatHangs.Add(ddh);
                data.SaveChanges();

                // Lấy mã đơn hàng vừa tạo
                int maDonHang = ddh.MaDonHang;

                // Thêm chi tiết đơn hàng
                foreach (var item in gh.lst)
                {
                    ChiTietDonDatHang ct = new ChiTietDonDatHang();
                    ct.MaDonHang = maDonHang;
                    ct.MaSP = item.MaSanPham;
                    ct.SoLuong = item.SoLuong;
                    ct.DonGia = (decimal)item.DonGia;
                    ct.ThanhTien = (decimal)item.ThanhTien;

                    data.ChiTietDonDatHangs.Add(ct);
                }

                data.SaveChanges();

                // Xóa giỏ hàng
                gh.XoaGioHang();
                Session["gh"] = gh;

                // Chuyển sang trang thông báo
                return RedirectToAction("ThongBaoDatHang");
            }
            catch (Exception ex)
            {
                ViewBag.ThongBao = "Có lỗi xảy ra: " + ex.Message;
                return View();
            }
        }

        // Trang thông báo đặt hàng thành công
        public ActionResult ThongBaoDatHang()
        {
            return View();
        }

        public ActionResult XoaDSGioHang()
        {
            GioHang gh = (GioHang)Session["gh"];
            if (gh == null)
            {
                gh = new GioHang();
            }
            gh.XoaGioHang();
            Session["gh"] = gh;
            return RedirectToAction("Index", "Home");
        }
    }
}