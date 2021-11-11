using QLDPKS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDPKS.Controllers
{
    public class DatPhongController : Controller
    {
        private QLKSEntities db = new QLKSEntities();
        // GET: DatPhong
        public ActionResult Index()
        {
            return View();
        }


        // GET: DatPhong/ThemDatPhong/{maSP}
        public ActionResult ThemVaoDatPhong(int id)
        {
            if (Session["cart"] == null)
            {
                var p = db.Phong.Find(id);

                List<DatPhongKS> cart = new List<DatPhongKS>();
                cart.Add(new DatPhongKS { phong = p, soNgay = 1 });
                Session["cart"] = cart;
            }
            else
            {
                List<DatPhongKS> cart = (List<DatPhongKS>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].soNgay++;
                }
                else
                {
                    var p = db.Phong.Find(id);
                    cart.Add(new DatPhongKS { phong = p, soNgay = 1 });
                }
                Session["cart"] = cart;
            }

            return RedirectToAction("Index");
        }

        // GET: DatPhong/XoaKhoiDatPhong/{maSP}
        public ActionResult XoaKhoiDatPhong(int id)
        {
            List<DatPhongKS> cart = (List<DatPhongKS>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
        private int isExist(int id)
        {
            List<DatPhongKS> cart = (List<DatPhongKS>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].phong.ID.Equals(id))
                    return i;
            return -1;
        }
    }
}