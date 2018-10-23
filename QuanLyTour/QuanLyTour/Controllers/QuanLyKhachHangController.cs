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
                return Redirect("/SigninForStaff");
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
            ViewData["hoten"] = "";
            ViewData["matkhau"] = "";
            ViewData["taikhoan"] = "";
            ViewData["SDT"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult TaoKhachHang(Khach khach)
        {
            if(Session[KHACHHANG] != null)
            {
                return Redirect("/");
            }
            ViewData["hoten"] = "";
            ViewData["matkhau"] = "";
            ViewData["taikhoan"] = "";
            ViewData["SDT"] = "";
            bool erroR = false;
            if(string.IsNullOrEmpty(khach.HoTen) || string.IsNullOrWhiteSpace(khach.HoTen))
            {
                ViewData["hoten"] = "Chưa nhập họ và tên";
                erroR = true;
            }

            if (string.IsNullOrEmpty(khach.Pwd) || string.IsNullOrWhiteSpace(khach.Pwd))
            {
                ViewData["matkhau"] = "Chưa nhập mật khẩu";
                erroR = true;
            }

            if (string.IsNullOrEmpty(khach.Username) || string.IsNullOrWhiteSpace(khach.Username))
            {
                ViewData["taikhoan"] = "Chưa nhập tài khoản";
                erroR = true;
            }

            if (string.IsNullOrEmpty(khach.SDT) || string.IsNullOrWhiteSpace(khach.SDT))
            {
                ViewData["SDT"] = "Chưa nhập số điện thoại";
                erroR = true;
            }

            if(erroR)
            {
                return View(khach);
            }

            Khach Khach = db.Khaches.SingleOrDefault(n => n.Username == khach.Username);

            if(Khach != null)
            {
                ViewData["taikhoan"] = "Tài khoản đã được dùng";
                return View(khach);
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

        public ActionResult SuaThongTinCaNhan()
        {
            if(Session[KHACHHANG] == null)
            {
                return RedirectToRoute(routeName: "DangNhap");
            }
            Khach khach = (Khach)Session[KHACHHANG];
            return View(khach);
        }

        [HttpPost]
        public ActionResult SuaThongTinCaNhan(Khach khach)
        {
            if(Session[KHACHHANG] == null)
            {
                return RedirectToRoute(routeName: "DangNhap");
            }

            Khach Khach = (Khach)Session[KHACHHANG];

            bool erroR = false;

            if(string.IsNullOrEmpty(khach.Pwd))
            {
                ViewData["matkhau"] = "Mật khẩu không được để trống";
                erroR = true;
            }

            if(string.IsNullOrEmpty(khach.HoTen) || string.IsNullOrWhiteSpace(khach.HoTen))
            {
                ViewData["hoten"] = "Họ tên không được để trống";
                erroR = true;
            }

            if (string.IsNullOrEmpty(khach.SDT) || string.IsNullOrWhiteSpace(khach.SDT))
            {
                ViewData["SDT"] = "Số điện thoại không được để trống";
                erroR = true;
            }

            if(erroR)
            {
                return View(khach);
            }

            Khach khacH = db.Khaches.SingleOrDefault(n => n.MaKhach == Khach.MaKhach);

            if(khacH == null)
            {
                ViewData["loi"] = "Khách hàng không có trong CSDL";
                return View(khach);
            }

            khacH.HoTen = khach.HoTen;
            khacH.Pwd = khach.Pwd;
            khacH.SDT = khach.SDT;
            khacH.Diachi = khach.Diachi;
            khacH.TenCoQuan = khach.TenCoQuan;

            db.SubmitChanges();

            return Redirect("/XemThongTinCaNhan");
        }

        public ActionResult XemThongTinKhachHang(int khach)
        {
            if(Session[NHANVIEN] == null)
            {
                return Redirect("/SigninForStaff");
            }

            var Khach = db.Khaches.SingleOrDefault(n => n.MaKhach == khach);

            return View(Khach);
        }
    }
}