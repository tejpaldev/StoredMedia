using StoreyedMedia.DAL;
using StoreyedMedia.Infrastructure;
using StoreyedMedia.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System;

namespace StroreyedMedia.BAL
{
    public class StoryBal
    {
        #region Constants

        private readonly StoryDal _Story;
        private string _StoryAddedSuccess = "Story Added";
        private string _StoryAddedFailed = "Failed To Add Story";
        static string s3DirectoryName = "Story";


        #endregion

        #region Constructor

        public StoryBal()
        {
            _Story = new StoryDal();
        }

        #endregion

        #region Business Methods 

        #region Home

        /// <summary>
        /// Map stories,contacts,user
        /// </summary>
        /// <param name="storyListId"></param>
        /// <param name="storyId"></param>
        /// <param name="myList"></param>
        /// <param name="contacts"></param>
        /// <returns></returns>
        public Result AddStoriesToList(string storyListId, string storyId, string myList, string contacts, string isNew)
        {
            int detachFromContacts = (string.IsNullOrEmpty(contacts)) ? 1 : 0;
            int detachFromMyList = bool.Parse(myList) ? 0 : 1;
            int isNewRequest = 1;


            DataTable searchListTable = ConvertToDataTable(storyId, contacts, myList);
            bool result = _Story.AddStoryToList(int.Parse(storyId), int.Parse(storyListId), searchListTable, detachFromContacts, detachFromMyList, isNewRequest);
            Result repositoryResult = new Result { IsSuccess = result, Message = result ? _StoryAddedSuccess : _StoryAddedFailed };
            return repositoryResult;
        }

        /// <summary>
        /// Populate  Add story list popup
        /// </summary>
        /// <param name="storyId"></param>
        /// <returns></returns>
        public List<StoryList> GetStoriesList(string storyId)
        {
            List<StoryList> storyList = _Story.GetContactListOfUserForStory(int.Parse(storyId));
            return storyList;
        }

        /// <summary>
        /// Get Featured Stories
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <param name="orderByClause"></param>
        /// <returns></returns>
        public List<Story> GetFeaturedStories(int pageNumber, int pageSize, out int total, string orderByClause)
        {
            total = 0;
            List<Story> Stories = _Story.GetFeaturedStories(pageNumber, pageSize, orderByClause) ?? new List<Story>();
            foreach (var Story in Stories)
            {
                Story.StoryThumbnail = S3Cloud.IsValidGuid(Story.StoryThumbnail) ? S3Cloud.GetFileFromS3(Story.StoryThumbnail) : string.Empty;

            }
            return Stories;
        }

        /// <summary>
        /// Get Featured Slider Stories
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <param name="orderByClause"></param>
        /// <returns></returns>
        public List<Story> GetFeaturedSliderStories(int pageNumber, int pageSize, out int total, string orderByClause)
        {
            total = 0;
            List<Story> Stories = _Story.GetFeaturedSliderStories(pageNumber, pageSize, orderByClause) ?? new List<Story>();

            foreach (var Story in Stories)
            {

                Story.FeaturedImage = S3Cloud.IsValidGuid(Story.FeaturedImage) ? S3Cloud.GetFileFromS3(Story.FeaturedImage) : string.Empty;
                Story.SourceLogo = S3Cloud.IsValidGuid(Story.SourceLogo) ? S3Cloud.GetFileFromS3(Story.SourceLogo) : string.Empty;

            }
            return Stories;
        }

        public Story GetStoryById(int id)
        {
            Story results = _Story.GetStoryById(id);
            if (results != null && results.TagIdList != null)
            {
                DataTable dt = results.TagIdList;
                DataTable dtN = new DataTable();
                dtN.Columns.Add("Id");
                dtN.Columns.Add("Name");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string str = dt.Rows[i]["TagIdList"].ToString();
                    string[] values = str.Split('_');
                    DataRow dr = dtN.NewRow();
                    dr["Id"] = values[0];
                    dr["Name"] = values[1];
                    dtN.Rows.Add(dr);
                }
                results.TagIdList = dtN;
            }
            //results.TagIdList = dtN;
            return results;
        }

        public int GetStatus(string action)
        {
            int results = _Story.GetStatus(action);
            return results;
        }

        #endregion


        /// <summary>
        /// Get Story of a User
        /// </summary> 
        /// <returns></returns>
        public List<Story> GetAllStories(int pageNumber, int pageSize, out int total, string orderByClause)
        {
            total = 0;
            List<Story> Stories = _Story.GetAllStories(pageNumber, pageSize, orderByClause);
            return Stories;
        }

        /// <summary>
        /// Get total count of Story
        /// </summary> 
        /// <returns></returns>
        public int GetTotalStories()
        {
            return _Story.GetTotalStories();
        }



        /// <summary>
        /// Check if story alreay exist
        /// </summary>
        /// <param name="StoryId"></param>
        /// <returns></returns>
        public int IsStoryExist(string title, string storyLink)
        {
            int isStoryExist = _Story.IsStoryExist(title, storyLink);
            return isStoryExist;
        }

        /// <summary>
        /// Edit a Story
        /// </summary>
        /// <param name="Story"></param>
        /// <returns></returns>
        public Story EditStory(Story Story, HttpPostedFileBase darkLogoFile, HttpPostedFileBase lightLogoFile, List<HttpPostedFileBase> uploadMediafiles, HttpPostedFileBase featuredImageFile, List<int> tagIdList)
        {
            List<string> UploadMediaUrlList = new List<string>();
            //string featuredImageFileKey = null;
            //if (uploadMediafiles[0] != null)
            //{
            //    foreach (HttpPostedFileBase file in uploadMediafiles)
            //    {
            //        string UploadFileKey = S3Cloud.KeyGenerator();
            //        SaveImageToCloud(file, UploadFileKey);
            //        UploadMediaUrlList.Add(UploadFileKey);
            //    }
            //}
            //if (featuredImageFile != null)
            //{
            //    featuredImageFileKey = S3Cloud.KeyGenerator();
            //    SaveImageToCloud(featuredImageFile, featuredImageFileKey);
            //}
            UploadMediaUrlList.Add("bfe4d566-0e27-49a7-bed0-ce1e5863edd1");
            UploadMediaUrlList.Add("54513516-de75-4d31-9677-a743814ad70e");
            UploadMediaUrlList.Add("c104a03b-4cea-4935-bb75-b3dd3a001b6e");
            UploadMediaUrlList.Add("766aac3a-4eae-4ebb-957a-c682e6c77f56");
            Story.UploadMediaUrlList = ConvertToDataTable(UploadMediaUrlList);
            Story.TagIdList = ConvertToDataTable(tagIdList);
            //Story.FeaturedImage = featuredImageFileKey;
            Story.FeaturedImage = "bfe4d566-0e27-49a7-bed0-ce1e5863edd1";
            return _Story.EditStory(Story);

        }

        /// <summary>
        /// Get Media Type
        /// </summary> 
        /// <returns></returns>
        public List<Media> GetAllMediaType()
        {
            List<Media> MediaTypes = _Story.GetAllMediaType();
            return MediaTypes;
        }


        /// <summary>
        /// Get Edit Story Statuses
        /// </summary> 
        /// <returns></returns>
        public List<Story> GetEditStoryStatuses()
        {
            List<Story> editStoryStatus = _Story.GetEditStoryStatuses();
            return editStoryStatus;
        }

        /// <summary>
        /// Get Details of a Story
        /// </summary>
        /// <param name="storyId"></param>
        /// <returns></returns>
        public Story GetStoryDetailsById(int storyId)
        {
            var story = _Story.GetStoryDetailsById(storyId);
            story.FeaturedImage = S3Cloud.IsValidGuid(story.FeaturedImage) ? S3Cloud.GetFileFromS3(story.FeaturedImage, s3DirectoryName) : string.Empty;

            return story;
        }

        /// <summary>
        /// Get Related Stories
        /// </summary>
        /// <param name="storyId"></param>
        /// <returns></returns>
        public List<Story> GetRelatedStories(int storyId)
        {
            var stories = _Story.GetRelatedStories(storyId);

            if (stories != null && stories.Count > 0)
            {
                foreach (var story in stories)
                {
                    story.StoryThumbnail = S3Cloud.IsValidGuid(story.StoryThumbnail) ? S3Cloud.GetFileFromS3(story.StoryThumbnail, s3DirectoryName) : string.Empty;

                }
            }
            return stories;
        }

        /// <summary>
        /// Get Stories By Author
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public List<Story> GetStoriesByAuthor(string author)
        {
            if (!string.IsNullOrEmpty(author))
            {
                var stories = _Story.GetStoriesByAuthor(author);
                return stories;
            }
            return null;

        }

        /// <summary>
        /// Get Tags By StoryId
        /// </summary>
        /// <param name="storyId"></param>
        /// <returns></returns>
        public List<Tags> GetTagsByStoryId(int storyId)
        {
            var tags = _Story.GetTagsByStoryId(storyId);
            return tags;
        }


        public List<Comment> GetComments(int id)
        {
            List<Comment> results = _Story.GetComments(id);
            return results;
        }

        public int AddComment(int storyId, string description, string CreatedBy)
        {
            int results = _Story.AddComment(storyId, description, CreatedBy);
            return results;
        }
        #endregion

        #region Private Methods
        private DataTable ConvertToDataTable(List<string> listOfKeys)
        {

            var lenthObject = listOfKeys.Count();


            DataTable table = new DataTable();
            table.Columns.Add("[UploadMediaUrl]", typeof(string));


            for (int index = 0; index < lenthObject; index++)
            {
                table.Rows.Add(listOfKeys[index]);

            }


            return table;

        }


        private DataTable ConvertToDataTable(List<int> listOfIds)
        {

            var lenthObject = listOfIds.Count();


            DataTable table = new DataTable();
            table.Columns.Add("[TagId]", typeof(int));


            for (int index = 0; index < lenthObject; index++)
            {
                table.Rows.Add(listOfIds[index]);

            }


            return table;

        }

        private bool SaveImageToCloud(HttpPostedFileBase file, string key)
        {

            return S3Cloud.FileUpload(file, key);

        }

        private DataTable ConvertToDataTable(string storyId, string contacts, string myList)
        {
            string[] contactIds = contacts.Trim().Split(',');
            int storyIdprivate = int.Parse(storyId);
            string listType;
            int userId = CommonBase.LoggedInUserId;
            string storyStatus = "UnSent";

            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("StoryId", typeof(int));

            table.Columns.Add("ListType", typeof(string));
            table.Columns.Add("StoryStatus", typeof(string));
            if (!string.IsNullOrEmpty(contacts.TrimEnd()))
            {
                for (int index = 0; index <= contactIds.Length - 1; index++)
                {
                    listType = "Contact";
                    table.Rows.Add(contactIds[index], storyIdprivate, listType, storyStatus);
                }
            }
            if (bool.Parse(myList))
            {
                listType = "User";
                table.Rows.Add(userId, storyIdprivate, listType, storyStatus);
            }
            return table;
        }


        #endregion
        class TagList
        {
            public string Id { get; set; }

            public string Tags { get; set; }
        }

    }
}
