using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreyedMedia.Model;
using System.Data.SqlClient;
using StoreyedMedia.DAL.Mappers;
using System.Web;

namespace StoreyedMedia.DAL
{
    public class CategoriesDal : DalBase
    {
        #region Repository Methods 

        /// <summary>
        /// Get Categories
        /// </summary>
        /// <returns></returns>
        public List<Categories> GetAllCategories(int pageNumber, int pageSize, string orderByClause)
        {

            SqlCommand command = GetDbSprocCommand("GetAllCategories");
            command.Parameters.Add(CreateParameter("@PageNumber", pageNumber));
            command.Parameters.Add(CreateParameter("@PageSize", pageSize));
            command.Parameters.Add(CreateParameter("@OrderByClause", orderByClause, 20));

            return GetDtoList<Categories>(ref command);
        }

        /// <summary>
        /// Get total count
        /// </summary>
        /// <returns></returns>
        public int GetTotalCategoriesCount()
        {
            SqlCommand command = GetDbSprocCommand("GetCategoriesCount");
            //command.Parameters.Add(CreateParameter("@UserId", userId));
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
        /// Add/Update a category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public Categories EditCategory(Categories category)
        {
            SqlCommand command = GetDbSprocCommand("SaveCategory");
            command.Parameters.Add(CreateParameter("@CategoryId", category.CategoryId));
            command.Parameters.Add(CreateParameter("@Category", category.Category, 50));
            command.Parameters.Add(CreateParameter("@IconUrl", category.IconUrl, 50));
            command.Parameters.Add(CreateParameter("@IsEnabled", Convert.ToInt32(category.IsEnabled)));
            if (category.IsNew)
            {
                command.Parameters.Add(CreateParameter("@CreatedByUser", category.CreatedByUser, 50));
            }
            else
            {
                command.Parameters.Add(CreateParameter("@LastModifiedByUser", category.LastModifiedByUser, 50));

            }


            return GetSingleDto<Categories>(ref command);
        }

        /// <summary>
        /// Delete a Category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool DeleteCategory(int categoryId)
        {
            SqlCommand command = GetDbSprocCommand("DeleteCategory");
            command.Parameters.Add(CreateParameter("@CategoryId", categoryId));
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
