//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StorageApp.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Работники
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Работники()
        {
            this.Задачи = new HashSet<Задачи>();
            this.Задачи1 = new HashSet<Задачи>();
            this.Рейтинг = new HashSet<Рейтинг>();
        }
    
        public long ID_работника { get; set; }
        public string ФИО_работника { get; set; }
        public string Специальность { get; set; }
        public Nullable<long> ID_пользователя { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Задачи> Задачи { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Задачи> Задачи1 { get; set; }
        public virtual Пользователи Пользователи { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Рейтинг> Рейтинг { get; set; }
    }
}
