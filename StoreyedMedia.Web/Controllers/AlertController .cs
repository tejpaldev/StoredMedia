
using StoreyedMedia.Model;
using StroreyedMedia.BAL;
using System.Collections.Generic;
using System.Web.Mvc;

namespace StoreyedMedia.Web.Controllers
{
    public class AlertController : Controller
    {

        #region Constructor

        readonly AlertBal _service; 
        /// <summary>
        /// initialize service in constructor
        /// </summary>
        public AlertController()
        {
            _service = new AlertBal();

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
         
          
        /// <summary>
        /// Delete alert
        /// </summary>
        /// <param name="searchId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteAlert(int searchId)
        {
            var result = _service.DeleteAlert(searchId);
            TempData["DeleteMessage"] = "Alert has been deleted";
            return Json(Content(result.ToString(), ContentType));
        }

        /// <summary>
        /// Get alert details by id
        /// </summary>
        /// <param name="searchId"></param>
        /// <returns></returns>
        //[HttpPost]
        public ActionResult GetAlertDetails(int searchId)
        {
            var resultAlerts = _service.GetAlertDetailsById(searchId);
            ViewBag.Title = searchId == 0 ? "Create Alert" : "Edit Alert";
            return View("Alert", resultAlerts );
        }


        /// <summary>
        /// get all alerts by id
        /// </summary>
        /// <param name="searchId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAlerts(int searchId, string type)
        {
            type = "";
            var resultAlerts = _service.GetAlertsById(searchId, type);
            return Json(Content(resultAlerts.ToString(), ContentType));
        }

        /// <summary>
        /// Create Alert
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //public JsonResult CreateAlert(Alert alert, List<AlertMap> lstAlertMap, int id, string listType, ModelBase modelBase)
        //{
        //    bool result = _service.CreateAlert(alert, lstAlertMap, id, listType, modelBase);
        //    return Json(Content(result.ToString(), ContentType));
        //}


        /// <summary>
        /// create /update alert
        /// </summary>
        /// <param name="alert"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EditAlert(string id,string searchId ,string name, string keyWords, string[] sourcesList, string[] tagsList)
        { 
            Alert alert = new Alert { SearchName = name, Keywords = keyWords, SearchId = int.Parse(searchId ),Status=1,Id=int.Parse(id), listType="Contact",CreatedByUser="TestUser"};
            var result= _service.EditAlert(alert,sourcesList,tagsList);
            return Json( 1  ); 
        }
         
        

        #endregion


    }
}