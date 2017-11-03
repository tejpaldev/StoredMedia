using StoreyedMedia.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StoreyedMedia.DAL
{
    public class ContactsDal : DalBase
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
        /// Validate during Edit
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public Contact ValidateContact(Contact contact)
        {
            return null;
        }

        /// <summary>
        /// Get Contacts of a User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Contact> GetAllContactsByUserId(int userId, int pageNumber, int pageSize,string orderByClause)
        {
            
            SqlCommand command = GetDbSprocCommand("GetAllContacts");
            command.Parameters.Add(CreateParameter("@UserId", userId));
            command.Parameters.Add(CreateParameter("@PageNumber", pageNumber));
            command.Parameters.Add(CreateParameter("@PageSize", pageSize));
            command.Parameters.Add(CreateParameter("@OrderByClause", orderByClause,20));
            
            return GetDtoList<Contact>(ref command);
        }

        /// <summary>
        /// Get total count
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetTotalContactsByUserId(int userId)
        {
            SqlCommand command = GetDbSprocCommand("GetContactsCount");
            command.Parameters.Add(CreateParameter("@UserId", userId));
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
        /// Get Details of a Contact
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public Contact GetContactDetailsById(int contactId)
        {
            SqlCommand command = GetDbSprocCommand("GetContactDetails");
            command.Parameters.Add(CreateParameter("@ContactId", contactId));
            command.Parameters.Add(CreateParameter("@ListType", "Contact",50));
            return GetSingleDto<Contact>(ref command);
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
        /// Get stories shared with a contact
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Story> GetStoriesById(int id)
        {
            SqlCommand command = GetDbSprocCommand("GetStoryListById");
            command.Parameters.Add(CreateParameter("@Id", id));
            //command.Parameters.Add(CreateParameter("@ListType", type, 20));
            return GetDtoList<Story>(ref command);
        }

        /// <summary>
        /// Add/Update a contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public Contact EditContact(Contact contact)
        {
            SqlCommand command = GetDbSprocCommand("SaveContact");
            command.Parameters.Add(CreateParameter("@ContactId", contact.Id));
            command.Parameters.Add(CreateParameter("@UserId", contact.UserId));
            command.Parameters.Add(CreateParameter("@FirstName", contact.FirstName, 50));
            command.Parameters.Add(CreateParameter("@LastName", contact.LastName, 50));
            command.Parameters.Add(CreateParameter("@Notes", contact.Notes, 200));
            command.Parameters.Add(CreateParameter("@Email", contact.Email, 50));
            command.Parameters.Add(CreateParameter("@CellPhone", contact.CellPhone));
            command.Parameters.Add(CreateParameter("@OfficePhone", contact.OfficePhone));
            if (contact.IsNew)
            {
                command.Parameters.Add(CreateParameter("@CreatedByUser", contact.CreatedByUser, 50));
            }
            else
            {
                command.Parameters.Add(CreateParameter("@LastModifiedByUser", contact.LastModifiedByUser, 50));

            }


            return GetSingleDto<Contact>(ref command);
        }

        /// <summary>
        /// Delete a contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteContact(int id)
        {
            SqlCommand command = GetDbSprocCommand("DeleteContact");
            command.Parameters.Add(CreateParameter("@ContactId",  id));  
            return ExecuteNonQueryProcedures(ref command);
        }

        /// <summary>
        /// Star A Contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool StarAContact(int id, string lastModifiedByUser)
        {
            SqlCommand command = GetDbSprocCommand("StarAContact");
            command.Parameters.Add(CreateParameter("@ContactId", id)); 
            command.Parameters.Add(CreateParameter("@LastModifiedByUser", lastModifiedByUser, 50));
            return ExecuteNonQueryProcedures(ref command);
        }

        /// <summary>
        /// Delete multiple Contacts
        /// </summary>
        /// <param name="contactIdList"></param>
        /// <returns></returns>
        public bool DeleteMultipleContacts(DataTable contactIdList)
        {
            SqlCommand command = GetDbSprocCommand("DeleteContacts");
            command.Parameters.Add(CreateParameter("@ContactIdList", contactIdList ));
            return ExecuteNonQueryProcedures(ref command);
        }

        /// <summary>
        /// Add multiple contacts
        /// </summary>
        /// <param name="contactsList"></param>
        /// <returns></returns>
        public bool AddMultipleContacts(DataTable contactsList)
        {
            SqlCommand command = GetDbSprocCommand("AddMultipleContacts");
            command.Parameters.Add(CreateParameter("@ContactListMap", contactsList ));
            return ExecuteNonQueryProcedures(ref command); 
        }

        /// <summary>
        /// Star a contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool StarContact(int id)
        {
            SqlCommand command = GetDbSprocCommand("StarContact");
            command.Parameters.Add(CreateParameter("@ContactId", id));
            return ExecuteNonQueryProcedures(ref command);
        }

        /// <summary>
        /// Star multiple contacts
        /// </summary>
        /// <param name="contactIdList"></param>
        /// <returns></returns>
        public bool StarMultipleContacts(DataTable contactIdList)
        {
            SqlCommand command = GetDbSprocCommand("StarContacts");
            command.Parameters.Add(CreateParameter("@ContactsList", contactIdList));
            return ExecuteNonQueryProcedures(ref command);
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
