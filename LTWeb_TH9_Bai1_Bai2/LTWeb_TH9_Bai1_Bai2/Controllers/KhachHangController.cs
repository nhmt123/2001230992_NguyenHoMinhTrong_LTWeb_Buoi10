using LTWeb_TH9_Bai1_Bai2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LTWeb_TH9_Bai1_Bai2.Controllers
{
    public class KhachHangController : Controller
    {
        QL_SachEntities data = new QL_SachEntities();

        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(KhachHang kh)
        {
            string nhapLai = Request["NhapLaiMatKhau"];
            if (kh.MatKhau != nhapLai)
            {
                ViewBag.ThongBao = "Mật khẩu nhập lại không khớp!";
                return View(kh); 
            }
            if (data.KhachHangs.Any())
            {
                int maxId = data.KhachHangs.Max(t => t.MaKH);
                kh.MaKH = maxId + 1;
            }
            else
            {
                kh.MaKH = 1;
            }
            data.KhachHangs.Add(kh);
            data.SaveChanges();
            TempData["ThongBao"] = "Đăng ký thành công! Hãy đăng nhập để tiếp tục.";
            return RedirectToAction("DangNhap");
        }
        public ActionResult DangNhap()
        {
            ViewBag.ThongBao = TempData["ThongBao"];
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(string TaiKhoan, string MatKhau)
        {
            var kh = data.KhachHangs.FirstOrDefault(k => k.TaiKhoan == TaiKhoan && k.MatKhau == MatKhau);
            if (kh != null)
            {
                // Lưu vào session
                Session["TaiKhoan"] = kh;
                Session["HoTen"] = kh.HoTen;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng!";
                return View();
            }
        }

        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}