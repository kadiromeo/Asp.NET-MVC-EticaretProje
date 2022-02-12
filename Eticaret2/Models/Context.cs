using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Eticaret2.Models
{
    public class Context:DbContext
    {
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Kullanici> Kullanicis { get; set; }
        public DbSet<Urun> Uruns { get; set; }
        public DbSet<Yorum> Yorums { get; set; }
        public DbSet<Siparis> Siparis { get; set; }
        public DbSet<SiparisLine> SiparisLines { get; set; }
        public DbSet<AdminSiparis> AdminSiparis { get; set; }

    }
}