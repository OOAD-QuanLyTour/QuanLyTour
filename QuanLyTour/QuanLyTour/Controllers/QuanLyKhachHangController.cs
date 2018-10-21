using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyTour.Models;

namespace QuanLyTour.Controllers
{
    public class QuanLyKhachHangController : QuanLy
    {
        // GET: QuanLyKhachHang

        public QuanLyKhachHangController() : base()
        {

        }

        public ActionResult XemDanhSachKhachHang ()
        {
            if(Session[NHANVIEN] == null)
            {
                return Redirect("/");
            }

            var khachhangs = db.Khaches;

            if(khachhangs == null)
            {
                return View();
            }

            return View(khachhangs.ToList());
        }
        public ActionResult TaoKhachHang()
        {
            if(Session[KHACHHANG] != null)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(true)]
        public ActionResult TaoKhachHang(Khach khach)
        {
            if(Session[KHACHHANG] != null)
            {
                return Redirect("/");
            }

            var Khach = db.Khaches.Where(n => n.Username == khach.Username);

            if(Khach != null)
            {
                ViewData["taikhoan"] = "Tài khoản đã được dùng";
            }

            db.Khaches.InsertOnSubmit(khach);
            db.SubmitChanges();

            return RedirectToRoute(routeName: "DangNhap");
        }

        
        public ActionResult XemThongTinCaNhan()
        {
            if (Session[KHACHHANG] != null)
            {
                Khach khach = (Khach)Session[KHACHHANG];
                return View(khach);
            }
            return RedirectToRoute(routeName: "DangNhap");
        }

        
        public ActionResult XemThongTinKhachHang(int khach)
        {
            if(Session[NHANVIEN] == null)
            {
                return Redirect("/");
            }

            var Khach = db.Khaches.SingleOrDefault(n => n.MaKhach == khach);

            return View(Khach);
        }
    }
}