using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QLDPKS.Controllers
{
    public class XacThucController : Controller
    {
    
    
           protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["MaNhanVien"] == null)
                //filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { action = "", controller = "Home" }));
            base.OnActionExecuting(filterContext);
        }

        
    }
    
}