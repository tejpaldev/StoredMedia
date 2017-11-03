using System;
using StoreyedMedia.Model;
using System.Collections.Generic;
using System.Data;
using StoreyedMedia.DAL;
using StoreyedMedia.Infrastructure;

namespace StroreyedMedia.BAL
{
    public class ContactsBal
    {
        #region Constants

        private readonly ContactsDal _contacts ;


        #endregion

        #region Constructor

        public ContactsBal()
        {
            _contacts = new ContactsDal();
        }

        #endregion

        #region Business Methods 

        /// <summary>
        /// Get Contacts of a User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Contact> GetAllContactsByUserId(int userId, int pageNumber, int pageSize, out int total,string orderByClause)
        {
            total = GetTotalContactsCountOfUser(userId);
            return _contacts.GetAllContactsByUserId(userId, pageNumber, pageSize, orderByClause);
        }

        /// <summary>
        /// Get total count of Contacts
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetTotalContactsCountOfUser(int userId)
        {
            return _contacts.GetTotalContactsByUserId(userId);
        }


        
        /// <summary>
        /// Get Details of a Contact
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public Contact GetContactDetailsById(int contactId)
        {
            return _contacts.GetContactDetailsById(contactId);
        }

        /// <summary>
        /// Edit a contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public Contact EditContact(Contact contact)
        {
            return _contacts.EditContact(contact);

        }
         

        /// <summary>
        /// Delete a contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteContact(int id)
        {
            return _contacts.DeleteContact(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool StarAContact(int id)
        { 
            return _contacts.StarAContact(id, CommonBase.LoggedInUser);
        }

        /// <summary>
        /// Delete multiple Contacts
        /// </summary>
        /// <param name="contactsList"></param>
        /// <returns></returns>
        public bool DeleteMultipleContacts(string[] contactsList)
        {
            return _contacts.DeleteMultipleContacts(ConvertToDataTable(contactsList) );
        }

        /// <summary>
        /// Add multiple contacts
        /// </summary>
        /// <param name="contactsList"></param>
        /// <returns></returns>
        public bool AddMultipleContacts(DataTable contactsTable)
        { 
            return _contacts.AddMultipleContacts(contactsTable);
        }
        
        /// <summary>
        /// Star a contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool StarContact(int id)
        {
            return _contacts.StarContact(id);
        }

        /// <summary>
        /// Star multiple contacts
        /// </summary>
        /// <param name="contactsList"></param>
        /// <returns></returns>
        public bool StarMultipleContacts(string[] contactsList)
        {
            return _contacts.StarMultipleContacts(ConvertToDataTable(contactsList));
        }

        #region Alerts

        /// <summary>
        /// Delete Alert
        /// </summary>
        /// <param name="searchId"></param>
        /// <returns></returns>
        public bool DeleteAlert(int searchId)
        {
            return _contacts.DeleteAlert(searchId);
        }

       
        /// <summary>
        /// Get alerts shared with a contact
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Alert> GetAlertsById(int id, string type)
        {
            return _contacts.GetAlertsById(id, type);
        }

        /// <summary>
        /// Get stories shared with a contact
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Story> GetStoriesById(int id)
        {
            return _contacts.GetStoriesById(id);
        }

        #endregion

        #endregion

        #region Private Methods

        private Contact ValidateContact(Contact contact)
        {
            return _contacts.ValidateContact(contact);
        }

        private DataTable ConvertToDataTable(string[] listOfIds)
        {
            
            var lenthObject = listOfIds.Length;


            DataTable table = new DataTable();
            table.Columns.Add("[ContactId]", typeof(int)); 


            for (int index = 0; index <lenthObject; index++)
            { 
                table.Rows.Add(int.Parse(listOfIds[index]));

            }


            return table;

        }
        #endregion
    }
}
