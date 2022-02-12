using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret2.Models
{
    public class Yorum
    {
        public short Id { get; set; }
        public DateTime Tarih { get; set; }
        public string Icerik { get; set; }
        public byte Onay { get; set; }
        public float Puan { get; set; }
        public Urun Urun { get; set; }
        public short UrunId { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public short KullaniciId { get; set; }
    }
}