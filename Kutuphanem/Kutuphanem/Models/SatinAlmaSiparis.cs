using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kutuphanem.Models
{
    public class SatinAlmaSiparis
    {
        public int ID { get; set; }
        public int? KitapID { get; set; }

        public int? KullaniciID { get; set; }

        public int? Miktar { get; set; }

        public double? Tutar { get; set; } 
        public int? GelenMiktar { get; set; }
        public bool Onay { get; set; }
        public bool SepetOnay { get; set; }

    }
}