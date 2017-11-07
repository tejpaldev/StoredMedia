using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreyedMedia.Model;
using System.Data.SqlClient;
using StoreyedMedia.DAL.Mappers;

namespace StoreyedMedia.DAL
{
    public class TagsDal : DalBase
    {
        #region Repository Methods 
        /// <summary>
        /// Get Tags
        /// </summary>
        /// <returns></returns>
        public List<Tags> GetAllTags(int categoryId, int pageNumber, int pageSize, string orderByClause)
        {

            SqlCommand command = GetDbSprocCommand("GetAllTags");
            command.Parameters.Add(CreateParameter("@CategoryId", categoryId));
            command.Parameters.Add(CreateParameter("@PageNumber", pageNumber));
            command.Parameters.Add(CreateParameter("@PageSize", pageSize));
            command.Parameters.Add(CreateParameter("@OrderByClause", orderByClause, 20));

            return GetDtoList<Tags>(ref command);
        }

        /// <summary>
        /// Get total count
        /// </summary>
        /// <returns></returns>
        public int GetTotalTagsCount(int categoryId)
        {
            SqlCommand command = GetDbSprocCommand("GetTagsCount");
            command.Parameters.Add(CreateParameter("@CategoryId ", categoryId));
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

        protected static List<T> GetDtoList<T>(ref SqlCommand command) where T : ModelBase
        {
            List<T> dtoList = new List<T>();
            try
            {
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    // Get a parser for this DTO type and populate
                    // the ordinals.
                    IDataMapper parser = new MapperFactory().GetMapper(typeof(T));
                    //parser.PopulateOrdinals(reader); 
                    // Use the parser to build our list of DTOs.
                    while (reader.Read())
                    {
                        T dto = null;
                        dto = (T)parser.GetData(reader);
                        dtoList.Add(dto);
                        //reader.NextResult();
                    }
                    reader.Close();
                }
                else
                {
                    // Whenver there's no data, we return null.
                    dtoList = null;
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
            return dtoList;
        }


        /// <summary>
        /// Add/Update a tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Tags EditTag(Tags tag)
        {
            SqlCommand command = GetDbSprocCommand("SaveTag");
            command.Parameters.Add(CreateParameter("@TagId", tag.TagId));
            command.Parameters.Add(CreateParameter("@Tag", tag.Tag, 50));
            command.Parameters.Add(CreateParameter("@TagType", tag.TagType, 50));
            command.Parameters.Add(CreateParameter("@CategoryId", tag.CategoryId));
            command.Parameters.Add(CreateParameter("@IsEnabled", Convert.ToInt32(tag.IsEnabled)));
            if (tag.IsNew)
            {
                command.Parameters.Add(CreateParameter("@CreatedByUser", tag.CreatedByUser, 50));
            }
            else
            {
                command.Parameters.Add(CreateParameter("@LastModifiedByUser", tag.LastModifiedByUser, 50));

            }


            return GetSingleDto<Tags>(ref command);
        }

        /// <summary>
        /// Delete a tag
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public bool DeleteTag(int tagId)
        {
            SqlCommand command = GetDbSprocCommand("DeleteTag");
            command.Parameters.Add(CreateParameter("@TagId", tagId));
            return ExecuteNonQueryProcedures(ref command);
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
