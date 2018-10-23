using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyTour.Models;
using System.Data.Linq;

namespace QuanLyTour.Controllers
{
    public class QuanLyPhieuDangKyController : QuanLy
    {
        public QuanLyPhieuDangKyController() : base()
        {

        }

        // GET: QuanLyPhieuDangKy
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DanhSachPhieuDaDangKy()
        {
            if(Session[KHACHHANG] == null)
            {
                return Redirect("/DangNhap");
            }

            Khach khach = (Khach) Session[KHACHHANG];

            if(khach.PhieuDangKies.Count == 0)
            {
                return View();
            }

            return View(khach.PhieuDangKies.ToList());
        }

        public ActionResult DanhSachPhieuChuaDuyet()
        {
            if (Session[NHANVIEN] == null)
            {
                return Redirect("/SigninForStaff");
            }

            var phieudangkies = db.PhieuDangKies;

            if(phieudangkies == null)
            {
                return View();
            }

            var phieudangkieS = phieudangkies.Where(n => n.TinhTrang == null);

            return View(phieudangkieS);

        }

        public ActionResult DuyetPhieuDangKy(int phieu)
        {
            if(Session[NHANVIEN] == null)
            {
                return Redirect("/SigninForStaff");
            }

            var phieudangky = db.PhieuDangKies.SingleOrDefault(n => n.MaPhieuDK == phieu);

            var khachthamgias = phieudangky.KhachThamGias;

            if(khachthamgias != null)
            {
                ViewBag.KhachThamGias = khachthamgias.ToList();
            }

            return View(phieudangky);
        }

        [HttpPost]
        public ActionResult DuyetPhieuDangKy(FormCollection phieu)
        {
            if (Session[NHANVIEN] == null)
            {
                return Redirect("/SigninForStaff");
            }

            bool tinhtrang;

            if(!bool.TryParse(phieu["TinhTrang"],out tinhtrang))
            {
                return RedirectToAction("DuyetPhieuDangKy",routeValues: new { phieu = int.Parse(phieu["MaPhieuDK"]) });
            }

            var phieudangky = db.PhieuDangKies.SingleOrDefault(n => n.MaPhieuDK == int.Parse(phieu["MaPhieuDK"]));

            phieudangky.TinhTrang = tinhtrang;
            db.SubmitChanges();

            return RedirectToRoute(routeName: "DanhSachPhieuChuaDuyet");
        }

        public ActionResult DanhSachLichTrinh()
        {
            if (Session[NHANVIEN] == null)
            {
                return Redirect("/SigninForStaff");
            }

            var lichtrinhs = db.LichTrinh_Tours;

            if(lichtrinhs.Count() == 0)
            {
                return View();
            }

            return View(lichtrinhs.ToList());
        }

        public ActionResult DanhSachPhieuDangKyTheoLichTrinh(int lichtrinh)
        {
            if (Session[NHANVIEN] == null)
            {
                return Redirect("/SigninForStaff");
            }

            var phieudangkies = db.PhieuDangKies.Where(n => n.MaLichtrinh == lichtrinh);

            if (phieudangkies.Count() == 0)
            {
                return View();
            }

            return View(phieudangkies.ToList());
        }

        public ActionResult DanhSachPhieuDangKy()
        {
            if(Session[NHANVIEN] == null)
            {
                return Redirect("/SigninForStaff");
            }

            var phieudangkies = db.PhieuDangKies;

            if(phieudangkies.Count() == 0)
            {
                return View();
            }

            return View(phieudangkies.ToList());
        }

        public ActionResult XemPhieuDangKy(int phieu)
        {
            if(Session[NHANVIEN] == null)
            {
                return Redirect("/SigninForStaff");
            }

            var phieudangky = db.PhieuDangKies.SingleOrDefault(n => n.MaPhieuDK == phieu);

            if(phieudangky == null)
            {
                return View();
            }

            if(phieudangky.KhachThamGias != null)
            {
                ViewBag.KhachThamGias = phieudangky.KhachThamGias.ToList();
            }
            return View(phieudangky);
        }
    }
}
