using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyTour.Models;

namespace QuanLyTour.Controllers
{
    public class QuanLy : Controller
    {
        // GET: QuanLy
        protected QuanLyTourDataContext db;
        public static string KHACHHANG = "KhachHang";
        public static string NHANVIEN = "NhanVien";
        public static string ADMIN = "Admin";

        public QuanLy()
        {
            db = new QuanLyTourDataContext();
        }

    }
}
