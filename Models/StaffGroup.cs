//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StaffGroup")]
    public partial class StaffGroup
    {
        public int Id { get; set; }
        public Nullable<int> GroupId { get; set; }
        public Nullable<int> StaffId { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }

        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        [ForeignKey("StaffId")]
        public virtual Staff Staff { get; set; }
    }
}
