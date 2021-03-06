﻿using QuanLyTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace QuanLyTour.Controllers
{
    public class QuanLyTourController : QuanLy
    {

        public QuanLyTourController() : base()
        {
        }

        // GET: QuanLyTour
        public ActionResult Index()
        {
            ViewData["text"] = "";

            var tours = db.Tours;
            if (tours == null)
            {
                return View();
            }
            return View(tours.ToList());
        }

        [HttpPost]
        public ActionResult Index(FormCollection tour)
        {
            ViewData["text"] = tour["Text"];

            var tours = db.Tours;
            if (tours == null)
            {
                return View();
            }



            if (tour["LoaiTimKiem"].Equals("TenTour"))
            {
                var tourS = tours.Where(n => n.TenTour.Contains(tour["Text"]));
                if (tourS == null)
                {
                    return View();
                }
                return View(tourS.ToList());
            }
            else if (tour["LoaiTimKiem"].Equals("DiaDiem"))
            {
                var tourS = from t in tours
                            join dt in db.DiaDiem_Tours on t.MaTour equals dt.Matour
                            join dd in db.DiaDiems on dt.MaDD equals dd.MaDD
                            where dd.TenDD.Contains(tour["Text"])
                            select t;
                if (tourS == null)
                {
                    return View();
                }
                return View(tourS.ToList());
            }

            return View();
        }

        // GET: QuanLyTour/Create
        public ActionResult Details(int tour)
        {
            var tours = db.Tours;

            if (tours == null)
            {
                return View();
            }

            var Tour = tours.SingleOrDefault(n => n.MaTour == tour);

            if (Tour == null)
            {
                return View();
            }

            ViewBag.LichTrinhs = Tour.LichTrinh_Tours;

            return View(Tour);
        }

        public ActionResult DangKyLichTrinhTour(int tour)
        {
            if (Session[KHACHHANG] == null)
            {
                return RedirectToRoute(routeName: "DangNhap");
            }
            var tours = db.Tours;

            if (tours == null)
            {
                return View();
            }

            var Tour = tours.SingleOrDefault(n => n.MaTour == tour);

            if (Tour == null)
            {
                return View();
            }

            ViewBag.Tour = Tour;
            var lichtrinhs = Tour.LichTrinh_Tours.Where(n => n.NgayDi > DateTime.Now);
            if(lichtrinhs != null)
            {
                ViewBag.LichTrinhs = lichtrinhs.ToList();
            }
            return View();
        }

        [HttpPost]
        public ActionResult DangKyLichTrinhTour(FormCollection tour)
        {
            if (Session[KHACHHANG] != null)
            {
                Khach khach = (Khach)Session["KhachHang"];
                PhieuDangKy phieu = new PhieuDangKy();
                phieu.MaKhach = khach.MaKhach;

                if (tour["LichTrinh"] == null)
                {
                    ViewData["LichTrinh"] = "Bạn chưa chọn lịch trình";
                }
                else
                {
                    phieu.MaLichtrinh = int.Parse(tour["LichTrinh"]);
                }

                phieu.Ngaylap = DateTime.Now;
                phieu.TinhTrang = null;

                if (bool.Parse(tour["LoaiThamGia"]))
                {

                    string[] tens = tour.GetValues("Ten");
                    string[] sdts = tour.GetValues("SDT");

                    for (int i = 0; i < tens.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(tens[i]) || !string.IsNullOrEmpty(sdts[i]))
                        {
                            KhachThamGia khachThamGia = new KhachThamGia();
                            khachThamGia.Ten = tens[i];
                            khachThamGia.SDT = sdts[i];
                            phieu.KhachThamGias.Add(khachThamGia);
                        }
                    }
                }

                db.PhieuDangKies.InsertOnSubmit(phieu);
                db.SubmitChanges();

                return Redirect("/");
            }
            return RedirectToRoute(routeName: "DangNhap");
        }

        public ActionResult XemTourDaDangKy()
        {
            if (Session[KHACHHANG] == null)
            {
                return RedirectToAction("");
            }

            Khach khach = (Khach) Session["KhachHang"];

            var phieu = db.PhieuDangKies.Where(n => n.Khach.MaKhach == khach.MaKhach);
            
            if(phieu == null)
            {
                return View();
            }

            return View(phieu.ToList());
        }

        public ActionResult TaoTour()
        {
            if(Session[NHANVIEN]==null)
            {
                return Redirect("/SigninForStaff");
            }
            return View();
        }

        [HttpPost]
        public ActionResult TaoTour(Tour tour)
        {
            if(Session[NHANVIEN] == null)
            {
                return Redirect("/SignInForStaff");
            }

            bool erroR = false;

            if(string.IsNullOrEmpty(tour.TenTour) || string.IsNullOrWhiteSpace(tour.TenTour))
            {
                ViewData["tentour"] = "Chưa nhập tên tour";
                erroR = true;
            }

            if(tour.GiaTour == 0)
            {
                ViewData["giatour"] = "Chưa nhập giá tour";
                erroR = true;
            }
            if (erroR)
            {
                return View(tour);
            }

            db.Tours.InsertOnSubmit(tour);
            db.SubmitChanges();

            return Redirect("/QuanLy");
        }

        public ActionResult DanhSachXoaTour()
        {
            if(Session[NHANVIEN] == null)
            {
                return Redirect("/SignInForStaff");
            }

            var tours = db.Tours.Where(n => n.LichTrinh_Tours.Count == 0 || n.LichTrinh_Tours.Where(m => (m.NgayDi <= DateTime.Now && DateTime.Now <= m.NgayKetthuc)||
                                        m.PhieuDangKies.Where(k => k.TinhTrang == true || k.TinhTrang == null) == null) == null);

            if(tours != null)
            {
                return View(tours.ToList());
            }

            return View();
        }

        public ActionResult XoaTour(int tour)
        {
            if(Session[NHANVIEN] == null)
            {
                return Redirect("/SigninForStaff");
            }

            var Tour = db.Tours.SingleOrDefault(n => n.MaTour == tour);

            if (Tour == null)
            {
                return Redirect("");
            }

            return View(Tour);
        }

        [HttpPost]
        public ActionResult XoaTour(FormCollection tour)
        {
            if(Session[NHANVIEN] == null)
            {
                return Redirect("/SigninForStaff");
            }

            Tour Tour = db.Tours.SingleOrDefault(n => n.MaTour == int.Parse(tour["MaTour"]));

            if(Tour.LichTrinh_Tours.Where(n => n.NgayDi >= DateTime.Now && n.NgayKetthuc <= DateTime.Now).Count() == 0)
            {
                db.Tours.DeleteOnSubmit(Tour);
                db.SubmitChanges();
            }

            return RedirectToRoute(routeName: "DanhSachXoaTour");

        }

        public ActionResult DanhSachSuaTour()
        {
            if(Session[NHANVIEN] == null)
            {
                return Redirect("/SigninForStaff");
            }

            var tours = db.Tours;

            if(tours == null)
            {
                return View();
            }

            return View(tours);
        }

        public ActionResult SuaTour(int tour)
        {
            if (Session[NHANVIEN] == null)
            {
                return Redirect("/SigninForStaff");
            }

            var Tour = db.Tours.SingleOrDefault(n => n.MaTour == tour);

            if(Tour == null)
            {
                return Redirect("");
            }

            return View(Tour);
        }

        [HttpPost]
        public ActionResult SuaTour(Tour toursua)
        {
            if(Session[NHANVIEN] == null)
            {
                return Redirect("/SigninForStaff");
            }

            bool erroR = false;

            if (toursua.GiaTour == 0)
            {
                ViewData["giatour"] = "Chưa nhập giá tour";
                erroR = true;
            }

            if (string.IsNullOrEmpty(toursua.TenTour) || string.IsNullOrWhiteSpace(toursua.TenTour))
            {
                ViewData["tentour"] = "Chưa nhập tên tour";
                erroR = true;
            }

            if(erroR)
            {
                return View(toursua);
            }

            var Tour = db.Tours.SingleOrDefault(n => n.MaTour == toursua.MaTour);
            Tour.GiaTour = toursua.GiaTour;
            Tour.TenTour = toursua.TenTour;
            Tour.MoTa = toursua.MoTa;
            db.SubmitChanges();

            return RedirectToRoute(routeName: "DanhSachSuaTour");
        }

    }
}
