using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace projectPart3.Models
{
    [Table("thuonghieus")]
    public class ThuongHieu
    {
        [Key]
        public int ma_thuong_hieu { get; set; }
        public string ten_thuong_hieu { get; set; }
    }
}