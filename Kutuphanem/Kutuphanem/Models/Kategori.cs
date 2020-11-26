namespace Kutuphanem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kategori")]
    public partial class Kategori
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string KategoriAdi { get; set; }
    }
}
