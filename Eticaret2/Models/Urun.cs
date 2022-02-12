using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret2.Models
{
    public class Urun
    {
        public short Id { get; set; }
        public string Ad { get; set; }
        public string Marka { get; set; }
        public string Stok { get; set; }
        public decimal Fiyat { get; set; }
        public string Aciklama { get; set; }
        public string Foto { get; set; }
        public Kategori Kategori { get; set; }

        public List<Yorum> Yorums { get; set; }
    }
}