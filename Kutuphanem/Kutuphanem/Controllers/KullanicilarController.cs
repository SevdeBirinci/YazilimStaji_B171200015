using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kutuphanem.Models;
using System.ComponentModel.DataAnnotations;
using Kutuphanem.f;
using System.Threading.Tasks;

namespace Kutuphanem.Controllers
{
    public class KullanicilarController : Controller
    {
        private Kutuphane db = new Kutuphane();

        public ActionResult Index()
        {
            return View(db.Kullanici.ToList());
        }
        [ValidateInput(false)]
        public ActionResult GirisYap()

        {
            Kutuphane ktp = new Kutuphane();

            var model = db.Kullanici.ToList();

            return View();
        }


        public ActionResult KayitOl()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KayitOl([Bind(Include = "ID,KullaniciAdi,AdSoyad,Sifre,Email,Adres,Yas,Tip")] Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                db.Kullanici.Add(kullanici);
                db.SaveChanges();
                return RedirectToAction("GirisYap");
            }

            return View(kullanici);
        }
        [HttpPost]
        public ActionResult Kontrol(Kullanici kullanici)
        {//kullanıcı girilenle doğru mu?

            var model = db.Kullanici.Where(s => s.KullaniciAdi == kullanici.KullaniciAdi && s.Sifre == kullanici.Sifre && s.Email == kullanici.Email).FirstOrDefault();
            if (model != null)
            {

                Session["giris"] = model;
                Response.Write("Hoşgeldiniz " + kullanici.KullaniciAdi);
            }
            else
            {
                Session["giris"] = null;
                return RedirectToAction("KayitOl", "Kullanicilar");
            }
            return RedirectToAction("Index", "Kitaplar");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanici kullanici = db.Kullanici.Find(id);
            if (kullanici == null)
            {
                return HttpNotFound();
            }
            return View(kullanici);
        }



        public ActionResult Create()
        {
            return View();
        }


        public ActionResult YoneticiyeGelenMesaj()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,KullaniciAdi,AdSoyad,Sifre,Email,Adres,Yas,Tip")] Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                db.Kullanici.Add(kullanici);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kullanici);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanici kullanici = db.Kullanici.Find(id);
            if (kullanici == null)
            {
                return HttpNotFound();
            }
            return View(kullanici);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,KullaniciAdi,AdSoyad,Sifre,Email,Adres,Yas,Tip")] Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kullanici).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kullanici);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanici kullanici = db.Kullanici.Find(id);
            if (kullanici == null)
            {
                return HttpNotFound();
            }
            return View(kullanici);
        }

        // POST: Kullanicilar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kullanici kullanici = db.Kullanici.Find(id);
            db.Kullanici.Remove(kullanici);
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


        [adminFilter]
        public ActionResult YoneticiGiris()
        {
            return View();
        }

        public ActionResult Cikis()
        {
            Session.Abandon();
            Session.Clear();
            return View();
        }
        public ActionResult Search(string searching)
        {
            using (Kutuphane db = new Kutuphane())
            {
                return View("Index", db.Kitap.Where(c => c.KitapAdi.Contains(searching) || searching == null).ToList());
            }
        }
        public ActionResult Ayarlar(int? id)
        {
            Kutuphane db = new Kutuphane();
            var kullanici_ = db.Kullanici.Where(s => s.ID == id).FirstOrDefault();
            kullanici_.Tip = false;
            //if (id == )
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Kullanici kullanici = db.Kullanici.Find(id);
            //if (kullanici == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(kullanici);
            return View();
        }


    }


    } 
