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

        public ActionResult DanhSachPhieuChuaDuyet()
        {
            if (Session[NHANVIEN] == null)
            {
                return Redirect("");
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
                return Redirect("");
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
        public ActionResult DuyetPhieuDangKy(PhieuDangKy phieu)
        {
            if (Session[NHANVIEN] == null)
            {
                return Redirect("");
            }

            if(phieu.TinhTrang == null)
            {
                return RedirectToAction("DuyetPhieuDangKy",routeValues: new { phieu = phieu.MaPhieuDK});
            }

            var phieudangky = db.PhieuDangKies.SingleOrDefault(n => n.MaPhieuDK == phieu.MaPhieuDK);

            phieudangky.TinhTrang = phieu.TinhTrang;
            db.SubmitChanges();

            return RedirectToRoute(routeName: "DanhSachPhieuChuaDuyet");
        }

        public ActionResult DanhSachPhieuDangKy()
        {
            if(Session[NHANVIEN] == null)
            {
                return RedirectToAction("");
            }

            var phieudangkies = db.PhieuDangKies;

            if(phieudangkies == null)
            {
                return View();
            }

            return View(phieudangkies.ToList());
        }

        public ActionResult XemPhieuDangKy(int phieu)
        {
            if(Session[NHANVIEN] == null)
            {
                return Redirect("");
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
