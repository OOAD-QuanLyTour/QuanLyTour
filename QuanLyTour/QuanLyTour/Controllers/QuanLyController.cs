﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTour.Controllers
{
    public class QuanLyController : QuanLy
    {
        // GET: QuanLy
        public ActionResult Index()
        {
            if(Session[NHANVIEN] != null || Session[ADMIN] != null)
                return View();
            return Redirect("/");
        }
    }
}