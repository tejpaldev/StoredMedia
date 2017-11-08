using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreyedMedia.Infrastructure;
using StoreyedMedia.Model;
using StroreyedMedia.BAL;

namespace StoreyedMedia.Web.Controllers
{
    public class TagsController : Controller
    {
        #region  Constants 

        private const string ContentType = "application/json";
        private const string FieldCreationDateFormat = "yyyy-MM-ddTHH:mm:ssZ";
        private static int CategoryId = 0;

        #endregion

        #region Constructor

        readonly StoreyedMedia.BAL.TagsBal _service;
        readonly StoreyedMedia.BAL.CategoriesBal _service1;

        /// <summary>
        /// initialize service in constructor
        /// </summary>
        public TagsController()
        {
            _service = new StoreyedMedia.BAL.TagsBal();
            _service1 = new BAL.CategoriesBal();

        }

        #endregion

        #region CRUD
        /// <summary>
        /// return view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("Tags");
        }

        /// <summary>
        /// Get all Tags
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetTags(int categoryId)
        {
            TempData["CategoryId"] = categoryId;
            CategoryId = categoryId;
            ViewData["CategoryId"] = GetAllCategoriesForTags();
            return Index();
        }

        /// <summary>
        /// Get all Tags
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAllTags(int page, int limit, string sortBy, string direction)
        {
            int total = 0;
            string orderByClause = sortBy + " " + direction;
            int categoryId = CategoryId;
            List<Tags> records = _service.GetAllTagsByCategoryId(categoryId, page, limit, out total, orderByClause);
            if (records != null && records[0].Category != null)
                TempData["Category"] = records[0].Category;
            else
                TempData["Category"] = "";
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
            // return View(records.ToList());
        }

        // <summary>
        // edit contact
        // </summary>
        // <param name = "tag" ></ param >
        // < returns ></ returns >
        [HttpPost]
        public ActionResult Save(Tags tags)
        {
            //category.UserId = 1;
            if (tags.TagId == 0)
            {
                tags.IsNew = true;
                tags.CreatedByUser = "T";
                tags.TagType = "Genres";
                tags.CategoryId = CategoryId;
                TempData["tagMessage"] = "Tag has been Added successfully";
            }
            else
            {
                TempData["tagMessage"] = "Tag has been updated successfully";
                tags.TagType = "Genres";
            }
            Tags result = _service.EditTag(tags);
            ViewData["CategoryId"] = GetAllCategoriesForTags();
            return Index();

        }

        // <summary>
        // Delete a tag based on identity of tag.
        // </summary>
        // <param name = "id" ></ param >
        // < returns ></ returns >
        [HttpPost]
        public JsonResult Remove(int TagId)
        {
            var result = _service.DeleteTag(TagId);
            TempData["deleteTag"] = "Tag has been deleted";
            return Json(Content(result.ToString(), ContentType));
        }

        private SelectList GetAllCategoriesForTags()
        {
            List<Categories> lstCategories = new List<Categories>();
            int total = _service1.GetTotalCategoriesCount();
            lstCategories = _service1.GetAllCategories(1, total, out total, null);
            SelectList categoryList = new SelectList(lstCategories, "CategoryId", "Category");
            return categoryList;
        }

        #endregion
    }
}