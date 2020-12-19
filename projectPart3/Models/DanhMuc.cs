using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace projectPart3.Models
{
    [Table("danhmucs")]
    public class DanhMuc
    {
        [Key]
        public int ma_danh_muc { get; set; }
        public string ten_danh_muc { get; set; }
    }
}