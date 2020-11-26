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

namespace Kutuphanem.Controllers
{
    [adminFilter]

    public class SatinAlmaSiparisController : Controller
    {
        private Kutuphane db = new Kutuphane();

        // GET: SatinAlmaSiparis
        public ActionResult Index()
        {
            return View(db.SatinAlmaSiparis.ToList());
        }
        [HttpPost]
        public JsonResult SiparisSatirOnayla(int id, int miktar)
        {

            using (Kutuphane db = new Kutuphane())
            {
                var siparis = db.Siparis.Where(s => s.ID == id).FirstOrDefault();

                if (siparis != null)
                {
                    Kitap ktp = new Kitap();
                    SatinAlmaSiparis satinAlmaSiparis = new SatinAlmaSiparis();
                    satinAlmaSiparis.KitapID = siparis.KitapID;
                    satinAlmaSiparis.KullaniciID = siparis.KullaniciID;
                    satinAlmaSiparis.Miktar = miktar;
                    satinAlmaSiparis.Tutar = 12;
                    satinAlmaSiparis.GelenMiktar = 0;
                    satinAlmaSiparis.Onay = false;
                    siparis.SiparisOnay = true;
                    //if (ktp.Miktar >= 0)
                    //{
                    //    siparis.SOnay = false;
                    //}
                    //else
                    //{
                    //    siparis.SOnay = true;
                    //}
                    satinAlmaSiparis.SepetOnay = false;
                    db.Entry(siparis).State = EntityState.Modified;
                    db.SatinAlmaSiparis.Add(satinAlmaSiparis);
                    db.SaveChanges();
                }

            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult KitapEviGelenSiparis()
        {
          using (Kutuphane db = new Kutuphane())
            {
                var siparis = db.Siparis.Where(s=>s.SOnay!=true).ToList();
                return View(siparis);
            }
        }

        // GET: SatinAlmaSiparis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SatinAlmaSiparis satinAlmaSiparis = db.SatinAlmaSiparis.Find(id);
            if (satinAlmaSiparis == null)
            {
                return HttpNotFound();
            }
            return View(satinAlmaSiparis);
        }

        // GET: SatinAlmaSiparis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SatinAlmaSiparis/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,KitapID,KullaniciID,Miktar,Tutar,KitapAdi,Yazar,Kategori")] SatinAlmaSiparis satinAlmaSiparis)
        {
            if (ModelState.IsValid)
            {
                db.SatinAlmaSiparis.Add(satinAlmaSiparis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(satinAlmaSiparis);
        }

        // GET: SatinAlmaSiparis/Edit/5
        public ActionResult KitapEviGelenSiparisler(int? id)
        {
            Kutuphane db = new Kutuphane();
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Siparis spt = db.Siparis.Find(id);
            //if (spt == null)
            //{
            //    return HttpNotFound();
            //}
            return View(spt);
        }

        // POST: SatinAlmaSiparis/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KitapEviGelenSiparisler([Bind(Include = "ID,KitapID,KullaniciID,Miktar,Tutar,KitapAdi,Yazar,Kategori,GelenMiktar")] SatinAlmaSiparis satinAlmaSiparis)
        {
            Kutuphane db = new Kutuphane();
           
                db.SatinAlmaSiparis.Add(satinAlmaSiparis);
                db.SaveChanges();
            
            return View(satinAlmaSiparis);
        }

        // GET: SatinAlmaSiparis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SatinAlmaSiparis satinAlmaSiparis = db.SatinAlmaSiparis.Find(id);
            if (satinAlmaSiparis == null)
            {
                return HttpNotFound();
            }
            return View(satinAlmaSiparis);
        }

        // POST: SatinAlmaSiparis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SatinAlmaSiparis satinAlmaSiparis = db.SatinAlmaSiparis.Find(id);
            db.SatinAlmaSiparis.Remove(satinAlmaSiparis);
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
    }
}
