using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QLDPKS.Areas.Admin.Controllers
{
    public class XacThucController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllername = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (Session["MaNhanVien"] == null)
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", controller = "Home" }));
            else
            {
                if (Session["Quyen"].ToString().ToLower() == "false")
                {
                    if (controllername != "DatPhongAdmin" && controllername != "BinhLuan" && controllername != "BaiViet" && controllername != "KhachHang" && controllername != "Phong" && controllername != "PhieuDatPhong" && controllername != "ChiTietPhieuDatPhong")
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "XacThuc", Controller = "Home" }));
                    }
                }

            }
            base.OnActionExecuting(filterContext);
        }
    }
}