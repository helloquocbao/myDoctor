//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace myDoctor.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietThuoc
    {
        public int idctthuoc { get; set; }
        public Nullable<int> idKetQua { get; set; }
        public Nullable<int> idthuoc { get; set; }
        public Nullable<int> soluong { get; set; }
    
        public virtual KetQuaKham KetQuaKham { get; set; }
        public virtual Thuoc Thuoc { get; set; }
    }
}
