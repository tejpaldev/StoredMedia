using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreyedMedia.DAL;
using StoreyedMedia.Infrastructure;
using StoreyedMedia.Model;
using System.Web;

namespace StoreyedMedia.BAL
{
    public class CategoriesBal
    {
        #region Constants

        private readonly CategoriesDal _Categories;


        #endregion

        #region Constructor

        public CategoriesBal()
        {
            _Categories = new CategoriesDal();
        }

        #endregion

        #region Business Methods 

        /// <summary>
        /// Get Categories
        /// </summary>
        /// <returns></returns>
        public List<Categories> GetAllCategories(int pageNumber, int pageSize, out int total, string orderByClause)
        {
            total = GetTotalCategoriesCount();
            List<Categories> lstCategory = _Categories.GetAllCategories(pageNumber, pageSize, orderByClause);
            foreach (var category in lstCategory)
            {
                category.IconUrl = S3Cloud.IsValidGuid(category.IconUrl) ? S3Cloud.GetFileFromS3(category.IconUrl) : string.Empty;
                //category.IconUrl = S3Cloud.GetFileFromS3(category.IconUrl);
            }
            return lstCategory;
        }


        /// <summary>
        /// Get total count of Categories
        /// </summary>
        /// <returns></returns>
        public int GetTotalCategoriesCount()
        {
            return _Categories.GetTotalCategoriesCount();
        }

        /// <summary>
        /// Edit a category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public Categories EditCategory(Categories category, HttpPostedFileBase files)
        {
            if (files != null)
            {
                var categoryIcon = files;
                category.IconUrl = S3Cloud.KeyGenerator();
                SaveImageToCloud(categoryIcon, category.IconUrl);
            }
            return _Categories.EditCategory(category);

        }


        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool DeleteCategory(int categoryId)
        {
            return _Categories.DeleteCategory(categoryId);
        }
        #endregion

        #region Private Methods

        private bool SaveImageToCloud(HttpPostedFileBase file, string key)
        {

            return S3Cloud.FileUpload(file, key);

        }
        #endregion
    }
}
