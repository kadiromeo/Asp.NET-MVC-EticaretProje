using Eticaret2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eticaret2.Controllers
{
    public class KartController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            return View(KartıGetir());
        }
        private void SiparisKayıt(Kart kart, AlisverisDetay model)
        {
            var siparis = new Siparis();
            siparis.SiparisNo = "A" + (new Random()).Next(1111, 9999).ToString();
            siparis.ToplamTutar = kart.Toplam();
            siparis.SiparisTarihi = DateTime.Now;
            siparis.Email = (string)Session["Email"];
            siparis.SiparisDurum = SiparisDurum.Bekleniyor;
            siparis.Adres = model.Adres;
            siparis.Sehir = model.Sehir;
            siparis.Ilce = model.Ilce;
            siparis.Mahalle = model.Mahalle;
            siparis.PostaKod = model.PostaKod;
            siparis.SiparisLine = new List<SiparisLine>();
            foreach (var item in kart.KartLines)
            {
                var siparisline = new SiparisLine();
                siparisline.Adet = item.Adet;
                siparisline.Fiyat = item.Adet * item.Urun.Fiyat;
                siparisline.UrunId = item.Urun.Id;
                siparis.SiparisLine.Add(siparisline);
            }
            db.Siparis.Add(siparis);
            db.SaveChanges();
        }
        public ActionResult Odeme()
        {
            return View(new AlisverisDetay());
        }

        [HttpPost]
        public ActionResult Odeme(AlisverisDetay model)
        {
            var kart = KartıGetir();
            if (kart.KartLines.Count == 0)
            {
                ModelState.AddModelError("UrunYok", "Sepetinizde ürün bulunmamaktadır");
            }
            if (ModelState.IsValid)
            {
                SiparisKayıt(kart, model);
                kart.Temizle();
                return View("SiparisTamamlandi");
            }
            else
            {
                return View(model);
            }
        }

        public PartialViewResult Sepet()
        {
            return PartialView(KartıGetir());
        }

        public PartialViewResult Sepet1()
        {
            return PartialView(KartıGetir());
        }

        public ActionResult KartaEkle(int id)
        {
            var urun = db.Uruns.FirstOrDefault(m => m.Id == id);
            if (urun != null)
            {
                KartıGetir().UrunEkle(urun, 1);
            }
            return RedirectToAction("Index");
        }

        public Kart KartıGetir()
        {
            var kart = (Kart)Session["Kart"];
            if (kart == null)
            {
                kart = new Kart();
                Session["Kart"] = kart;
            }
            return kart;
        }

        public ActionResult KarttanSil(int id)
        {
            var urun = db.Uruns.FirstOrDefault(m => m.Id == id);
            if (urun != null)
            {
                KartıGetir().UrunSil(urun);
            }
            return RedirectToAction("Index");
        }


    }
}