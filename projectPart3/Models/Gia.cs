using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace projectPart3.Models
{
    [Table("gias")]
    public class Gia
    {
        [Key]
        public int ma_gia { get; set; }
        public int gia_goc { get; set; }
        public int gia_khuyen_mai { get; set; }

        [DataType(DataType.Date)]
        public DateTime ngay_ap_dung { get; set; }
    }
}