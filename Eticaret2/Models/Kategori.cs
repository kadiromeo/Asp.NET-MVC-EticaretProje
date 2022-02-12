using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret2.Models
{
    public class Kategori
    {
        public short Id { get; set; }
        public string Ad { get; set; }
        public List<Urun> Uruns { get; set; }
    }
}