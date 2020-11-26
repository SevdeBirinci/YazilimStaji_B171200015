using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kutuphanem.f;
using Kutuphanem.Models;
using System.Web.Script.Services;
using System.Web.Services;


namespace Kutuphanem.Controllers
{
  
    public class KitaplarController : Controller
    {
        private Kutuphane db = new Kutuphane();
        [Filtre]
        public ActionResult Index(int[] id)
        {
            Kitap kitap = db.Kitap.Find(id);
            Siparis sip = new Siparis();
            Kitap ktp = new Kitap();
            
       if(ktp.Miktar>0)
            {
                sip.SOnay = false;
            }

            return View(db.Kitap.ToList());
        }
        // GET: Kitaplar/Details/5
       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitap kitap = db.Kitap.Find(id);
            if (kitap == null)
            {
                return HttpNotFound();
            }
            return View(kitap);
        }

        // GET: Kitaplar/Create
    
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kitaplar/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,KitapAdi,Yazar,Kategori,Miktar,Fiyat,Resim")] Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                db.Kitap.Add(kitap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kitap);
        }

        // GET: Kitaplar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitap kitap = db.Kitap.Find(id);
            if (kitap == null)
            {
                return HttpNotFound();
            }
           
            return View(kitap);
        }

        // POST: Kitaplar/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,KitapAdi,Yazar,Kategori,Miktar,Fiyat,Resim")] Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {

                    string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");

                    string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string TamYolYeri = "~/Images/KullaniciResimleri/" + DosyaAdi + uzanti;

                    Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));
                    kitap.Resim = DosyaAdi + uzanti;
                } 
                db.Entry(kitap).State = EntityState.Modified;
                db.SaveChanges();
                
            }
            return RedirectToAction("Index");
        }

        // GET: Kitaplar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitap kitap = db.Kitap.Find(id);
            if (kitap == null)
            {
                return HttpNotFound();
            }
            return View(kitap);
        }
        public ActionResult Duzenle()
        {
            return View(db.Kitap.ToList());
        }
        // POST: Kitaplar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kitap kitap = db.Kitap.Find(id);
            db.Kitap.Remove(kitap);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpPost]
        public ActionResult Yeni(Kitap kitapBilgi)
        {
            //eğer dosya gelmişse işlemleri yap
            if (Request.Files.Count > 0)
            {

                string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");

                string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                string TamYolYeri = "~/Images/KullaniciResimleri/" + DosyaAdi + uzanti;

                Request.Files[0].SaveAs(Server.MapPath(TamYolYeri));
                kitapBilgi.Resim = DosyaAdi + uzanti;
            }
            db.Kitap.Add(kitapBilgi);
            db.SaveChanges();
            return RedirectToAction("Index", "Kitaplar");
        }
        public ActionResult ChartDataQuery()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ChartData()
        {
            List<Kitap> list = new List<Kitap>();
            using (Kutuphane context = new Kutuphane())
            {
                list = context.Kitap.ToList();

            }
            return Json(list, JsonRequestBehavior.AllowGet);
            //return View(db.Kitap.ToList());
        }
        public ActionResult Grafik()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Grafikk()
        {
            List<Kitap> list = new List<Kitap>();
            using (Kutuphane context = new Kutuphane())
            {
                list = context.Kitap.ToList();

            }
            return Json(list, JsonRequestBehavior.AllowGet);
            //return View(db.Kitap.ToList());
        }
        public ActionResult Grafik2()
        {
            return View();
        }
        // GET: Chart  
        [HttpGet]

        public ActionResult Grafik22()
        {
            List<Kitap> list = new List<Kitap>();
            using (Kutuphane context = new Kutuphane())
            {
                list = context.Kitap.ToList();

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Search(string searching)
        {
            using (Kutuphane db = new Kutuphane())
            {
                return View("Index",db.Kitap.Where(c => c.KitapAdi.Contains(searching)|c.Yazar.Contains(searching) | c.Kategori.Contains(searching) || searching == null).ToList());
            }

        }
    }

}
