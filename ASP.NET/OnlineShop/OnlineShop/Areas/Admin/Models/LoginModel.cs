using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    /// <summary>
    /// Lớp này dùng để định nghĩa các trường hay đối tượng login gồm những thông tin nào.
    /// </summary>
    public class LoginModel
    {
        [Required(ErrorMessage="Mời nhập userName")]
        public string userName { set; get; }

        [Required(ErrorMessage = "Mời nhập passWord")]
        public string passWord { set; get; }

        public bool remember { set; get; }
    }
}