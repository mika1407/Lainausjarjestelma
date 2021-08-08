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

    public partial class Lainaajat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lainaajat()
        {
            this.Lainaukset = new HashSet<Lainaukset>();
            this.Tuotteet = new HashSet<Tuotteet>();
        }
    
        public int LainaajaID { get; set; }

        [Display(Name = "Lainaaja")]
        public string Etunimi { get; set; }

        [Display(Name = "Lainaaja")]
        public string Sukunimi { get; set; }
        public string Email { get; set; }
        public string Puhelinnumero { get; set; }
        public Nullable<int> LoginID { get; set; }
    
        public virtual Logins Logins { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lainaukset> Lainaukset { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tuotteet> Tuotteet { get; set; }
    }
}
