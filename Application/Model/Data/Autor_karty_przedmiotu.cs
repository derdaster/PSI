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
    
    public partial class Autor_karty_przedmiotu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Autor_karty_przedmiotu()
        {
            this.Karta_przedmiotu_Autor_karty_przedmiotu = new HashSet<Karta_przedmiotu_Autor_karty_przedmiotu>();
            this.Karta_przedmiotu = new HashSet<Karta_przedmiotu>();
        }
    
        public int ID { get; set; }
        public string Imię { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Karta_przedmiotu_Autor_karty_przedmiotu> Karta_przedmiotu_Autor_karty_przedmiotu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Karta_przedmiotu> Karta_przedmiotu { get; set; }
    }
}
