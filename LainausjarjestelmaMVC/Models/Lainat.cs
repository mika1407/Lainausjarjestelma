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
    
    public partial class Lainat
    {
        public int lainaid { get; set; }
        public string tuoteid { get; set; }
        public Nullable<int> lainaajaid { get; set; }
        public Nullable<System.DateTime> lainapvm { get; set; }
        public Nullable<System.DateTime> palautuspvm { get; set; }
    }
}
