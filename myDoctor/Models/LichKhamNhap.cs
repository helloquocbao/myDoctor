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
    
    public partial class LichKhamNhap
    {
        public int idDatLichNhap { get; set; }
        public Nullable<int> idBacSi { get; set; }
        public string hoten { get; set; }
        public Nullable<int> tuoi { get; set; }
        public string sdt { get; set; }
        public Nullable<System.DateTime> ngaydat { get; set; }
        public string trieuchung { get; set; }
        public Nullable<bool> tinhTrang { get; set; }
    
        public virtual BacSi BacSi { get; set; }
    }
}
