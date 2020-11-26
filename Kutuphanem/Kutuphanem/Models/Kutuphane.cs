namespace Kutuphanem.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

    public partial class Kutuphane : DbContext
    {
        public Kutuphane()
            : base("name=Kutuphane")
        {
        }

        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<Kitap> Kitap { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<Siparis> Siparis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        internal static IEnumerable<object> Where(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }

        public System.Data.Entity.DbSet<Kutuphanem.Models.SatinAlmaSiparis> SatinAlmaSiparis { get; set; }
    }
}
