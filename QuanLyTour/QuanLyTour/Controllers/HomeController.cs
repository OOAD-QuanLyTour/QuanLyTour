using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyTour.Models;
using PagedList;

namespace QuanLyTour.Controllers
{
    public class HomeController : Controller
    {
        QuanLyTourDataContext db;

        public HomeController()
        {
            db = new QuanLyTourDataContext();
        }

        // GET: Home
        public ActionResult Index(int? page)
        {
            ViewData["text"] = "";
            int pageNumber = 1;
            if (page != null && page > 1)
            {
                pageNumber = (int)page;
            }

            int pageSize = 20;

            var tours = db.Tours;
            if (tours == null)
            {
                return View();
            }
            return View(tours.ToList().ToPagedList(pageNumber,pageSize));
        }

        [HttpPost]
        public ActionResult Index(FormCollection form, int? page)
        {
            ViewData["text"] = form["text"];
            int pageNumber = 1;
            if (page != null && page > 1)
            {
                pageNumber = (int)page;
            }

            int pageSize = 20;

            var tours = db.Tours;
            if (tours == null)
            {
                return View();
            }



            if (form["LoaiTimKiem"].Equals("TenTour"))
            {
                var tourS = tours.Where(n => n.TenTour.Contains(form["LoaiTimKiem"]));
                if(tourS == null)
                {
                    return View();
                }
                return View(tourS.ToList().ToPagedList(pageNumber, pageSize));
            } else if(form["LoaiTimKiem"].Equals("DiaDiem"))
            {
                var tourS = from t in tours join dt in db.DiaDiem_Tours on t.MaTour equals dt.Matour
                                            join dd in db.DiaDiems on dt.MaDD equals dd.MaDD
                            where dd.TenDD.Contains(form["text"])
                            select t;
                if (tourS == null)
                {
                    return View();
                }
                return View(tourS.ToList().ToPagedList(pageNumber, pageSize));
            }

            return View();
        }

        // GET: Home/Details/5
        public ActionResult Details(int tour)
        {
            var tours = db.Tours;

            if(tours == null)
            {
                return View();
            }

            var Tour = tours.SingleOrDefault(n => n.MaTour == tour);
            
            if(Tour == null)
            {
                return View();
            }

            ViewBag.LichTrinhs = Tour.LichTrinh_Tours;

            return View(Tour);
        }
    }
}
