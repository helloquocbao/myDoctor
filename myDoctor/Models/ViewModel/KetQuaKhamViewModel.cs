using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myDoctor.Models.ViewModel
{
    public class KetQuaKhamViewModel
    {
        public string tenbs { get; set; }
        public int? idBacSi { get; set; }
        public int idKetQua { get; set; }
        public Nullable<int> idDatLich { get; set; }
        public string ketqua { get; set; }
        public string hdthuoc { get; set; }
        public string ChuyenKhoa { get; set; }
        public string HocVi1 { get; set; }
        public Nullable<int> tienkham { get; set; }
    }
}