using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {


        //
        // GET: /Admin/User/

        public ActionResult Index(string searchString, int pageNumber = 1, int pageSize = 3)
        {
            var model = UserDAO.Instance.GetListAllUser(searchString, pageNumber, pageSize);
            ViewBag.searchString = searchString;
            return View(model);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = UserDAO.Instance;
                user.Password = Commond.Encryptor.MD5Hash(user.Password);
                long userID = dao.Insert(user);

                if (userID > 0)
                {
                    return RedirectToAction("index", "User");
                }
                else
                {
                    ModelState.AddModelError("", Resource.CreateErrorExitsUser);
                }
            }

            return View("Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new User();
            var result = UserDAO.Instance.GetByUserID(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Commond.Encryptor.MD5Hash(user.Password);
                var result = UserDAO.Instance.Update(user);
                if (result)
                {
                    return RedirectToAction("index", "User");
                }
                else
                {
                    ModelState.AddModelError("", Resource.UserUpdateError);
                }
            }

            return View("Index");
        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var result = UserDAO.Instance.Delete(id);
                if (result)
                {
                    return RedirectToAction("index", "User");
                }
                else
                {
                    ModelState.AddModelError("", Resource.UserDeleteError);
                }
            }

            return View("Index");
        }


        /// <summary>
        /// Thay đổi trang thái
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = UserDAO.Instance.ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

    }
}
