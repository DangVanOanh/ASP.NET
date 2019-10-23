using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Commond
{
    /// <summary>
    /// Dùng để lưu lại thông tin của UserLogin để có thể sử dụng trong toàn bộ chương trình
    /// </summary>
    [Serializable]
    public class UserLogin  
    {
        public long UserID { set; get; }
        public string UserName { set; get; }
    }
}