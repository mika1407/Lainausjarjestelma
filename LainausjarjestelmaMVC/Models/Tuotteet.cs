//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LainausjarjestelmaMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Tuotteet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tuotteet()
        {
            this.Lainaukset = new HashSet<Lainaukset>();
        }
    
        public int TuoteID { get; set; }

        [Display(Name = "Tuote")]
        public string Nimi { get; set; }
        public string Kotivarasto { get; set; }
        public string Kuva { get; set; }
        public string Tila { get; set; }
        public string Lainaaja { get; set; }

        [Display(Name = "Lainauspäivä")]
        [DataType(DataType.Time)]
        [DisplayFormatAttribute(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Lainauspaiva { get; set; }

        [Display(Name = "Palautuspäivä")]
        [DataType(DataType.Time)]
        [DisplayFormatAttribute(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Palautuspaiva { get; set; }
        public string Varastopaikka { get; set; }
        public Nullable<int> LainaajaID { get; set; }
        public Nullable<int> VarastoID { get; set; }
    
        public virtual Lainaajat Lainaajat { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lainaukset> Lainaukset { get; set; }
        public virtual Varastot Varastot { get; set; }
    }
}
