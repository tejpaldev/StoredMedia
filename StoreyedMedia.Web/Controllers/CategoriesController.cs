using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreyedMedia.Infrastructure;
using StoreyedMedia.Model;
using StroreyedMedia.BAL;
using System.Web.Mvc;

namespace StoreyedMedia.Web.Controllers
{
    public class CategoriesController : Controller
    {
        #region  Constants 

        publicconst string ContentType = "application/json";
        private const string FieldCreationDateFormat = "yyyy-MM-ddTHH:mm:ssZ";

        #endregion

        #region Constructor

        readonly StoreyedMedia.BAL.CategoriesBal _service;

        /// <summary>
        /// initialize service in constructor
        /// </summary>
        public CategoriesController()
        {
            _service = new StoreyedMedia.BAL.CategoriesBal();

        }

        #endregion

        #region CRUD
        /// <summary>
        /// return view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("Categories");
        }

        /// <summary>
        /// Get all Categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCategories(int page, int limit, string sortBy, string direction)
        {
            int total = 0;
            string orderByClause = sortBy + " " + direction;
            List<Categories> records = _service.GetAllCategories(page, limit, out total, orderByClause);
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        // <summary>
        // edit contact
        // </summary>
        // <param name = "category" ></ param >
        // < returns ></ returns >
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Categories categories, List<HttpPostedFileBase> files)
        {
            //category.UserId = 1;
            if (categories.CategoryId == 0)
            {
                categories.IsNew = true;
                categories.CreatedByUser = "T";
                Session["categoryMessage"] = "Category has been Added successfully";
            }
            else
                Session["categoryMessage"] = "Category has been updated successfully";
            if (files[0] == null)
                categories.IconUrl = null;
            Categories result = _service.EditCategory(categories, files[0]);
            return Index();

        }

        // <summary>
        // Delete a category based on identity of category.
        // </summary>
        // <param name = "categoryId" ></ param >
        // < returns ></ returns >
        [HttpPost]
        public JsonResult Remove(int categoryId)
        {
            var result = _service.DeleteCategory(categoryId);
            TempData["deleteMessage"] = "Category has been deleted";
            return Json(Content(result.ToString(), ContentType));
        }

        #endregion
    }
}
