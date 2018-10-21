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
                return Redirect("/");
            }

            var nhanviens = db.NhanViens.Where(n => n.Chucvu == true);

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
                return Redirect("/");
            }

            var nhanVien = db.NhanViens.SingleOrDefault(n => n.MaNV == nhanvien);

            return View(nhanvien);
        }

        public ActionResult ThemNhanVien ()
        {
            if(Session[ADMIN] == null)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(true)]
        public ActionResult ThemNhanVien(NhanVien nhanvien)
        {
            if(Session[ADMIN] == null)
            {
                return Redirect("/");
            }

            db.NhanViens.InsertOnSubmit(nhanvien);
            db.SubmitChanges();

            return Redirect("/");
        }

        public ActionResult XoaNhanVien(int nhanvien)
        {
            if (Session[ADMIN] == null)
            {
                return Redirect("/");
            }

            var nhanVien = db.NhanViens.SingleOrDefault(n => n.MaNV == nhanvien && n.Chucvu == false);

            if(nhanVien == null)
            {
                return RedirectToRoutePermanent(routeName: "QuanLyNhanVien");
            }

            return View(nhanVien);
        }

        [HttpDelete]
        public ActionResult XoaNhanVien(NhanVien nhanvien)
        {
            if(Session[ADMIN]==null)
            {
                return Redirect("/");
            }

            db.NhanViens.DeleteOnSubmit(nhanvien);
            db.SubmitChanges();

            return RedirectToRoutePermanent(routeName: "QuanLyNhanVien");
        }
    }
}