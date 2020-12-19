using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace projectPart3.Models
{
    [Table("sanphams")]
    public class SanPham
    {
        [Key]
        public int ma_sp { get; set; }
        public string ten_sp { get; set; }

        public int ma_gia { get; set; }
        
        [ForeignKey("ma_gia")]
        public virtual Gia gias { get; set; }

        public int ma_ncc { get; set; }
        [ForeignKey("ma_ncc")]
        public virtual NhaCungCap nhacungcaps { get; set; }

        public int ma_danh_muc { get; set; }
        [ForeignKey("ma_danh_muc")]
        public virtual DanhMuc danhmucs { get; set; }
        public bool trang_thai { get; set; }
        public string ghi_chu { get; set; }

        internal object Find(int ma_san_pham)
        {
            throw new NotImplementedException();
        }

        public string xuat_xu { get; set; }
        public string mo_ta { get; set; }
        public string hinh_anh { get; set; }

    }
}