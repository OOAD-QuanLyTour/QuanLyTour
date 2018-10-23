using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using QuanLyTour.Models;

namespace QuanLyTour.Controllers
{
    public class QuanLyDangNhapDangXuatController : QuanLy
    {
        // GET: QuanLyDangNhapDangXuat

        public QuanLyDangNhapDangXuatController() : base ()
        {
            
        }

        public ActionResult DangNhapKhachHang()
        {
            if(Session[KHACHHANG] == null)
            {
                return Redirect("/DangNhap");
            }
            return Redirect("/");
        }

        [HttpPost]
        public ActionResult DangNhapKhachHang(FormCollection form)
        {
            string taikhoan = form["taikhoan"];
            string matkhau = form["matkhau"];
            bool error = false;
            if(string.IsNullOrEmpty(taikhoan) || string.IsNullOrWhiteSpace(taikhoan))
            {
                ViewData["taikhoan"] = "Chưa nhập tài khoản";
                error = true;
            }
            if (string.IsNullOrEmpty(matkhau) || string.IsNullOrWhiteSpace(matkhau))
            {
                ViewData["matkhau"] = "Chưa nhập mật khẩu";
                error = true;
            }

            if(error)
            {
                return View();
            }

            var khach = db.Khaches.SingleOrDefault(n => n.Username == taikhoan && n.Pwd == matkhau);

            if(khach == null)
            {
                ViewData["loi"] = "Tài khoản hoặc mật khẩu không đúng";
                return View();
            }

            Session[KHACHHANG] = khach;
            return Redirect("/");
        }


        public ActionResult DangNhapNhanVien()
        {
            if (Session[NHANVIEN] != null)
            {
                return Redirect("/QuanLy");
            }

            return View();
        }

        [HttpPost]
        public ActionResult DangNhapNhanVien(FormCollection form)
        {
            if(Session[NHANVIEN] != null)
            {
                return Redirect("/QuanLy");
            }

            string taikhoan = form["taikhoan"];
            string matkhau = form["matkhau"];
            bool error = false;
            if (string.IsNullOrEmpty(taikhoan) || string.IsNullOrWhiteSpace(taikhoan))
            {
                ViewData["taikhoan"] = "Chưa nhập tài khoản";
                error = true;
            }
            if (string.IsNullOrEmpty(matkhau) || string.IsNullOrWhiteSpace(matkhau))
            {
                ViewData["matkhau"] = "Chưa nhập mật khẩu";
                error = true;
            }

            if (error)
            {
                return View();
            }

            var khach = db.NhanViens.SingleOrDefault(n => n.Username == taikhoan && n.Pwd == matkhau && n.Chucvu == false);

            if (khach == null)
            {
                ViewData["loi"] = "Tài khoản hoặc mật khẩu không đúng";
                return View();
            }

            Session[NHANVIEN] = khach;
            return Redirect("/QuanLy");
        }

        public ActionResult DangNhapQuanTriVien()
        {
            if (Session[ADMIN] != null)
            {
                return Redirect("/QuanLy");
            }

            return View();
        }

        [HttpPost]
        public ActionResult DangNhapQuanTriVien(FormCollection form)
        {
            string taikhoan = form["taikhoan"];
            string matkhau = form["matkhau"];
            bool error = false;
            if (string.IsNullOrEmpty(taikhoan) || string.IsNullOrWhiteSpace(taikhoan))
            {
                ViewData["taikhoan"] = "Chưa nhập tài khoản";
                error = true;
            }
            if (string.IsNullOrEmpty(matkhau) || string.IsNullOrWhiteSpace(matkhau))
            {
                ViewData["matkhau"] = "Chưa nhập mật khẩu";
                error = true;
            }

            if (error)
            {
                return View();
            }

            var khach = db.NhanViens.SingleOrDefault(n => n.Username == taikhoan && n.Pwd == matkhau && n.Chucvu == true);

            if (khach == null)
            {
                ViewData["loi"] = "Tài khoản hoặc mật khẩu không đúng";
                return View();
            }

            Session[ADMIN] = khach;
            return Redirect("/QuanLy");
        }

        public ActionResult DangXuatKhachHang()
        {
            Session[KHACHHANG] = null;
            return Redirect("/");
        }

        public ActionResult DangXuatNhanVien()
        {
            Session[NHANVIEN] = null;
            return Redirect("/SigninForStaff");
        }

        public ActionResult DangXuatQuanTriVien()
        {
            Session[ADMIN] = null;
            return Redirect("/SigninForAdmin");
        }
    }
}