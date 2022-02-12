using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eticaret2.Models;
namespace Eticaret2.Controllers
{
    public class HomeController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            return View(db.Uruns.ToList());
        }

        public ActionResult Detay(int id)
        {
            var result = (from sub in db.Yorums
                          where sub.UrunId == id
                          select sub.Puan).DefaultIfEmpty(0).Average();

            ViewBag.sub = Math.Round((float)result);

            var urunler = db.Uruns.Find(id);
            ViewBag.Urun = urunler;
            ViewBag.sayi = db.Yorums.ToList().Where(m => m.UrunId == id).Count();


            var yorum = new Yorum()
            {
                UrunId = urunler.Id

            };


            return View("Detay", yorum);
        }

        public ActionResult YorumGonder(Yorum yorum, int rating)
        {
            var uyeid = Session["Id"];
            if (yorum != null)
            {
                yorum.KullaniciId = (short)Convert.ToInt32(uyeid);
                yorum.Tarih = DateTime.Now;
                yorum.Puan = Convert.ToInt32(rating);
                db.Yorums.Add(yorum);
                db.SaveChanges();
            }
            return RedirectToAction("Detay", "Home", new { id = yorum.UrunId });
        }

        public PartialViewResult Kategori()
        {
            var kategorilistele = db.Kategoris.ToList();
            return PartialView(kategorilistele);
        }

        public ActionResult KategoriDetay(int id)
        {
            var kategoridetaylistele = db.Uruns.Where(m => m.Kategori.Id == id).ToList();
            return View(kategoridetaylistele);
        }

        public ActionResult Ara(string deger)
        {
            var arama = db.Uruns.Where(m => m.Ad.Contains(deger)).ToList();
            return View(arama);
        }
    }
}