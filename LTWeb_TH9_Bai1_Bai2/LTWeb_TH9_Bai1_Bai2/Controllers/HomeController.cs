using LTWeb_TH9_Bai1_Bai2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTWeb_TH9_Bai1_Bai2.Controllers
{
    public class HomeController : Controller
    {
        QL_SachEntities data = new QL_SachEntities();
        public ActionResult Index()
        {
            List<Sach> dsSach = data.Saches.OrderByDescending(s => s.NgayCapNhat).ToList();
            return View(dsSach);
        }

        public ActionResult DSMenu_ChuDe()
        {
            List<ChuDe> dsCD = data.ChuDes.ToList();
            return PartialView(dsCD);
        }

        public ActionResult DSMenu_NXB()
        {
            List<NhaXuatBan> dsNXB = data.NhaXuatBans.ToList();
            return PartialView(dsNXB);
        }

        public ActionResult XemChiTiet(int id)
        {
            Sach sach = data.Saches.SingleOrDefault(n => n.MaSach == id);
            //ViewBag.Masach = sach.MaSach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        public ActionResult HTSachTheoChuDe(int id)
        {
            ChuDe dept = data.ChuDes.SingleOrDefault(d => d.MaChuDe == id);
            if (dept == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Sach> dsnhanvien = data.Saches.Where(e => e.MaChuDe == id).ToList();
            return View(dsnhanvien);
        }
        public ActionResult HTSachTheoNXB(int id)
        {
            NhaXuatBan dept = data.NhaXuatBans.SingleOrDefault(d => d.MaNXB == id);
            if (dept == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Sach> dsnhanvien = data.Saches.Where(e => e.MaNXB == id).ToList();
            return View(dsnhanvien);
        }
        public ActionResult TimKiem()
        {
            // Đổ dropdown chủ đề
            ViewBag.MaChuDe = new SelectList(data.ChuDes.ToList(), "MaChuDe", "TenChuDe");
            return View();
        }

        // POST: Xử lý tìm kiếm
        [HttpPost]
        public ActionResult TimKiem(string keyword, int? MaChuDe, int[] Gia)
        {
            // Bắt đầu truy vấn cơ bản
            var saches = data.Saches.AsQueryable();

            // Lọc theo từ khóa
            if (!string.IsNullOrEmpty(keyword))
            {
                saches = saches.Where(s => s.TenSach.Contains(keyword));
            }

            // Lọc theo chủ đề
            if (MaChuDe.HasValue)
            {
                saches = saches.Where(s => s.MaChuDe == MaChuDe.Value);
            }

            // Chuyển sang danh sách để xử lý giá
            var result = saches.ToList();

            // Lọc theo giá (nếu có chọn checkbox)
            if (Gia != null && Gia.Length > 0)
            {
                var filtered = new List<Sach>();

                foreach (var range in Gia)
                {
                    switch (range)
                    {
                        case 1:
                            filtered.AddRange(result.Where(s => s.GiaBan >= 0 && s.GiaBan <= 10000));
                            break;
                        case 2:
                            filtered.AddRange(result.Where(s => s.GiaBan >= 12000 && s.GiaBan <= 20000));
                            break;
                        case 3:
                            filtered.AddRange(result.Where(s => s.GiaBan >= 21000 && s.GiaBan <= 40000));
                            break;
                        case 4:
                            filtered.AddRange(result.Where(s => s.GiaBan > 40000));
                            break;
                    }
                }

                result = filtered.Distinct().ToList();
            }

            // Truyền lại dropdown chủ đề
            ViewBag.MaChuDe = new SelectList(data.ChuDes.ToList(), "MaChuDe", "TenChuDe", MaChuDe);

            // Trả kết quả về cùng view
            return View(result);
        }
    }
}