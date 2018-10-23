using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTour.Controllers
{
    public class ThongKeDoanhThuController : ThongKe
    {
        // GET: ThongKeDoanhThu

        public ThongKeDoanhThuController () : base() {}

        public ActionResult ThongKeDoanhThuTheoThang(DateTime? time)
        {
            if(Session[NHANVIEN] == null)
            {
                return Redirect("/SigninforStaff");
            }

            if(time == null)
            {
                time = DateTime.Now;
            }

            var tours = db.Tours;

            if(tours.Count() == 0)
            {
                return View();
            }

            List<int> doanhthus = new List<int>();
            foreach(var tour in tours)
            {
                doanhthus.Add(tour.GiaTour * tour.LichTrinh_Tours.Sum(m => m.PhieuDangKies.Count(k => k.TinhTrang == true &&
                                k.Ngaylap.Month == ((DateTime)time).Month && k.Ngaylap.Year == ((DateTime)time).Year)));
            }

            ViewBag.DoanhThu = doanhthus;
            return View(tours.ToList());
        }

        public ActionResult ThongKeDoanhThuTheoQuy(FormCollection Form)
        {
            if (Session[NHANVIEN] == null)
            {
                return Redirect("/SigninforStaff");
            }
            int quy;
            int nam;
            FormCollection form = Form;
            if (form == null)
            {
                quy = 1 >= DateTime.Now.Month && DateTime.Now.Month <= 3 ? 1 : (4 >= DateTime.Now.Month && DateTime.Now.Month <= 6 ? 2
                    : (7 >= DateTime.Now.Month && DateTime.Now.Month <= 9 ? 3 : 4));
                nam = DateTime.Now.Year;
            }

            if(int.TryParse(form["quy"],out quy))
            {
                return ThongKeDoanhThuTheoQuy(null);
            }
            if(int.TryParse(form["nam"], out nam))
            {
                return ThongKeDoanhThuTheoQuy(null);
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
                doanhthus.Add(tour.GiaTour * tour.LichTrinh_Tours.Sum(m => m.PhieuDangKies.Count(k => thang <= k.Ngaylap.Month && k.Ngaylap.Month <= thang+2 
                                                                        && k.Ngaylap.Year == nam && k.TinhTrang == true)));
            }

            ViewBag.DoanhThu = doanhthus;
            return View(tours.ToList());
        }

        public ActionResult ThongKeDoanhThuTheoNam(int? nam)
        {
            if (Session[NHANVIEN] == null)
            {
                return Redirect("/SigninforStaff");
            }

            if(nam == null)
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
                doanhthus.Add(tour.GiaTour * tour.LichTrinh_Tours.Sum(m => m.PhieuDangKies.Count(k => k.Ngaylap.Year == nam && k.TinhTrang == true)));
            }

            ViewBag.DoanhThu = doanhthus;
            return View(tours.ToList());
        }
    }
}