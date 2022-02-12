using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eticaret2.Models;

namespace Eticaret2.Controllers
{
    public class SiparisController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            var siparis = db.Siparis.Select(m => new AdminSiparis()
            {
                Id = m.Id,
                SiparisNo = m.SiparisNo,
                SiparisTarihi = m.SiparisTarihi,
                SiparisDurum = m.SiparisDurum,
                ToplamTutar = m.ToplamTutar,
                Sayi = m.SiparisLine.Count
            }).OrderByDescending(m => m.SiparisTarihi).ToList();
            return View(siparis);
        }
    }
}