using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTour.Controllers
{
    public class ThongKeController : ThongKe
    {
        // GET: ThongKe

        public ThongKeController() : base() { }

        public ActionResult Index()
        {
            if(Session[NHANVIEN] == null)
            {
                return Redirect("/SigninForStaff");
            }
            return View();
        }
    }
}