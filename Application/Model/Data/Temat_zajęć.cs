//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Temat_zajęć
    {
        public int ID { get; set; }
        public string NumerZajęć { get; set; }
        public string Temat { get; set; }
        public int LiczbaGodzin { get; set; }
        public Nullable<int> Treść_ProgramowaID { get; set; }
    
        public virtual Treść_programowa Treść_programowa { get; set; }
    }
}
