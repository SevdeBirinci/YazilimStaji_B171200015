using Kutuphanem.f;
using Kutuphanem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kutuphanem.Controllers
{
   
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
     
       
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //[HttpPost]
        //public ActionResult Kontrol(Kullanici veri)
        //{
        //    Kutuphane db = new Kutuphane();
        //    var model = db.Kullanici.Where(s => s.KullaniciAdi == veri.KullaniciAdi && s.Sifre == veri.Sifre).FirstOrDefault();
        //    if (model != null)
        //    {
        //        Session["giris"] = 1;
        //    }
        //    else
        //    {
        //        Session["giris"] = 0;
        //    }

        //    return RedirectToAction("Index", "Admin");
        //}
    }
}