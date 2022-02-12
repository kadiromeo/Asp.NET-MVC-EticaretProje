using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret2.Models
{
    public class Kart
    {
        private List<KartLine> _kartLines = new List<KartLine>();
        public List<KartLine> KartLines
        {
            get { return _kartLines; }
        }

        public void UrunEkle(Urun urun, short adet)
        {
            var line = _kartLines.FirstOrDefault(m => m.Urun.Id == urun.Id);
            if (line==null)
            {
                _kartLines.Add(new KartLine {Urun=urun, Adet=adet});
            }
            else
            {
                line.Adet += adet;
            }
        }

        public void UrunSil(Urun urun)
        {
            _kartLines.RemoveAll(m => m.Urun.Id == urun.Id);
        }

        public decimal Toplam()
        {
            return _kartLines.Sum(m => m.Urun.Fiyat * m.Adet);
        }

        public void Temizle()
        {
            _kartLines.Clear();
        }

    }

    public class KartLine
    {
        public Urun Urun { get; set; }
        public short Adet { get; set; }
    }
}