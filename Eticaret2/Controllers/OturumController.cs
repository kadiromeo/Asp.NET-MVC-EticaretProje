using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Eticaret2.Models;
namespace Eticaret2.Controllers
{
    public class OturumController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Kayit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Kayit(Kullanici model)
        {
            var sifre = model.Sifre;
            var sifretekrar = model.SifreTekrar;
            var email = (from i in db.Kullanicis where i.Email.Equals(model.Email) select i).FirstOrDefault();
            if (sifre != sifretekrar)
            {
                ViewBag.Uyari = "Şifreler uyuşmamaktadır...!";
                return View();
            }
            else if (email != null)
            {
                ViewBag.Uyari = "Email kullanılmaktadır, lütfen farklı bir email giriniz...!";
                return View();
            }
            else
            {
                db.Kullanicis.Add(model);
                model.Rol = 2;
                model.Sifre = Crypto.Hash(sifre, "MD5");
                model.SifreTekrar = Crypto.Hash(sifretekrar, "MD5");
                db.SaveChanges();
                return RedirectToAction("Giris", "Oturum");
            }

        }

        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Giris(Kullanici data)
        {
            var sifre = Crypto.Hash(data.Sifre, "MD5");
            var kullanici = (from i in db.Kullanicis where i.Email.Equals(data.Email) && i.Sifre.Equals(sifre) select i).FirstOrDefault();
            if (kullanici != null)
            {
                Session["Id"] = kullanici.Id.ToString();
                Session["Email"] = kullanici.Email.ToString();
                Session["Rol"] = kullanici.Rol.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Uyari = "Email veya Şifre hatalı girilmiştir...!";
                return View();
            }

        }

        public ActionResult Cikis()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ProfilGuncelle()
        {
            var kullanicilar = (string)Session["Email"];
            var degerler = db.Kullanicis.FirstOrDefault(m => m.Email == kullanicilar);

            return View(degerler);
        }

        [HttpPost]
        public ActionResult ProfilGuncelle(Kullanici model)
        {
            if (ModelState.IsValid)
            {
                var kullanicilar = (string)Session["Email"];
                var user = db.Kullanicis.Where(m => m.Email == kullanicilar).FirstOrDefault();
                user.Ad = model.Ad;
                user.Soyad = model.Soyad;
                user.Email = model.Email;
                user.Sehir = model.Sehir;
                user.Ilce = model.Ilce;
                user.PostaKod = model.PostaKod;
                user.Adres = model.Adres;
                db.SaveChanges();
                ViewBag.Uyari = "Bilgileriniz Başarılı Bir Şekilde Değişmiştir...!";
                return View();
            }
            else
            {
                ViewBag.Uyari = "Bilgilerinizin Değişmesi Başarısız Oldu...!";
                return View();
            }

        }

        public ActionResult SifreDegistir()
        {
            var kullanicilar = (string)Session["Email"];
            var degerler = db.Kullanicis.FirstOrDefault(m => m.Email == kullanicilar);

            return View(degerler);
        }

        [HttpPost]
        public ActionResult SifreDegistir(Kullanici model)
        {
            var sifre = Crypto.Hash(model.Sifre, "MD5");
            var sifretekrar = Crypto.Hash(model.SifreTekrar, "MD5");
            if (ModelState.IsValid)
            {
                var kullanicilar = (string)Session["Email"];
                var user = db.Kullanicis.Where(m => m.Email == kullanicilar).FirstOrDefault();
                user.Sifre = sifre;
                user.SifreTekrar = sifretekrar;

                db.SaveChanges();
                ViewBag.Uyari = "Şifreniz Başarılı Bir Şekilde Değişmiştir...!";
                return View();
            }
            else
            {
                ViewBag.Uyari = "Şifrenizin Değişmesi Başarısız Oldu...!";
                return View();
            }
        }

        public ActionResult SiparisDurum()
        {
            var kullanici = (string)Session["Email"];
            var siparis = db.Siparis.Where(m => m.Email == kullanici).Select(m => new KullaniciSiparis
            {
                Id = m.Id,
                SiparisNo = m.SiparisNo,
                SiparisDurum = m.SiparisDurum,
                SiparisTarihi = m.SiparisTarihi,
                ToplamTutar = m.ToplamTutar

            }).OrderByDescending(m => m.SiparisTarihi).ToList();
            return View(siparis);
        }

        public ActionResult Detay(int id)
        {
            var model = db.Siparis.Where(m => m.Id == id).Select(m => new SiparisDetay()
            {
                SiparisId = m.Id,
                SiparisNo = m.SiparisNo,
                ToplamTutar = m.ToplamTutar,
                SiparisTarih = m.SiparisTarihi,
                SiparisDurum = m.SiparisDurum,
                Adres = m.Adres,
                Sehir = m.Sehir,
                Ilce = m.Ilce,
                Mahalle = m.Mahalle,
                PostaKod = m.PostaKod,
                SiparisLines = m.SiparisLine.Select(i => new SiparisLineModel()
                {
                    UrunId = i.UrunId,
                    Foto = i.Urun.Foto,
                    UrunAd = i.Urun.Ad,
                    Adet = i.Adet,
                    Fiyat = i.Fiyat
                }).ToList()

            }).FirstOrDefault();
            return View(model);
        }

    }
}