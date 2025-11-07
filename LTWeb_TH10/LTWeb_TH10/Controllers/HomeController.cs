using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTWeb_TH10.Models;

namespace LTWeb_TH10.Controllers
{
    public class HomeController : Controller
    {
        QL_BanHangEntities data = new QL_BanHangEntities();
        public ActionResult Index()
        {
            List<SanPham> dsSP = data.SanPhams.ToList();
            return View(dsSP);
        }

        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string TaiKhoan, string MatKhau)
        {
            var kh = data.KhachHangs.FirstOrDefault(t => t.TaiKhoan == TaiKhoan && t.MaKhau == MatKhau);
            if (kh != null)
            {
                Session["KhachHang"] = kh;
                Session["TenKH"] = kh.TenKH;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}