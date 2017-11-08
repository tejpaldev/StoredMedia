using StoreyedMedia.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StoreyedMedia.DAL
{
    public class StoryDal : DalBase
    {


        #region Repository Methods 

        /// <summary>
        /// Validate during Edit
        /// </summary>
        /// <param name="Story"></param>
        /// <returns></returns>
        public Story ValidateStory(Story Story)
        {
            return null;
        }

        /// <summary>
        /// Get Stories 
        /// </summary> 
        /// <returns></returns>
        public List<Story> GetAllStories(int pageNumber, int pageSize, string orderByClause)
        {

            SqlCommand command = GetDbSprocCommand("GetStories");
            command.Parameters.Add(CreateParameter("@PageNumber", pageNumber));
            command.Parameters.Add(CreateParameter("@PageSize", pageSize));
            command.Parameters.Add(CreateParameter("@OrderByClause", orderByClause, 20));

            return GetDtoList<Story>(ref command);
        }



        /// <summary>
        /// Get total count
        /// </summary> 
        /// <returns></returns>
        public int GetTotalStories()
        {
            SqlCommand command = GetDbSprocCommand("GetStoriesCount");
            int result = 0;
            try
            {
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0))
                    {
                        result = reader.GetInt32(0);
                    }

                    reader.Close();
                }

            }
            catch (Exception e)
            {
                throw new Exception("Error populating data", e);
            }
            finally
            {
                command.Connection.Close();
                command.Connection.Dispose();
            }
            return result;
        }

        /// <summary>
        /// Get Details of a Story
        /// </summary>
        /// <param name="StoryId"></param>
        /// <returns></returns>
        public Story GetStoryDetailsById(int StoryId)
        {
            SqlCommand command = GetDbSprocCommand("GetStoryByID");
            command.Parameters.Add(CreateParameter("@StoryId", StoryId));
            return GetSingleDto<Story>(ref command);
        }


        /// <summary>
        /// Check if story alreay exist
        /// </summary>
        /// <param name="StoryId"></param>
        /// <returns></returns>
        public int IsStoryExist(string title, string storyLink)
        {
            SqlCommand command = GetDbSprocCommand("IsStoryExist");
            command.Parameters.Add(CreateParameter("@Title", title, 500));
            command.Parameters.Add(CreateParameter("@StoryLink", storyLink, 50));
            SqlParameter parm3 = new SqlParameter("@IsExists", SqlDbType.Int);
            parm3.Direction = ParameterDirection.Output;
            command.Parameters.Add(parm3);
            int result = 0;
            try
            {
                command.Connection.Open();
                command.ExecuteNonQuery();
                int IsExists = Convert.ToInt32(command.Parameters["@IsExists"].Value.ToString());
                result = IsExists;

            }
            catch (Exception e)
            {
                throw new Exception("Error populating data", e);
            }
            finally
            {
                command.Connection.Close();
                command.Connection.Dispose();
            }
            return result;
        }

        /// <summary>
        /// Add/Update a Story
        /// </summary>
        /// <param name="Story"></param>
        /// <returns></returns>
        public Story EditStory(Story Story)
        {
            SqlCommand command = GetDbSprocCommand("SaveStory");

            command.Parameters.Add(CreateParameter("@StoryId", Story.StoryId));
            command.Parameters.Add(CreateParameter("@DatePosted", Story.DatePosted));
            command.Parameters.Add(CreateParameter("@Title", Story.Title, 2000));
            command.Parameters.Add(CreateParameter("@FeaturedImage", Story.FeaturedImage, 500));
            command.Parameters.AddWithValue("@FeatureOnHomepage", Story.FeatureOnHomepage);
            //command.Parameters.Add(CreateParameter("@StoryImage", Story.StoryImage, 50));
            command.Parameters.Add(CreateParameter("@Synopsis", Story.Synopsis, 2000));
            command.Parameters.Add(CreateParameter("@Logline", Story.LogLine, 150));
            command.Parameters.Add(CreateParameter("@SubmittedBy", Story.SubmittedById));
            command.Parameters.Add(CreateParameter("@PublishedBy", Story.PublishedById));
            command.Parameters.Add(CreateParameter("@TagIdList", Story.TagIdList));
            command.Parameters.Add(CreateParameter("@URL", Story.StoryLink, 50));
            command.Parameters.Add(CreateParameter("@Author", Story.Author, 50));
            command.Parameters.Add(CreateParameter("@DateSrcPublished", Convert.ToDateTime(Story.DateSrcPublished)));
            command.Parameters.Add(CreateParameter("@SourceId", Story.SourceId));
            command.Parameters.Add(CreateParameter("@StatusId", Story.StatusId));
            command.Parameters.Add(CreateParameter("@MediaTypeId", Story.MediaTypeId));

            command.Parameters.Add(CreateParameter("@UploadMediaUrlList", Story.UploadMediaUrlList));
            if (Story.IsNew)
            {
                command.Parameters.Add(CreateParameter("@CreatedByUser", Story.CreatedByUser, 50));
            }
            else
            {
                command.Parameters.Add(CreateParameter("@LastModifiedByUser", Story.LastModifiedByUser, 50));
            }

            return GetSingleDto<Story>(ref command);
        }

        public Story GetStoryById(int id)
        {
            SqlCommand command = GetDbSprocCommand("GetStoryById");

            command.Parameters.Add(CreateParameter("@StoryId", id));

            return GetSingleDto<Story>(ref command);
        }


        public List<Comment> GetComments(int id)
        {
            SqlCommand command = GetDbSprocCommand("GetComments");
            command.Parameters.Add(CreateParameter("@StoryId", id));
            return GetDtoList<Comment>(ref command);
        }

        public int AddComment(int storyId, string description,string CreatedBy)
        {
            SqlCommand command = GetDbSprocCommand("SaveComments");
            command.Parameters.Add(CreateParameter("@storyId", storyId));
            command.Parameters.Add(CreateParameter("@Description", description, 40));
            command.Parameters.Add(CreateParameter("@CreatedBy", CreatedBy, 50));

            int result = 0;
            try
            {
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0))
                    {
                        result = Convert.ToInt32(reader["StatusId"]);
                    }
                    reader.Close();
                }

            }
            catch (Exception e)
            {
                throw new Exception("Error populating data", e);
            }
            finally
            {
                command.Connection.Close();
                command.Connection.Dispose();
            }
            return result;

        }


        public int GetStatus(string action)
        {
            SqlCommand command = GetDbSprocCommand("GetSaveTypeStatus");
            command.Parameters.Add(CreateParameter("@action", action, 40));

            int result = 0;
            try
            {
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0))
                    {
                        result = Convert.ToInt32(reader["StatusId"]);
                    }
                    reader.Close();
                }

            }
            catch (Exception e)
            {
                throw new Exception("Error populating data", e);
            }
            finally
            {
                command.Connection.Close();
                command.Connection.Dispose();
            }
            return result;

        }



        #region Home

        /// <summary>
        /// Add story to list
        /// </summary>
        /// <param name="searchListTable"></param>
        /// <returns></returns>
        public bool AddStoryToList(int storyId, int storyListId, DataTable searchListTable, Int32 detachFromContacts, Int32 detachFromMyList, Int32 isNewRequest)
        {
            SqlCommand command = GetDbSprocCommand("AddStoryToList");
            command.Parameters.Add(CreateParameter("@SearchListTable", searchListTable));
            command.Parameters.Add(CreateParameter("@StoryId", storyId));
            command.Parameters.Add(CreateParameter("@UserId", CommonBase.LoggedInUserId));
            //command.Parameters.Add(CreateParameter("@detachFromContacts", detachFromContacts));
            //command.Parameters.Add(CreateParameter("@detachFromMyList", detachFromMyList));
            //command.Parameters.Add(CreateParameter("@isNewRequest", isNewRequest));


            //command.Parameters.Add(CreateParameter("@StoryListId", storyListId));
            if (storyListId == 0)
            {
                command.Parameters.Add(CreateParameter("@CreatedByUser", CommonBase.LoggedInUser, 50));
            }
            else
            {
                command.Parameters.Add(CreateParameter("@LastModifiedByUser", CommonBase.LoggedInUser, 50));

            }
            return ExecuteNonQueryProcedures(ref command);
        }

        /// <summary>
        /// Get Featured Stories
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByClause"></param>
        /// <returns></returns>
        public List<Story> GetFeaturedStories(int pageNumber, int pageSize, string orderByClause)
        {

            SqlCommand command = GetDbSprocCommand("GetFeaturedStories");
            command.Parameters.Add(CreateParameter("@PageNumber", pageNumber));
            command.Parameters.Add(CreateParameter("@PageSize", pageSize));
            command.Parameters.Add(CreateParameter("@OrderByClause", orderByClause, 20));

            return GetDtoList<Story>(ref command);
        }

        /// <summary>
        ///  Get Featured SliderStories
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByClause"></param>
        /// <returns></returns>
        public List<Story> GetFeaturedSliderStories(int pageNumber, int pageSize, string orderByClause)
        {

            SqlCommand command = GetDbSprocCommand("GetFeaturedSliderStories");
            command.Parameters.Add(CreateParameter("@PageNumber", pageNumber));
            command.Parameters.Add(CreateParameter("@PageSize", pageSize));
            command.Parameters.Add(CreateParameter("@OrderByClause", orderByClause, 20));

            return GetDtoList<Story>(ref command);
        }


        /// <summary>
        /// contact list for add to list popup
        /// </summary>
        /// <param name="storyId"></param>
        /// <returns></returns>
        public List<StoryList> GetContactListOfUserForStory(int storyId)
        {

            SqlCommand command = GetDbSprocCommand("GetContactListOfUserForStory");
            command.Parameters.Add(CreateParameter("@StoryId", storyId));
            command.Parameters.Add(CreateParameter("@UserId", CommonBase.LoggedInUserId));

            return GetDtoList<StoryList>(ref command);
        }

        /// <summary>
        /// Get Media Types 
        /// </summary> 
        /// <returns></returns>
        public List<Media> GetAllMediaType()
        {

            SqlCommand command = GetDbSprocCommand("GetAllMediaType");
            return GetDtoList<Media>(ref command);
        }

        /// <summary>
        /// Get Edit Story Statuses
        /// </summary> 
        /// <returns></returns>
        public List<Story> GetEditStoryStatuses()
        {
            SqlCommand command = GetDbSprocCommand("GetEditStoryStatus");
            List<Story> lstStory = new List<Story>();
            try
            {
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Story s = new Story();
                        s.StatusId = Convert.ToInt32(reader["StatusId"]);
                        s.Status = Convert.ToString(reader["Status"]);
                        lstStory.Add(s);
                    }
                    reader.Close();
                }

            }
            catch (Exception e)
            {
                throw new Exception("Error populating data", e);
            }
            finally
            {
                command.Connection.Close();
                command.Connection.Dispose();
            }
            return lstStory;
        }
        #endregion


        #region Private Methods 


        /// <summary>
        /// Execute non query procedures.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private bool ExecuteNonQueryProcedures(ref SqlCommand command)
        {
            bool result = false;
            try
            {
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows || reader.RecordsAffected > 0)
                {
                    //reader.Read();
                    result = true;
                    //   PopulateOrdinals(reader);
                    //if (!reader.IsDBNull(_ordinalIsStarred))
                    //{
                    //    result = reader.GetBoolean(_ordinalResult);
                    //}

                    reader.Close();
                }

            }
            catch (Exception e)
            {
                throw new Exception("Error populating data", e);
            }
            finally
            {
                command.Connection.Close();
                command.Connection.Dispose();
            }
            // return the success/failure, it's either populated with data or null.
            return result;
        }



        #endregion
    }
}
#endregion