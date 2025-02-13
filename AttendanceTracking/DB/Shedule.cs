//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AttendanceTracking.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shedule()
        {
            this.StudentShedule = new HashSet<StudentShedule>();
        }
    
        public int Id { get; set; }
        public int IdSpecialistGroup { get; set; }
        public System.DateTime DateStart { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
        public int IdCabinet { get; set; }
    
        public virtual Cabinet Cabinet { get; set; }
        public virtual SpecialistGroup SpecialistGroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentShedule> StudentShedule { get; set; }
    }
}
