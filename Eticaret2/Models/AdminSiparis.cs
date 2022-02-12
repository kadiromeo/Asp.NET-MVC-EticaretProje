using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret2.Models
{
    public class AdminSiparis
    {
        public int Id { get; set; }
        public string SiparisNo { get; set; }
        public decimal ToplamTutar { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public SiparisDurum SiparisDurum { get; set; }
        public int Sayi { get; set; }
    }
}