
using StoreyedMedia.Infrastructure;
using StoreyedMedia.Model;
using StroreyedMedia.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreyedMedia.Web.Controllers
{
    public class ContactController : Controller
    {

        #region Constructor

        readonly ContactsBal _service;

        /// <summary>
        /// initialize service in constructor
        /// </summary>
        public ContactController()
        {
            _service = new ContactsBal();

        }

        #endregion

        #region  Constants 

        private const string ContentType = "application/json";
        private const string FieldCreationDateFormat = "yyyy-MM-ddTHH:mm:ssZ";
        private const string IsStarred = "y";
        private const string NotStarred = "n";
        private const string ActionAddJson = "add";

        #endregion

        #region Actions

        #region CRUD
        /// <summary>
        /// return view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //call stubs here
            // DeleteAllDocuments();
            // AddDocuments();

            return View("Contact");
        }

        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetContacts(int page, int limit, string sortBy, string direction, string searchString = null, string starred = null)
        {
            int total = 0;
            int userId = 1;
            string orderByClause = sortBy + " " + direction;
            List<Contact> records = _service.GetAllContactsByUserId(userId, page, limit, out total, orderByClause);
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get contact details by id
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetContactDetailsById(int contactId)
        {
            Contact contact = _service.GetContactDetailsById(contactId);
            return Json(Content(contact.ToString(), ContentType));
        }


        /// <summary>
        /// edit contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Contact contact)
        {
            contact.UserId = 1;
            if (contact.Id == 0)
            {
                contact.IsNew = true;
                contact.CreatedByUser = "T";
                TempData["message"] = "Contact has been Added successfully";
            }
            else
                TempData["message"] = "Contact has been updated successfully";
            Contact result = _service.EditContact(contact);
            return Index();

        }

        /// <summary>
        /// Delete a contact based on identity of contact.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Remove(int id)
        {
            var result = _service.DeleteContact(id);
            TempData["deleteMessage"] = "Contact has been deleted";
            return Json(Content(result.ToString(), ContentType));
        }

        /// <summary>
        /// Star A Contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult StarAContact(int id)
        {
            var result = _service.StarAContact(id);
            return Json(Content(result.ToString(), ContentType));
        }

        /// <summary>
        /// delete multiple contacts
        /// </summary>
        /// <param name="lstContact"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public bool BulkActionOnContacts(string[] contactIdList, string action)
        {
            if (contactIdList == null)
            {
                return false;
            }
            if (action.ToLower() == "delete")
            {
                TempData["multipleAction"] = "Selected Contacts has been deleted";
                var result = _service.DeleteMultipleContacts(contactIdList);
                return result;
            }
            else if(action.ToLower() == "star")
            {
                TempData["multipleAction"] = "Selected Contacts has been starred";
                var result = _service.StarMultipleContacts(contactIdList);
                return result;

            }
            return false;
        }

       /// <summary>
       /// Import multiple contacts
       /// </summary>
       /// <param name="filePath"></param>
        public void ImportContacts(string filePath)
        {
            var fileName = filePath;

            DataSet ds = ExcelImport.ImportExcelXLS(fileName, true);
            DataTable dataTable = ds.Tables[0];
            _service.AddMultipleContacts(dataTable);

        }

        #endregion

        #region Star

        /// <summary>
        /// Star a contact based on identity of contact.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult StarContact(int id)
        {
            var result = _service.StarContact(id);
            return Json(Content(result.ToString(), ContentType));
        }

        /// <summary>
        /// Star multiple contacts
        /// </summary>
        /// <param name="lstContact"></param>
        /// <returns></returns>
        [HttpPost]
        public bool StarMultipleContacts(string[] lstContact)
        {
            var result = _service.StarMultipleContacts(lstContact);
            return result;
        }

        #endregion

        #region Alert


        /// <summary>
        /// Get alerts shared with a contact
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public JsonResult GetAlertsById(int id, string type)
        {
            List<Alert> records = _service.GetAlertsById(id, type);
            return Json(new { records }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete alert
        /// </summary>
        /// <param name="searchId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteAlert(int searchId)
        {
            var result = _service.DeleteAlert(searchId);
            return Json(Content(result.ToString(), ContentType));
        }
      
        

        #endregion

        #region Story

        /// <summary>
        /// Get story
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public JsonResult GetStoriesById(int id)
        {
            List<Story> records = _service.GetStoriesById(id);
            return Json(new { records }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Stubs 




        #endregion

        #endregion
    }
}