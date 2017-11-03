using StoreyedMedia.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StoreyedMedia.DAL
{
    public class AlertDal : DalBase
    {
        #region Variables

        private int _ordinalContactId;
        private int _ordinalName;
        private int _ordinalNotes;
        private int _ordinalEmail;
        private int _ordinalPhone;
        private int _ordinalCreationDate;
        private int _ordinalIsStarred; 
        private int _ordinalResult;

        #endregion

        #region Repository Methods 

          
        /// <summary>
        /// Get Details of a alert
        /// </summary>
        /// <param name="alertId"></param>
        /// <returns></returns>
        public Alert GetAlertDetailsById(int alertId)
        {
            SqlCommand command = GetDbSprocCommand("GetAlertDetailsById");
            command.Parameters.Add(CreateParameter("@AlertId", alertId));
            return GetSingleDto<Alert>(ref command);
        }

        /// <summary>
        /// Get alerts shared with a contact
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Alert> GetAlertsById(int id,string type)
        {
            SqlCommand command = GetDbSprocCommand("GetAlertsById");
            command.Parameters.Add(CreateParameter("@Id", id));
            command.Parameters.Add(CreateParameter("@ListType", type,20));
            return GetDtoList<Alert>(ref command);
        }

        /// <summary>
        /// Get sources by Id
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>
        public List<Sources> GetSourcesById(int id )
        {
            SqlCommand command = GetDbSprocCommand("GetSourcesBySearchId");
            command.Parameters.Add(CreateParameter("@SearchId", id));
            return GetDtoList<Sources>(ref command);
        }

        /// <summary>
        /// Get tags by search id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Tags> GetTagsById(int id )
        { 
            SqlCommand command = GetDbSprocCommand("GetTagsBySearchId");
            command.Parameters.Add(CreateParameter("@SearchId", id));
            return GetDtoList<Tags>(ref command); 
        }


        
        /// <summary>
        /// Add/Update a contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public bool EditAlert(Alert alert,DataTable table )
        { 
            SqlCommand command = GetDbSprocCommand("CreateAlert");
            command.Parameters.Add(CreateParameter("@SearchId", alert.SearchId));
            command.Parameters.Add(CreateParameter("@SearchAlert", table));
            command.Parameters.Add(CreateParameter("@SearchName", alert.SearchName, 20));
            command.Parameters.Add(CreateParameter("@Keywords", alert.Keywords, 100));
            command.Parameters.Add(CreateParameter("@Status", alert.Status));
            command.Parameters.Add(CreateParameter("@Id", alert.Id ));
            command.Parameters.Add(CreateParameter("@ListType", alert.listType, 20));
            //command.Parameters.Add(CreateParameter("@ReturnVal", 20));
            
            if (alert.SearchId == 0)
            {
                command.Parameters.Add(CreateParameter("@CreatedByUser", alert.CreatedByUser, 50));
                command.Parameters.Add(CreateParameter("@CreatedOnDateTime", DateTime.Now));
            }
            else
            {
                command.Parameters.Add(CreateParameter("@LastModifiedByUser", alert.LastModifiedByUser, 50));
                command.Parameters.Add(CreateParameter("@LastModifiedDateTime", DateTime.Now));
            }

            return ExecuteNonQueryProcedures(ref command);


           // return GetSingleDto<Alert>(ref command);
        }

         
        /// <summary>
        /// Delete a contact
        /// </summary>
        /// <param name="searchId"></param>
        /// <returns></returns>
        public bool DeleteAlert(int searchId)
        {
            SqlCommand command = GetDbSprocCommand("DeleteAlert");
            command.Parameters.Add(CreateParameter("@SearchId", searchId));
            return ExecuteNonQueryProcedures(ref command);
        }

        /// <summary>
        /// Create Alert
        /// </summary>
        /// <returns></returns>
        public bool CreateAlert(Alert alert,DataTable lstAlertMap, int id, string listType, ModelBase modelBase)
        {
            SqlCommand command = GetDbSprocCommand("CreateAlert");
            command.Parameters.Add(CreateParameter("@SearchId", alert.SearchId));
            command.Parameters.Add(CreateParameter("@SearchAlert", lstAlertMap));
            command.Parameters.Add(CreateParameter("@SearchName", alert.SearchName,20));
            command.Parameters.Add(CreateParameter("@Keywords", alert.Keywords,100));
            command.Parameters.Add(CreateParameter("@Status", alert.Status));
            command.Parameters.Add(CreateParameter("@Id", id));
            command.Parameters.Add(CreateParameter("@ListType", listType,20));
            command.Parameters.Add(CreateParameter("@CreatedByUser", modelBase.CreatedByUser,50));
            command.Parameters.Add(CreateParameter("@CreatedOnDateTime", modelBase.CreatedOnDateTime));
            command.Parameters.Add(CreateParameter("@LastModifiedByUser", modelBase.LastModifiedByUser,50));
            command.Parameters.Add(CreateParameter("@LastModifiedDateTime", modelBase.LastModifiedDateTime));
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
                if (reader.HasRows)
                { 
                    reader.Read();
                 //   PopulateOrdinals(reader);
                    if (!reader.IsDBNull(_ordinalIsStarred))
                    {
                        result = reader.GetBoolean(_ordinalResult);
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
            // return the success/failure, it's either populated with data or null.
            return result;
        }

        

        #endregion
    }
}
