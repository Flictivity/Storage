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
    
    public partial class Рейтинг
    {
        public long ID_рейтинга { get; set; }
        public long ID_работника { get; set; }
        public long ID_группы { get; set; }
    
        public virtual Группа_работников Группа_работников { get; set; }
        public virtual Работники Работники { get; set; }
    }
}
