namespace Kutuphanem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kitap")]
    public partial class Kitap
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string KitapAdi { get; set; }

        [StringLength(50)]
        public string Yazar { get; set; }

        [StringLength(50)]
        public string Kategori { get; set; }

        public int? Miktar { get; set; }

        public double? Fiyat { get; set; }
        public string Resim { get; set; }
      
    }
}
