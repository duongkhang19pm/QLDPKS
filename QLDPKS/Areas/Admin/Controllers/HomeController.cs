using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLDPKS.Models;
namespace QLDPKS.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        QLKSEntities db = new QLKSEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            // Lấy thông tin bài viết
            var baiViet = db.BaiViet.Where(r => r.KiemDuyet == 1 && r.ID == id).SingleOrDefault();

            // Tăng lượt xem
            // Chính sách: Mỗi máy chỉ tăng 1 lượt xem
            if (Object.Equals(Session["DaXem" + baiViet.ID], null))
            {
                BaiViet bv = db.BaiViet.Find(id);
                bv.LuotXem = bv.LuotXem + 1;
                db.Entry(bv).State = EntityState.Modified;
                db.SaveChanges();

                // Đánh dấu là đã xem
                Session["DaXem" + bv.ID] = 1;
            }

            // Hiển thị bài viết ra View
            return View(baiViet);
        }


    }
}