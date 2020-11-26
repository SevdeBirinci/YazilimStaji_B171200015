using Kutuphanem.f;
using Kutuphanem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.WebPages;
namespace Kutuphanem.Controllers
{
  [Filtre]
    public class SepetController : Controller
    {// GET: Sepet
        public ActionResult Index()
        {
            using (Kutuphane db = new Kutuphane())
            { 
                var sepet = db.Siparis.Where(s=>s.SiparisOnay!=true).ToList(); 
                return View(sepet);
            }
        }
        [HttpPost]
        public ActionResult Ekle(FormCollection frm)
        {
            int adet = Convert.ToInt32(frm["adet"]);
            int id = Convert.ToInt32(frm["id"].ToString());
            using (Kutuphane db = new Kutuphane())
            {
                var kitap = db.Kitap.Where(s => s.ID == id).FirstOrDefault();
                double tutar = Convert.ToDouble(kitap.Fiyat) * Convert.ToDouble(adet);
                Siparis sip = new Siparis();
                sip.KitapID = id;
                sip.KullaniciID = 1;
                sip.Miktar = adet;
                sip.Tutar = tutar;
                sip.SiparisOnay = false;
                db.Siparis.Add(sip);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Kitaplar");
        }
        [HttpPost]
        public JsonResult SiparisSatirOnayla(int id,int miktar) {

            using (Kutuphane db = new Kutuphane())
            {
                

                    var siparis = db.Siparis.Where(s => s.ID == id).FirstOrDefault();
                    var _kitap = db.Kitap.Where(s => s.ID == id).FirstOrDefault();

                    if (siparis != null)
                    {
                        SatinAlmaSiparis satinAlmaSiparis = new SatinAlmaSiparis();
                        satinAlmaSiparis.KitapID = siparis.KitapID;
                        satinAlmaSiparis.KullaniciID = siparis.KullaniciID;
                        int miktarr = _kitap == null ? 0 : Convert.ToInt32(db.Siparis.Where(s => s.ID == id).FirstOrDefault().Miktar);
                        satinAlmaSiparis.Miktar = miktar;
                        satinAlmaSiparis.Tutar = 12;
                        satinAlmaSiparis.GelenMiktar = miktar;
                        satinAlmaSiparis.Onay = false;
                        siparis.SiparisOnay = false;

                        //if (miktarr <= miktar)
                        //{
                        //    siparis.SOnay = false;
                        //}
                       
                            siparis.SOnay = true;
                       

                        satinAlmaSiparis.SepetOnay = false;
                        db.Entry(siparis).State = EntityState.Modified;
                        db.SatinAlmaSiparis.Add(satinAlmaSiparis);
                        db.SaveChanges();
                  
                    }  
                }
            
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GelenSiparis()
        {

            using (Kutuphane db = new Kutuphane())
            {

                var siparis = db.Siparis.Where(s=>s.SOnay!=true).ToList();
                return View(siparis);

            }
        }
        public ActionResult SiparisAc(int? id)
        {
            Kutuphane db = new Kutuphane();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siparis siparis = db.Siparis.Find(id);
            if (siparis == null)
            {
                return HttpNotFound();
            }
            return View(siparis);
        }
         // POST: SatinAlmaSiparis/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SiparisAc([Bind(Include = "ID,KitapID,Miktar")] Siparis siparis)
        {
            Kutuphane db = new Kutuphane();

            if (ModelState.IsValid)
            {
                db.Entry(siparis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siparis);
        }
        [HttpPost]
        public JsonResult SiparisOnayla(string[] secili)
        {

            using (Kutuphane db = new Kutuphane())
            {
                foreach (var item in secili)
                {
                    int id = Convert.ToInt32(item);
                    int kitapid = Convert.ToInt32(db.Siparis.Where(s => s.ID == id).FirstOrDefault().KitapID);
                    var kitap = db.Kitap.Where(s => s.ID == kitapid).FirstOrDefault();
                    int miktar = kitap == null ? 0 : Convert.ToInt32(db.Siparis.Where(s => s.ID == id).FirstOrDefault().Miktar);
                    if (miktar > 0)
                    {
                        kitap.Miktar = kitap.Miktar - miktar;
                    }
                    else
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    db.Entry(kitap).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult MusteriSiparisOnayla(string[] seciliI)
        {
            using (Kutuphane db = new Kutuphane())
            {
                foreach (var item in seciliI)
                {
                    int id = Convert.ToInt32(item);
                    int kitapid = Convert.ToInt32(db.Siparis.Where(s => s.ID == id).FirstOrDefault().KitapID);
                    var kitap = db.Kitap.Where(s => s.ID == kitapid).FirstOrDefault();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Sil(int id)
        {
            using (Kutuphane db = new Kutuphane())
            {
                var siparis = db.Siparis.Where(s => s.ID == id).FirstOrDefault();
                db.Siparis.Remove(siparis);
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Onay(int[] id)
        {
            using (Kutuphane db = new Kutuphane())
            {
                foreach (int item in id)
                {
                    var siparis = db.Siparis.Where(s => s.ID == item).FirstOrDefault();
                    siparis.SiparisOnay = true;
                    int kitapid = Convert.ToInt32(db.Siparis.Where(s => s.ID == item).FirstOrDefault().KitapID);
                    var kitap = db.Kitap.Where(s => s.ID == kitapid).FirstOrDefault();
                    if (kitap != null)
                    {

                        int miktar = kitap == null ? 0 : Convert.ToInt32(db.Siparis.Where(s => s.ID == item).FirstOrDefault().Miktar);
                        siparis.SiparisOnay = true;
                        kitap.Miktar = kitap.Miktar - miktar;
                        if(kitap.Miktar>=0)
                        {
                            siparis.SOnay = true;
                        }
                        else
                        {
                            siparis.SOnay = false;
                        }
                        db.Entry(kitap).State = EntityState.Modified;
                    }

                    db.Entry(siparis).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult KitapEvi(int? id)
        {
            Kutuphane db = new Kutuphane();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siparis spt = db.Siparis.Find(id);
            if (spt == null)
            {
                return HttpNotFound();
            }
            return View(spt);
        }

        // POST: Kullanicilar/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KitapEvi([Bind(Include = "ID,Miktar,KitapID,SiparisAd,KullaniciID,Tutar,Toplam,SiparisOnay,KitapAdi,Yazar")] SatinAlmaSiparis sip)
        {

            if (ModelState.IsValid)
            {
                Kutuphane db = new Kutuphane();
                db.SatinAlmaSiparis.Add(sip);
                db.SaveChanges();
                return View();
            }
            return View(sip);
        }
        public ActionResult KitapEviGelenSiparis()
        {
            using (Kutuphane db = new Kutuphane())
            {
                var siparis = db.Siparis.Where(s=>s.SOnay!=true).ToList();
                return View(siparis);
            }
        }
        [HttpPost]
        public JsonResult SiparisGuncelle(int id, int gelenMiktar)
        {
            using (Kutuphane db = new Kutuphane())
            {
                var siparis = db.Siparis.Where(s => s.ID == id).FirstOrDefault();

                if (siparis != null)
                {
                    var _kitap = db.Kitap.Where(s => s.ID == siparis.KitapID).FirstOrDefault();
                    if (_kitap != null)
                    {
                        _kitap.Miktar = _kitap.Miktar + gelenMiktar;
                        db.Entry(_kitap).State = EntityState.Modified;
                    }
                    SatinAlmaSiparis satinAlmaSiparis = new SatinAlmaSiparis();
                    satinAlmaSiparis.KitapID = siparis.KitapID;
                    satinAlmaSiparis.KullaniciID = siparis.KullaniciID;
                    //satinAlmaSiparis.Miktar = miktar;
                    satinAlmaSiparis.Tutar = 12;
                    satinAlmaSiparis.GelenMiktar = gelenMiktar;
                    satinAlmaSiparis.Miktar = gelenMiktar;
                    satinAlmaSiparis.Onay = false;
                    siparis.SiparisOnay = true;
                    siparis.SOnay = true;
                    satinAlmaSiparis.SepetOnay = false;
                    if (_kitap.Miktar >= gelenMiktar)
                    {
                        siparis.SOnay = true;
                    }
                    else
                    {
                        siparis.SOnay = false;
                    }
                    db.Entry(siparis).State = EntityState.Modified;
                    db.SatinAlmaSiparis.Add(satinAlmaSiparis);
                    db.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}