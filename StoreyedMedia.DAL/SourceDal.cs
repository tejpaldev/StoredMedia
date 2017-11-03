using StoreyedMedia.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StoreyedMedia.DAL
{
    public class SourceDal : DalBase
    {
        

        #region Repository Methods 

        /// <summary>
        /// Validate during Edit
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public Source ValidateSource(Source Source)
        {
            return null;
        }

        /// <summary>
        /// Get Sources of a User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Source> GetAllSources( int pageNumber, int pageSize,string orderByClause)
        {
            
            SqlCommand command = GetDbSprocCommand("GetAllSources"); 
            command.Parameters.Add(CreateParameter("@PageNumber", pageNumber));
            command.Parameters.Add(CreateParameter("@PageSize", pageSize));
            command.Parameters.Add(CreateParameter("@OrderByClause", orderByClause,20));
            
            return GetDtoList<Source>(ref command);
        }

        /// <summary>
        /// Get total count
        /// </summary> 
        /// <returns></returns>
        public int GetTotalSources()
        {
            SqlCommand command = GetDbSprocCommand("GetSourcesCount"); 
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
        /// Get Details of a Source
        /// </summary>
        /// <param name="SourceId"></param>
        /// <returns></returns>
        public Source GetSourceDetailsById(int SourceId)
        {
            SqlCommand command = GetDbSprocCommand("GetSourceByID");
            command.Parameters.Add(CreateParameter("@SourceId", SourceId)); 
            return GetSingleDto<Source>(ref command);
        }

         

        /// <summary>
        /// Add/Update a Source
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public Source EditSource(Source Source)
        {
            SqlCommand command = GetDbSprocCommand("CreateSource");
            command.Parameters.Add(CreateParameter("@SourceId", Source.SourceId)); 
            command.Parameters.Add(CreateParameter("@DarkLogo", Source.DarkLogo, 50));
            command.Parameters.Add(CreateParameter("@SourceName", Source.SourceName, 100));
            command.Parameters.Add(CreateParameter("@LightLogo", Source.LightLogo, 50));
            command.Parameters.Add(CreateParameter("@Status", Source.Status ));
            if (Source.IsNew)
            {
                command.Parameters.Add(CreateParameter("@CreatedByUser", Source.CreatedByUser, 50));
            }
            else
            {
                command.Parameters.Add(CreateParameter("@LastModifiedByUser", Source.LastModifiedByUser, 50)); 
            }
 
            return GetSingleDto<Source>(ref command);
        }

        
        /// <summary>
        /// Archive A Source
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ArchiveASource(int id, string lastModifiedByUser)
        {
            SqlCommand command = GetDbSprocCommand("ArchiveSource");
            command.Parameters.Add(CreateParameter("@SourceId", id)); 
            command.Parameters.Add(CreateParameter("@LastModifiedByUser", lastModifiedByUser, 50));
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
                if (reader.HasRows || reader.RecordsAffected>0)
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
