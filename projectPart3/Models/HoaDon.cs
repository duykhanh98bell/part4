using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace projectPart3.Models
{
    [Table("hoadons")]
    public class HoaDon
    {
        [Key]
        public int ma_hd { get; set; }
        public int ma_kh { get; set;  }
        public int tong_tien { get; set; }
        public int trang_thai { get; set; }
        public string ghi_chu { get; set; }
        [DataType(DataType.Date)]
        public DateTime ngay_tao { get; set; }

        [ForeignKey("ma_kh")]
        public virtual KHACH khachs { get; set; }

        internal static object AsQueryable()
        {
            throw new NotImplementedException();
        }
    }
}