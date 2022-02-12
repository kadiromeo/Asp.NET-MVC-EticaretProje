using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret2.Models
{
    public class Siparis
    {
        public int Id { get; set; }
        public string SiparisNo { get; set; }
        public decimal ToplamTutar { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public SiparisDurum SiparisDurum { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Ilce { get; set; }
        public string Mahalle { get; set; }
        public string PostaKod { get; set; }
        public virtual List<SiparisLine> SiparisLine { get; set; }
    }
    public class SiparisLine
    {
        public int Id { get; set; }
        public int SiparisId { get; set; }
        public virtual Siparis Siparis { get; set; }
        public short Adet { get; set; }
        public decimal Fiyat { get; set; }
        public short UrunId { get; set; }
        public virtual Urun Urun { get; set; }
    }
}