using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyTour.Models;

namespace QuanLyTour.Controllers
{
    public class QuanLyNhanVienController : QuanLy
    {
        // GET: QuanLyNhanVien

        public QuanLyNhanVienController() : base ()
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QuanLyNhanVien()
        {
            if(Session[ADMIN] == null)
            {
                return Redirect("/SigninForAdmin");
            }

            var nhanviens = db.NhanViens.Where(n => n.Chucvu == false);

            if (nhanviens == null)
            {
                return View();
            }

            return View(nhanviens.ToList());
        }

        public ActionResult XemNhanVien (int nhanvien)
        {
            if(Session[ADMIN] == null)
            {
                return Redirect("/SigninForAdmin");
            }

            var nhanVien = db.NhanViens.SingleOrDefault(n => n.MaNV == nhanvien);

            return View(nhanVien);
        }

        public ActionResult ThemNhanVien ()
        {
            if(Session[ADMIN] == null)
            {
                return Redirect("/SigninForAdmin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ThemNhanVien(NhanVien nhanvien)
        {
            if(Session[ADMIN] == null)
            {
                return Redirect("/SigninForAdmin");
            }
            nhanvien.Chucvu = false;
            db.NhanViens.InsertOnSubmit(nhanvien);
            db.SubmitChanges();

            return RedirectToRoutePermanent(routeName: "QuanLyNhanVien");
        }

        public ActionResult XoaNhanVien(int nhanvien)
        {
            if (Session[ADMIN] == null)
            {
                return Redirect("/SigninForAdmin");
            }

            var nhanVien = db.NhanViens.SingleOrDefault(n => n.MaNV == nhanvien && n.Chucvu == false);

            if(nhanVien == null)
            {
                return RedirectToRoutePermanent(routeName: "QuanLyNhanVien");
            }

            return View(nhanVien);
        }

        [HttpPost]
        public ActionResult XoaNhanVien(FormCollection nhanvien)
        {
            if(Session[ADMIN]==null)
            {
                return Redirect("/SigninForAdmin");
            }

            NhanVien nhanVien = db.NhanViens.SingleOrDefault(n => n.MaNV == int.Parse(nhanvien["MaNV"]) && n.Chucvu == false);

            if(nhanVien == null)
            {
                return RedirectToRoutePermanent(routeName: "QuanLyNhanVien");
            }

            db.NhanViens.DeleteOnSubmit(nhanVien);
            db.SubmitChanges();

            return RedirectToRoutePermanent(routeName: "QuanLyNhanVien");
        }
    }
}