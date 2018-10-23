using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTour.Controllers
{
    public class ThongKeTourYeuThichController : ThongKe
    {
        // GET: ThongKeTourYeuThich
        public ThongKeTourYeuThichController() : base() { }

        public ActionResult ThongKeTourYeuThichTheoThang(DateTime? time)
        {
            if (Session[NHANVIEN] == null)
            {
                return Redirect("/SigninforStaff");
            }

            DateTime Time;

            if (time == null)
            {
                Time = DateTime.Now;
            } else
            {
                Time = (DateTime)time;
            }

            var tours = db.Tours;

            if (tours.Count() == 0)
            {
                return View();
            }

            List<int> doanhthus = new List<int>();
            foreach (var tour in tours)
            {
                doanhthus.Add(tour.LichTrinh_Tours.Sum(m => m.PhieuDangKies.Count(k => k.TinhTrang == true && k.Ngaylap.Month == Time.Month 
                                                                        && k.Ngaylap.Year == Time.Year)));
            }

            ViewBag.YeuThich = doanhthus;
            return View(tours.ToList());
        }

        public ActionResult ThongKeTourYeuThichTheoQuy(int? quy,int? nam)
        {
            if (Session[NHANVIEN] == null)
            {
                return Redirect("/SigninforStaff");
            }
            if (quy == null)
            {
                quy = 1 >= DateTime.Now.Month && DateTime.Now.Month <= 3 ? 1 : (4 >= DateTime.Now.Month && DateTime.Now.Month <= 6 ? 2
                    : (7 >= DateTime.Now.Month && DateTime.Now.Month <= 9 ? 3 : 4));
            }

            if(nam == null)
            {
                nam = DateTime.Now.Year;
            }

            int thang = quy == 1 ? 1 : (quy == 2 ? 4 : (quy == 3 ? 7 : 10));

            var tours = db.Tours;

            if (tours.Count() == 0)
            {
                return View();
            }

            List<int> doanhthus = new List<int>();
            foreach (var tour in tours)
            {
                doanhthus.Add(tour.LichTrinh_Tours.Sum(m => m.PhieuDangKies.Count(k => k.TinhTrang == true && thang <= k.Ngaylap.Month && k.Ngaylap.Month <= (thang + 2)
                                                                        && k.Ngaylap.Year == nam)));
            }

            ViewBag.YeuThich = doanhthus;
            return View(tours.ToList());
        }

        public ActionResult ThongKeTourYeuThichTheoNam(int? nam)
        {
            if (Session[NHANVIEN] == null)
            {
                return Redirect("/SigninforStaff");
            }

            if (nam == null)
            {
                nam = DateTime.Now.Year;
            }

            var tours = db.Tours;

            if (tours.Count() == 0)
            {
                return View();
            }

            List<int> doanhthus = new List<int>();
            foreach (var tour in tours)
            {
                doanhthus.Add(tour.LichTrinh_Tours.Sum(m => m.PhieuDangKies.Count(k => k.Ngaylap.Year == nam && k.TinhTrang == true)));
            }

            ViewBag.YeuThich = doanhthus;
            return View(tours.ToList());
        }
    }
}