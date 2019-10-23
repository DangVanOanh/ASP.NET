using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Commond
{
    public static class CommonConstants
    {
        public static string USER_SESSION = "USER_SESSION";

        public enum LoginCase
        {
            Login_UserNameDiffernce = -2,
            Login_UserNotExist,
            Login_PasswordDiffernce,
            Login_Success,
            Login_UserLock,
        }
    }
}