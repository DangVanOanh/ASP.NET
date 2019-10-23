using Models;
using Models.DAO;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Commond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Admin/Login/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = UserDAO.Instance;
                var result = dao.Login(model.userName, Encryptor.MD5Hash(model.passWord));
                CommonConstants.LoginCase myStatus;
                Enum.TryParse(result, out  myStatus);
                switch (myStatus)
                {
                    case CommonConstants.LoginCase.Login_UserNotExist:
                        ModelState.AddModelError(Resource.User, Resource.Login_UserNotExistMsg);
                        break;

                    case CommonConstants.LoginCase.Login_UserNameDiffernce:
                        ModelState.AddModelError(Resource.User, Resource.Login_UserNameDiffernceMsg);
                        break;

                    case CommonConstants.LoginCase.Login_PasswordDiffernce:
                        ModelState.AddModelError(Resource.User, Resource.Login_PasswordDiffernceMsg);
                        break;

                    case CommonConstants.LoginCase.Login_Success:

                        var user = dao.GetByUserName(model.userName);
                        var userSession = new UserLogin();
                        userSession.UserName = user.UserName;
                        userSession.UserID = user.ID;

                        Session.Add(CommonConstants.USER_SESSION, userSession);
                        return RedirectToAction("Index", "Home");

                    case CommonConstants.LoginCase.Login_UserLock:
                        ModelState.AddModelError(Resource.User, Resource.Login_UserLockMsg);
                        break;
                    default:
                        break;
                }
            }

            return View("Index");
        }

    }
}
