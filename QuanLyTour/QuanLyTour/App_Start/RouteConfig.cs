using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuanLyTour
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ThongKe",
                url: "ThongKe",
                defaults: new { controller = "ThongKe", action = "Index" }
            );

            routes.MapRoute(
                name: "QuanLy",
                url: "QuanLy",
                defaults: new { controller = "QuanLy", action = "Index" }
            );

            routes.MapRoute(
                name: "DanhSachSuaTour",
                url: "DanhSachTourCoTheSua",
                defaults: new { controller = "QuanLyTour", action = "DanhSachSuaTour" }
            );

            routes.MapRoute(
                name: "DanhSachXoaTour",
                url: "DanhSachTourCoTheXoa",
                defaults: new { controller = "QuanLyTour", action = "DanhSachXoaTour" }
            );

            routes.MapRoute(
                name: "TaoTour",
                url: "TaoTour",
                defaults: new { controller = "QuanLyTour", action = "TaoTour" }
            );

            routes.MapRoute(
                name: "XemTourDaDangKy",
                url: "XemTourDaDangKy",
                defaults: new { controller = "QuanLyTour", action = "XemTourDaDangKy" }
            );

            routes.MapRoute(
                name: "QuanLyTour",
                url: "DanhSachTour",
                defaults: new { controller = "QuanLyTour", action = "Index" }
            );

            routes.MapRoute(
                name: "DanhSachPhieuDaDangKy",
                url: "DanhSachPhieuDaDangKy",
                defaults: new { controller = "QuanLyPhieuDangKy", action = "DanhSachPhieuDaDangKy" }
            );


            routes.MapRoute(
                name: "DanhSachLichTrinh",
                url: "DanhSachLichTrinh",
                defaults: new { controller = "QuanLyPhieuDangKy", action = "DanhSachLichTrinh" }
            );

            routes.MapRoute(
                name: "DanhSachPhieuDangKy",
                url: "DanhSachPhieuDangKy",
                defaults: new { controller = "QuanLyPhieuDangKy", action = "DanhSachPhieuDangKy" }
            );


            routes.MapRoute(
                name: "DanhSachPhieuChuaDuyet",
                url: "DanhSachDuyetPhieu",
                defaults: new { controller = "QuanLyPhieuDangKy", action = "DanhSachPhieuChuaDuyet" }
            );

            routes.MapRoute(
                name: "SuaThongTinCaNhan",
                url: "SuaThongTinCaNhan",
                defaults: new { controller = "QuanLyKhachHang", action = "SuaThongTinCaNhan" }
            );

            routes.MapRoute(
                name: "XemThongTinCaNhan",
                url: "ThongTinCaNhan",
                defaults: new { controller = "QuanLyKhachHang", action = "XemThongTinCaNhan" }
            );

            routes.MapRoute(
                name: "XemDanhSachKhachHang",
                url: "DanhSachKhachHang",
                defaults: new { controller = "QuanLyKhachHang", action = "XemDanhSachKhachHang" }
            );

            routes.MapRoute(
                name: "QuanLyNhanVien",
                url: "DanhSachNhanVien",
                defaults: new { controller = "QuanLyNhanVien", action = "QuanLyNhanVien" }
            );

            routes.MapRoute(
                name: "TaoNhanVien",
                url: "ThemNhanVien",
                defaults: new { controller = "QuanLyNhanVien", action = "ThemNhanVien" }
            );

            routes.MapRoute(
                name: "DangXuatQuanTriVien",
                url: "LogoutForAdmin",
                defaults: new { controller = "QuanLyDangNhapDangXuat", action = "DangXuatQuanTriVien" }
            );

            routes.MapRoute(
                name: "DangNhapQuanTriVien",
                url: "SigninForAdmin",
                defaults: new { controller = "QuanLyDangNhapDangXuat", action = "DangNhapQuanTriVien" }
            );

            routes.MapRoute(
                name: "DangXuatNhanVien",
                url: "LogoutForStaff",
                defaults: new { controller = "QuanLyDangNhapDangXuat", action = "DangXuatNhanVien" }
            );

            routes.MapRoute(
                name: "DangNhapNhanVien",
                url: "SigninForStaff",
                defaults: new { controller = "QuanLyDangNhapDangXuat", action = "DangNhapNhanVien" }
            );



            routes.MapRoute(
                name: "DangKy",
                url: "DangKy",
                defaults: new { controller = "QuanLyKhachHang", action = "TaoKhachHang" }
            );

            routes.MapRoute(
                name: "DangXuat",
                url: "DangXuat",
                defaults: new { controller = "QuanLyDangNhapDangXuat", action = "DangXuatKhachHang" }
            );

            routes.MapRoute(
                name: "DangNhap",
                url: "DangNhap",
                defaults: new { controller = "QuanLyDangNhapDangXuat", action = "DangNhapKhachHang"}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "QuanLyTour", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
