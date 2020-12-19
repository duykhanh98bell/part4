using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectPart3.Models
{
    [Table("khachs")]
    public class KHACH
    {
        [Key]
        public int id_Khach { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User Name is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "PassWord is Required")]
        public string PassWord { get; set; }

        [Compare("PassWord", ErrorMessage = "Please Confirm Your Password.")]
        [DataType(DataType.Password)]
        public string ComfirmPassWord { get; set; }
    }
}