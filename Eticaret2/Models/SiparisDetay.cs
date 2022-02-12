using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret2.Models
{
    public class SiparisDetay
    {
        public int SiparisId { get; set; }
        public string SiparisNo { get; set; }
        public decimal ToplamTutar { get; set; }
        public DateTime SiparisTarih { get; set; }
        public SiparisDurum SiparisDurum { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Ilce { get; set; }
        public string Mahalle { get; set; }
        public string PostaKod { get; set; }
        public virtual List<SiparisLineModel> SiparisLines { get; set; }
    }
    public class SiparisLineModel
    {
        public short UrunId { get; set; }
        public string UrunAd { get; set; }
        public short Adet { get; set; }
        public decimal Fiyat { get; set; }
        public string Foto { get; set; }
    }
}