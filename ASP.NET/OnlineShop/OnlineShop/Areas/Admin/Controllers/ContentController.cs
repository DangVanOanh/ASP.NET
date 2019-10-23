using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }


        /// <summary>
        /// Set danh sach category lên dropdowlist
        /// </summary>
        /// <param name="categoryID"></param>
        public void SetViewBag(long? categoryID = null)
        {
            var listCategory = CategoryDAO.Instance.GetListAll();
            ViewBag.CatagoryId = new SelectList(listCategory, "ID", "Name", categoryID);
        }

    }
}
