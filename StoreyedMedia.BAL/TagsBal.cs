using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreyedMedia.DAL;
using StoreyedMedia.Infrastructure;
using StoreyedMedia.Model;

namespace StoreyedMedia.BAL
{
    public class TagsBal
    {
        #region Constants

        private readonly TagsDal _Tags;


        #endregion

        #region Constructor

        public TagsBal()
        {
            _Tags = new TagsDal();
        }

        #endregion

        #region Business Methods 

        /// <summary>
        /// Get Tags
        /// </summary>
        /// <returns></returns>
        public List<Tags> GetAllTags(int categoryId, int pageNumber, int pageSize, out int total, string orderByClause)
        {
            total = GetTotalTagsCount(categoryId);
            return _Tags.GetAllTags(categoryId, pageNumber, pageSize, orderByClause);
        }


        /// <summary>
        /// Get total count of Tags
        /// </summary>
        /// <returns></returns>
        public int GetTotalTagsCount(int categoryId)
        {
            return _Tags.GetTotalTagsCount(categoryId);
        }

        /// <summary>
        /// Edit a Tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Tags EditTag(Tags tag)
        {
            return _Tags.EditTag(tag);

        }

        /// <summary>
        /// Delete a Tag
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public bool DeleteTag(int tagId)
        {
            return _Tags.DeleteTag(tagId);
        }
        #endregion

    }
}
