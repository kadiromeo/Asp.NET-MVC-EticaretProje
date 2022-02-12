using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret2.Models
{
    public class Kullanici
    {
        public short Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
        public string SifreTekrar { get; set; }
        public string Sehir { get; set; }
        public string Ilce { get; set; }
        public string PostaKod { get; set; }
        public string Adres { get; set; }
        public byte Rol { get; set; }
        public List<Yorum> Yorums  { get; set; }
    }
}