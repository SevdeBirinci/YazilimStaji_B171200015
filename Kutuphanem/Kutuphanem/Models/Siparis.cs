namespace Kutuphanem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class Siparis
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string SiparisAd { get; set; }

        public int? KitapID { get; set; }

        public int? KullaniciID { get; set; }

        public int? Miktar { get; set; }

        public double? Tutar { get; set; }
      
        public int? Toplam { get; set; }
        public bool? SiparisOnay { get; set; }
        public bool? SOnay { get; set; }

    }
}
