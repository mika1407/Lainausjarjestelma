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

    public partial class Lainaukset
    {
        public int LainausID { get; set; }
        public string Tuote { get; set; }
        public string Lainaaja { get; set; }

        [Display(Name = "Lainauspäivä")]
        [DataType(DataType.Time)]
        [DisplayFormatAttribute(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> Lainauspaiva { get; set; }

        [Display(Name = "Palautuspäivä")]
        [DataType(DataType.Time)]
        [DisplayFormatAttribute(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> Palautuspaiva { get; set; }

        public string Varastopaikka { get; set; }
        public Nullable<int> TuoteID { get; set; }
        public Nullable<int> LainaajaID { get; set; }
        public Nullable<int> VarastoID { get; set; }
    
        public virtual Lainaajat Lainaajat { get; set; }
        public virtual Tuotteet Tuotteet { get; set; }
        public virtual Varastot Varastot { get; set; }
    }
}
