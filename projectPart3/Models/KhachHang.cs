using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectPart3.Models
{
    [Table("khachhangs")]
    public class KhachHang
    {
        [Key]
        public int ma_khach_hang { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User Name is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "PassWord is Required")]
        public string PassWord { get; set; }

        [Compare("PassWord",ErrorMessage = "Please Confirm Your Password.")]
        [DataType(DataType.Password)]
        public string ComfirmPassWord { get; set; }
    }
}